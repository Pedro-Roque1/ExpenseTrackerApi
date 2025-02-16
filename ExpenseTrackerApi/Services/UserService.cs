using AutoMapper;
using ExpenseTrackerApi.Data;
using ExpenseTrackerApi.Models.Dtos;
using ExpenseTrackerApi.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;

namespace ExpenseTrackerApi.Services
{
    public class UserService : IUserRepository
    {
        private ApiDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserService(ApiDbContext dbContext, IMapper mapper) 
        { 
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task Create(UserCreateRequest userRequest)
        {
            if (this.FindUserByUserName(userRequest.UserName).Result != null)
                throw new Exception("Already registered username.");

            var user = new User();
            user.Password = BCrypt.Net.BCrypt.HashPassword(userRequest.Password);
            user.Email = userRequest.Email;
            user.UserName = userRequest.UserName;

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<UserResponseDto>> GetAll()
        {
            return _mapper.Map<List<UserResponseDto>>(await _dbContext.Users.ToListAsync());
        }

        public async Task<User> FindUserByUserName(string login)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == login);
        }
    }
}
