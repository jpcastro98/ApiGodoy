using ApiGodoy.Models;
using ApiGodoy.Models.Dto;
using ApiGodoy.Repository;
using AutoMapper;

namespace ApiGodoy.Services
{
    public class UserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public UserService(IRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDTO>> GetAll()
        {
            var users = await _userRepository.GetAll();
            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }
    }
}
