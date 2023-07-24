// UserRepository.cs
using AutoMapper;
using Database.DbContexts;
using Database.Entities;
using Domain.Models;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UserRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<IUser>> GetAllUsersAsync(string sort = "asc", int limit = 10)
        {
            var usersQuery = _context.Users.AsQueryable();

            if (sort.ToLower() == "desc")
                usersQuery = usersQuery.OrderByDescending(u => u.UserName);
            else
                usersQuery = usersQuery.OrderBy(u => u.UserName);

            var usersDb = await usersQuery.Take(limit).ToListAsync();
            return _mapper.Map<IEnumerable<IUser>>(usersDb);
        }

        public async Task<IUser> GetUserByUsernameAsync(string username)
        {
            var userDb = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
            return _mapper.Map<IUser>(userDb);
        }

        public async Task AddOrUpdateUserAsync(IUser user)
        {
            var userDb = _mapper.Map<UserDb>(user);

            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userDb.UserName);

            if (existingUser != null)
            {
                // Update existing user
                existingUser.Id = userDb.Id;
                existingUser.Age = userDb.Age;
                existingUser.City = userDb.City;
                existingUser.PhoneNumber = userDb.PhoneNumber;
                existingUser.Email = userDb.Email;
            }
            else
            {
                // Add new user
                _context.Users.Add(userDb);
            }

            await _context.SaveChangesAsync();
        }
    }
}