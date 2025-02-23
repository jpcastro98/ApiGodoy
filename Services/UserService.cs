using System.Net;
using System.Security.Cryptography;
using ApiGodoy.Database;
using ApiGodoy.Entities.SessionHistory.SessionHistoryDto;
using ApiGodoy.Entities.User.UserDto;
using ApiGodoy.Entities.UserData.UserDataDto;
using ApiGodoy.User;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ApiGodoy.Services
{
    public class UserService
    {
        private readonly ApiDbContext _context;
        private readonly IMapper _mapper;
        private readonly PasswordHasher<object> _hasher;
        private readonly UserDataService _userDataService;

        public UserService(ApiDbContext context, IMapper mapper, UserDataService userDataService)
        {
            _context = context;
            _mapper = mapper;
            _hasher = new PasswordHasher<object>();
            _userDataService = userDataService;
        }

        public async Task<IEnumerable<ResultUserDto>> GetAll()
        {
            var users = await _context.Users
                .Include(u => u.UserData)
                .Select(u => new ResultUserDto
                {
                    Id = u.Id,
                    Email = u.Email,
                    UserData = _mapper.Map<ResultUserDataDto>(u.UserData),
                    SessionHistory = _context.SessionHistory
                        .Where(s => s.UserId == u.Id)
                        .OrderByDescending(s => s.LastLogin)
                        .Select(s => new ResultSessionHistoryDto
                        {
                            LastLogin = s.LastLogin
                        })
                        .FirstOrDefault()
                })
                .ToListAsync();

            return _mapper.Map<IEnumerable<ResultUserDto>>(users);

        }

        public async Task<UserDto?> GetById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return user == null ? null : _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto?> GetByEmail(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user == null ? null : _mapper.Map<UserDto>(user);
        }

        public async Task<UserModel?> Add(CreateUserDto createUserDto)
        {
            var exists = await UserExists(createUserDto.Email);
            if (exists) throw new Exception($"El usuario con email {createUserDto.Email} ya existe.");
            var user = _mapper.Map<UserModel>(createUserDto);
            user.Password = _hasher.HashPassword(null, user.Password);

            var createdUser = (await _context.Users.AddAsync(user)).Entity;
            user.UserData.Score = await _userDataService.CalculateScore(user.UserData);
            await _context.SaveChangesAsync();

            return createdUser;
        }

        public async Task<string> Update(int id, UpdateUserDto userDto)
        {
            var existingUser = await _context.Users
                .Include(u => u.UserData)
                .FirstOrDefaultAsync(u => u.Id == id);
            var exisDocument = await validateDocumentExists(userDto.UserData.IdentificationNumber, id);
            var existEmail =  await validateEmailExists(userDto.Email, id);

            if (exisDocument)
                throw new HttpRequestException($"Ya existe un usuario con este número de documento: {userDto.UserData.IdentificationNumber}", null, HttpStatusCode.BadRequest);
            if (existEmail)
                throw new HttpRequestException($"Ya existe un usuario con este email: {userDto.Email}", null, HttpStatusCode.BadRequest);
            if (existingUser == null) 
                throw new HttpRequestException($"Usuario no encontrado", null, HttpStatusCode.BadRequest);

            _mapper.Map(userDto, existingUser);
            existingUser.Password = _hasher.HashPassword(null, existingUser.Password);
            if (existingUser.UserData != null) existingUser.UserData.Score = await _userDataService.CalculateScore(existingUser.UserData);
            await _context.SaveChangesAsync();

            return "Usuario actualizado existosamente";
        }


        public async Task<bool> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UserExists(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<bool> validateEmailExists(string Email, int id)
        {
            return await _context.Users.AnyAsync(u => u.Email == Email && u.Id != id);
        }

        public async Task<bool> validateDocumentExists(string IdentificationNumber, int id)
        {
            return await _context.Users.AnyAsync(u => u.UserData.IdentificationNumber == IdentificationNumber && u.Id != id);
        }
    }
}
