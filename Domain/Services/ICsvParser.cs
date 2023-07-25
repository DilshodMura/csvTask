using Domain.Models;

namespace Domain.Services
{
    public interface ICsvParser
    {
        /// <summary>
        /// Parsing csv file
        /// </summary>
        public Task<IEnumerable<IUser>> ParseCsvFile(Stream stream);
    }
}
