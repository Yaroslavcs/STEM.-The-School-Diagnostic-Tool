using ProjectApi.API.DTOs;
using System.Threading.Tasks;

namespace ProjectApi.Bussines_Logic.Services
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterDto dto);
        Task<string> LoginAsync(LoginDto dto);
    }
} 