using ProjectApi.API.DTOs;
using System.Threading.Tasks;

namespace ProjectApi.Bussines_Logic.Services
{
    public interface IAccountService
    {
        Task<bool> ChangePasswordAsync(ChangePasswordDto dto);
        Task<string> GenerateResetTokenAsync(string email);
        Task<bool> ResetPasswordAsync(ResetPasswordDto dto);
    }
} 