using EmployeeTaskManagement.API.DTOs;
using EmployeeTaskManagement.API.Repositories;
using EmployeeTaskManagement.API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeTaskManagement.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;

        public AuthService(
            IAuthRepository authRepository,
            IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
        }

        public async Task<LoginResponseDto?> LoginAsync(
            LoginRequestDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email))
            {
                throw new ArgumentException(
                    "Email is required.");
            }

            if (string.IsNullOrWhiteSpace(dto.Password))
            {
                throw new ArgumentException(
                    "Password is required.");
            }

            var user = await _authRepository.LoginAsync(
                dto.Email,
                dto.Password);

            if (user == null)
            {
                return null;
            }

            bool passwordValid =
                BCrypt.Net.BCrypt.Verify(
                    dto.Password,
                    user.PasswordHash);

            if (!passwordValid)
            {
                return null;
            }

            return new LoginResponseDto
            {
                UserId = user.UserId,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role,
                Token = GenerateJwtToken(user)
            };
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    user.UserId.ToString()),

                new Claim(
                    ClaimTypes.Name,
                    user.FullName),

                new Claim(
                    ClaimTypes.Email,
                    user.Email),

                new Claim(
                    ClaimTypes.Role,
                    user.Role)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _configuration["Jwt:Key"]!));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
}