using ApiGodoy.Database;
using ApiGodoy.Entities.SessionHistory;
using ApiGodoy.Entities.SessionHistory.SessionHistoryDto;
using ApiGodoy.Entities.User.UserDto;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace ApiGodoy.Services
{
    public class SessionHistoryService
    {
        private readonly ApiDbContext _context;
        private readonly IMapper _mapper;
        private readonly PasswordHasher<object> _hasher;

        public SessionHistoryService(ApiDbContext sessionHistory, IMapper mapper)
        {
            _context = sessionHistory;
            _mapper = mapper;
        }

        public async Task<UserDto> GetById(int id)
        {
            var user = await _context.SessionHistory.FindAsync(id);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<SessionHistoryDto> GetByUser(int UserId)
        {
            var lastSession = await _context.SessionHistory
                .Where(u => u.UserId == UserId)
                .OrderByDescending(u => u.LastLogin) 
                .FirstOrDefaultAsync();

            return _mapper.Map<SessionHistoryDto>(lastSession);
        }


        public async Task<SessionHistoryModel?> Add(CreateSessionHistoryDto sessionHistoryDto)
        {
            var sessionDto = _mapper.Map<SessionHistoryModel>(sessionHistoryDto);
            var session =( await _context.SessionHistory.AddAsync(sessionDto)).Entity;
            await _context.SaveChangesAsync();
            return session;
        }
    }
}
