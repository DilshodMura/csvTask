using Domain.Repositories;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ICsvParser _csvParsingService;

        public UsersController(IUserRepository userRepository, ICsvParser csvParsingService)
        {
            _userRepository = userRepository;
            _csvParsingService = csvParsingService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadUsers(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Invalid file.");

            // Check if the file is a CSV file
            if (file.ContentType != "text/csv" && file.ContentType != "application/vnd.ms-excel")
                return BadRequest("Invalid file format. Only CSV files are allowed.");

            var users = _csvParsingService.ParseCsvFile(file.OpenReadStream());

            foreach (var user in users)
            {
                await _userRepository.AddOrUpdateUserAsync(user);
            }

            return Ok(users);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers(string sort = "asc", int limit = 10)
        {
            var users = await _userRepository.GetAllUsersAsync(sort, limit);
            return Ok(users);
        }
    }
}
