using Domain.Models;

namespace Domain.Repositories
{
    public interface IUserRepository
    {
        /// <summary>
        /// Get users by adding sorting and number of users
        /// </summary>
        Task<IEnumerable<IUser>> GetAllUsersAsync(string sort = "asc", int limit = 10);

        /// <summary>
        /// Get user by userName
        /// </summary>
        Task<IUser> GetUserByUsernameAsync(string username);

        /// <summary>
        /// Add or update username
        /// </summary>
        Task AddOrUpdateUserAsync(IUser user);
    }
}
