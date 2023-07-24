using Domain.Models;

namespace Domain.Services
{
    public interface ICsvParser
    {
        IEnumerable<IUser> ParseCsvFile(Stream stream);
    }
}
