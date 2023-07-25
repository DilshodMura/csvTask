using AutoMapper;
using CsvHelper;
using CsvHelper.Configuration;
using Domain.Models;
using Domain.Repositories;
using Domain.Services;
using Service.ServiceModels;
using System.Globalization;

namespace Service.Services
{
    public sealed class CsvParser : ICsvParser
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public CsvParser(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Parsing csv file
        /// </summary>
        public async Task<IEnumerable<IUser>> ParseCsvFile(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                var records = csv.GetRecords<User>().ToList();

                var parsedUsers = new List<IUser>();
                foreach (var user in records)
                {
                    var existingUser = await _userRepository.GetUserByUsernameAsync(user.UserName);
                    if (existingUser != null)
                    {
                        // If the user exists, update their information
                        _mapper.Map(user, existingUser);
                        await _userRepository.AddOrUpdateUserAsync(existingUser);
                        parsedUsers.Add(existingUser);
                    }
                    else
                    {
                        // If the user doesn't exist, add them as a new user
                        var newUser = _mapper.Map<User>(user); // Or UserDb, depending on your implementation
                        await _userRepository.AddOrUpdateUserAsync(newUser);
                        parsedUsers.Add(newUser);
                    }
                }

                return parsedUsers;
            }
        }
    }
 }
