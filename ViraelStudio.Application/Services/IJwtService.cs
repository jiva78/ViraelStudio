using System;
using System.Collections.Generic;
using System.Text;

namespace ViraelStudio.Application.Services
{
    public interface IJwtService
    {
        string GenerateToken(string userId, string email, IList<string> roles);
    }
}
