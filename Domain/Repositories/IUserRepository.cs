using Domain.Models;

namespace Domain.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<IUser>> GetAllUsersAsync(string sort = "asc", int limit = 10);
        Task<IUser> GetUserByUsernameAsync(string username);
        Task AddOrUpdateUserAsync(IUser user);
    }
}
