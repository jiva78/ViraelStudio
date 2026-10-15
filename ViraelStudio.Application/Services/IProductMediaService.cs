using System;
using System.Collections.Generic;
using System.Text;

namespace ViraelStudio.Application.Services
{
    public interface IProductMediaService
    {
        Task<string> SaveAsync(
            Stream fileStream,
            string fileName,
            CancellationToken cancellationToken = default);

        Task DeleteAsync(
            string url,
            CancellationToken cancellationToken = default);
    }
}
