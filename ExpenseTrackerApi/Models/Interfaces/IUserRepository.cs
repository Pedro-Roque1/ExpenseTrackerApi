using ExpenseTrackerApi.Models.Dtos;

namespace ExpenseTrackerApi.Models.Interfaces
{
    public interface IUserRepository
    {
        Task<User> FindUserByUserName(string login);
        Task<List<UserResponseDto>> GetAll();
        Task Create(UserCreateRequest user);
    }
}
