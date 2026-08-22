using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CarRent.Application.Abstractions
{
    public interface IFileStorageService
    {
        Task<string> SaveFileAsync(IFormFile file, string folderName, CancellationToken cancellationToken = default);
        void DeleteFile(string relativePath);
    }
}