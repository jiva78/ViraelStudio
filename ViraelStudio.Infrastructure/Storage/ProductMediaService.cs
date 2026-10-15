using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using ViraelStudio.Application.Services;
using ViraelStudio.Application.Options;

namespace ViraelStudio.Infrastructure.Storage
{
    public class ProductMediaService : IProductMediaService
    {
        private readonly string _uploadPath;
        private readonly string _relativeDirectory;

        public ProductMediaService(string webRootPath, ProductMediaStorageOptions options)
        {
            _uploadPath = Path.GetFullPath(Path.Combine(webRootPath, options.DirectoryName));
            _relativeDirectory = options.DirectoryName.Replace('\\', '/').Trim('/');
        }
        public Task DeleteAsync(string url, CancellationToken cancellationToken = default)
        {
            var prefix = $"/{_relativeDirectory}/";
            if (!url.StartsWith(prefix, StringComparison.Ordinal))
                return Task.CompletedTask;

            var fileName = url[prefix.Length..];

            if (string.IsNullOrWhiteSpace(fileName) ||
                fileName != Path.GetFileName(fileName))
            {
                return Task.CompletedTask;
            }

            var filePath = Path.Combine(_uploadPath, fileName);

            cancellationToken.ThrowIfCancellationRequested();

            if (File.Exists(filePath))
                File.Delete(filePath);

            return Task.CompletedTask;
        }

        public async Task<string> SaveAsync(Stream fileStream, string fileName, CancellationToken cancellationToken = default)
        {
            Directory.CreateDirectory(_uploadPath);

            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            var storedFileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(_uploadPath, storedFileName);

            await using var outputStream = new FileStream(
                filePath,
                FileMode.CreateNew,
                FileAccess.Write,
                FileShare.None,
                bufferSize: 81920,
                useAsync: true);

            await fileStream.CopyToAsync(outputStream, cancellationToken);

            return $"/{_relativeDirectory}/{storedFileName}";
        }
    }
}
