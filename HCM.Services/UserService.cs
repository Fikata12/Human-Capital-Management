using HCM.Services.Models.User;
using HCM.Data;
using HCM.Data.Models;
using Microsoft.EntityFrameworkCore;
using HCM.Services.Contracts;
using static HCM.Common.GeneralApplicationConstants;
using static HCM.Common.NotificationMessagesConstants.User;
using AutoMapper;

namespace HCM.Services
{
    public class UserService : IUserService
    {
        private readonly HcmDbContext context;
        private readonly IJwtTokenGenerator jwtTokenGenerator;
        private readonly IPasswordHasher passwordHasher;
        private readonly IMapper mapper;

        public UserService(HcmDbContext context, IJwtTokenGenerator jwtTokenGenerator, IPasswordHasher passwordHasher, IMapper mapper)
        {
            this.context = context;
            this.jwtTokenGenerator = jwtTokenGenerator;
            this.passwordHasher = passwordHasher;
            this.mapper = mapper;
        }

        public async Task<LoginResponseDto> LoginAsync(string username, string password)
        {
            var user = await context.Users
                .Include(u => u.UsersRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Username == username);

            if (user == null || !passwordHasher.Verify(password, user.PasswordHash))
            {
                return new LoginResponseDto { Success = false, Message = "Invalid credentials" };
            }

            if (!user.IsActive)
            {
                return new LoginResponseDto { Success = false, Message = "Your account is inactive" };
            }

            var roles = user.UsersRoles.Select(ur => ur.Role.Name).ToList();
            var token = jwtTokenGenerator.GenerateToken(user, roles);

            return new LoginResponseDto
            {
                Success = true,
                Message = "Login successful",
                Token = token,
                Username = user.Username,
                Roles = roles
            };
        }

        public async Task<LoginResponseDto> RegisterAsync(RegisterRequestDto dto)
        {
            if (await context.Users.AnyAsync(u => u.Username == dto.Username))
            {
                return new LoginResponseDto { Success = false, Message = "Username is taken" };
            }

            if (await context.Users.AnyAsync(u => u.Email == dto.Email))
            {
                return new LoginResponseDto { Success = false, Message = "Email is taken" };
            }

            var employeeRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == EmployeeRoleName);

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = passwordHasher.Hash(dto.Password),
                UsersRoles = new List<UserRole>
                {
                    new UserRole { RoleId = employeeRole!.Id }
                }
            };

            context.Users.Add(user);
            await context.SaveChangesAsync();

            return new LoginResponseDto
            {
                Success = true,
                Message = SuccessfullyCreatedAccount
            };
        }

        public async Task<UserInfoDto?> GetUserInfoAsync(Guid userId)
        {
            var user = await context.Users
                .Include(u => u.Person)
                .ThenInclude(p => p.Department)
                .Include(u => u.UsersRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return null;
            }

            return mapper.Map<UserInfoDto>(user);
        }
    }
}
