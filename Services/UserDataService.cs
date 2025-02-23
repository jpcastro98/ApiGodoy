using ApiGodoy.Entities.UserData;
using ApiGodoy.Entities.UserData.UserDataDto;
using ApiGodoy.Enums;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ApiGodoy.Database;
using ApiGodoy.Entities.User.UserDto;

namespace ApiGodoy.Services
{
    public class UserDataService
    {
        private readonly ApiDbContext _context;
        private readonly IMapper _mapper;

        public UserDataService(ApiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDataDto>> GetAll()
        {
            var users = await _context.UsersData.ToListAsync();
            return _mapper.Map<IEnumerable<UserDataDto>>(users);
        }

        public async Task<UserDataDto?> GetById(int id)
        {
            var user = await _context.UsersData.FindAsync(id);
            return user == null ? null : _mapper.Map<UserDataDto>(user);
        }

        public async Task<UserDataDto?> Add(CreateUserDataDto createUserDataDto)
        {
            if (await UserExists(createUserDataDto.IdentificationNumber)) return null;

            var userDataModel = _mapper.Map<UserDataModel>(createUserDataDto);
            userDataModel.Score = await CalculateScore(userDataModel);

            _context.UsersData.Add(userDataModel);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserDataDto>(userDataModel);
        }

        public async Task<UserDataDto?> Update(int id, UpdateUserDataDto userDataDto)
        {
            var existingUser = await _context.UsersData.FindAsync(id);
            if (existingUser == null) return null;

            _mapper.Map(userDataDto, existingUser);
            existingUser.Score = await CalculateScore(existingUser);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserDataDto>(existingUser);
        }

        public async Task<bool> Delete(int id)
        {
            var user = await _context.UsersData.FindAsync(id);
            if (user == null) return false;

            _context.UsersData.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UserExists(string identificationNumber)
        {
            return await _context.UsersData.AnyAsync(u => u.IdentificationNumber == identificationNumber);
        }

        public async Task<int> CalculateScore(UserDataModel userDataModel)
        {
            var userData = await _context.UsersData.Include(u => u.User)
                .FirstOrDefaultAsync(u => u.Id == userDataModel.Id);

            if (userData?.User?.Email == null) return (int)EmailDomainScore.defaultDomain;

            string emailDomain = userData.User.Email.Split('@')[1].Split('.')[0].ToLower();
            int score = emailDomain switch
            {
                "gmail" => (int)EmailDomainScore.gmail,
                "hotmail" => (int)EmailDomainScore.hotmail,
                _ => (int)EmailDomainScore.defaultDomain
            };

            int fullNameLength = userDataModel.Names?.Length ?? 0;
            if (fullNameLength > 10) score += 20;
            else if (fullNameLength >= 5) score += 10;

            return score;
        }
    }
}
