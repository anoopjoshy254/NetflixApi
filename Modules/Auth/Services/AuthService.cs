using System;
using System.Threading.Tasks;
using NetflixApi.Modules.Auth.DTOs;
using NetflixApi.Modules.Auth.Models;
using NetflixApi.Modules.Users.Models;
using NetflixApi.Modules.Users.Services;
using NetflixApi.Modules.Users.DTOs;
using NetflixApi.Modules.Notifications.Interfaces;

namespace NetflixApi.Modules.Auth.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IPasswordResetRepository _passwordResetRepository;
        private readonly IEmailVerificationRepository _emailVerificationRepository;
        private readonly IJwtService _jwtService;
        private readonly IEmailService _emailService;

        public AuthService(
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            IUserRoleRepository userRoleRepository,
            IRefreshTokenRepository refreshTokenRepository,
            IPasswordResetRepository passwordResetRepository,
            IEmailVerificationRepository emailVerificationRepository,
            IJwtService jwtService,
            IEmailService emailService)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _passwordResetRepository = passwordResetRepository;
            _emailVerificationRepository = emailVerificationRepository;
            _jwtService = jwtService;
            _emailService = emailService;
        }

        public async Task RegisterAsync(RegisterRequestDto dto)
        {
            var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
            if (existingUser != null)
            {
                throw new Exception("Email address is already in use.");
            }

            // Hash password using BCrypt
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = dto.Email,
                PasswordHash = passwordHash,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber,
                IsActive = true,
                IsEmailVerified = false,
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.AddAsync(user);

            // Assign default role = User
            var role = await _roleRepository.GetByNameAsync("User");
            if (role == null)
            {
                // Create role if it doesn't exist (assuming DB initialization)
                role = new Role { Name = "User" };
                await _roleRepository.AddAsync(role);
            }

            var userRole = new UserRole
            {
                UserId = user.Id,
                RoleId = role.Id
            };
            await _userRoleRepository.AddAsync(userRole);

            // Generate 6-digit OTP
            var token = new Random().Next(100000, 999999).ToString();
            var emailVerification = new EmailVerification
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddHours(1), // 1 hour expiry for OTP
                Verified = false
            };

            await _emailVerificationRepository.AddAsync(emailVerification);
            
            // Send OTP via Brevo Email Service
            string subject = "Verify Your Netflix Clone Account";
            string htmlContent = $"<h1>Welcome to Netflix Clone!</h1><p>Your 6-digit verification code is: <strong>{token}</strong></p><p>This code will expire in 1 hour.</p>";
            await _emailService.SendEmailAsync(user.Email, subject, htmlContent);
        }

        public async Task RegisterAdminAsync(RegisterRequestDto dto)
        {
            var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
            if (existingUser != null)
            {
                throw new Exception("Email address is already in use.");
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = dto.Email,
                PasswordHash = passwordHash,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber,
                IsActive = true,
                IsEmailVerified = true, // Auto-verify admin
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.AddAsync(user);

            // Assign Admin role
            var role = await _roleRepository.GetByNameAsync("Admin");
            if (role == null)
            {
                role = new Role { Name = "Admin" };
                await _roleRepository.AddAsync(role);
            }

            var userRole = new UserRole
            {
                UserId = user.Id,
                RoleId = role.Id
            };
            await _userRoleRepository.AddAsync(userRole);
        }

        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);
            if (user == null || !user.IsActive)
            {
                throw new Exception("Invalid email or password.");
            }

            // Verify password using BCrypt
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
            if (!isPasswordValid)
            {
                throw new Exception("Invalid email or password.");
            }

            // Generate tokens
            var roles = await _userRoleRepository.GetRolesByUserIdAsync(user.Id);
            var accessToken = _jwtService.GenerateAccessToken(user, roles);
            var refreshTokenString = _jwtService.GenerateRefreshToken();

            // Save Refresh Token
            var refreshToken = new RefreshToken
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Token = refreshTokenString,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                CreatedAt = DateTime.UtcNow,
                IsRevoked = false
            };

            await _refreshTokenRepository.AddAsync(refreshToken);

            var userDto = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                IsActive = user.IsActive,
                IsEmailVerified = user.IsEmailVerified,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };

            return new AuthResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshTokenString,
                User = userDto
            };
        }

        public async Task LogoutAsync(string token)
        {
            var refreshToken = await _refreshTokenRepository.GetByTokenAsync(token);
            if (refreshToken != null)
            {
                refreshToken.IsRevoked = true;
                await _refreshTokenRepository.UpdateAsync(refreshToken);
            }
        }

        public async Task ForgotPasswordAsync(ForgotPasswordDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            var token = Guid.NewGuid().ToString("N");
            var passwordReset = new PasswordReset
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddHours(2), // 2 hours expiry
                Used = false
            };

            await _passwordResetRepository.AddAsync(passwordReset);
        }

        public async Task ResetPasswordAsync(ResetPasswordDto dto)
        {
            var reset = await _passwordResetRepository.GetByTokenAsync(dto.Token);
            if (reset == null || reset.Used || reset.ExpiresAt < DateTime.UtcNow)
            {
                throw new Exception("Invalid or expired password reset token.");
            }

            var user = await _userRepository.GetByIdAsync(reset.UserId);
            if (user == null || !user.IsActive)
            {
                throw new Exception("User not found.");
            }

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            user.UpdatedAt = DateTime.UtcNow;
            await _userRepository.UpdateAsync(user);

            reset.Used = true;
            await _passwordResetRepository.UpdateAsync(reset);
        }

        public async Task VerifyEmailAsync(string token)
        {
            var verification = await _emailVerificationRepository.GetByTokenAsync(token);
            if (verification == null || verification.Verified || verification.ExpiresAt < DateTime.UtcNow)
            {
                throw new Exception("Invalid or expired email verification token.");
            }

            var user = await _userRepository.GetByIdAsync(verification.UserId);
            if (user == null || !user.IsActive)
            {
                throw new Exception("User not found.");
            }

            user.IsEmailVerified = true;
            user.UpdatedAt = DateTime.UtcNow;
            await _userRepository.UpdateAsync(user);

            verification.Verified = true;
            await _emailVerificationRepository.UpdateAsync(verification);
        }

        public async Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenDto dto)
        {
            var oldRefreshToken = await _refreshTokenRepository.GetByTokenAsync(dto.RefreshToken);
            if (oldRefreshToken == null || oldRefreshToken.IsRevoked || oldRefreshToken.ExpiresAt < DateTime.UtcNow)
            {
                throw new Exception("Invalid or expired refresh token.");
            }

            var user = await _userRepository.GetByIdAsync(oldRefreshToken.UserId);
            if (user == null || !user.IsActive)
            {
                throw new Exception("User not found.");
            }

            // Revoke/Replace old refresh token (rotation)
            oldRefreshToken.IsRevoked = true;
            await _refreshTokenRepository.UpdateAsync(oldRefreshToken);

            // Generate new tokens
            var roles = await _userRoleRepository.GetRolesByUserIdAsync(user.Id);
            var newAccessToken = _jwtService.GenerateAccessToken(user, roles);
            var newRefreshTokenString = _jwtService.GenerateRefreshToken();

            var newRefreshToken = new RefreshToken
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Token = newRefreshTokenString,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                CreatedAt = DateTime.UtcNow,
                IsRevoked = false
            };

            await _refreshTokenRepository.AddAsync(newRefreshToken);

            var userDto = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                IsActive = user.IsActive,
                IsEmailVerified = user.IsEmailVerified,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };

            return new AuthResponseDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshTokenString,
                User = userDto
            };
        }
    }
}
