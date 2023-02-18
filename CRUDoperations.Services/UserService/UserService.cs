using CRUDoperations.DataModel.Entities;
using CRUDoperations.Repositories.UnitOfWork;
using CRUDoperations.Services.Base;
using CRUDoperations.Services.CustomExceptions;
using CRUDoperations.Services.DTOs;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace CRUDoperations.Services.UserService
{
    public class UserService : BaseService, IUserService
    {
        public UserService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        #region Public Methods
        public async Task<bool> Add(UserDto userDto)
        {
            bool isValidUserRequrst = ValidateUser(userDto);
            if (!isValidUserRequrst)
                return isValidUserRequrst;

            User user = MapUser(userDto);
            UnitOfWork.UserRepository.CreateAsyn(user);

            return await UnitOfWork.CommitAsync() > default(int);
        }

        public async Task<bool> Update(UserDto userDto)
        {
            bool isValidUserRequrst = ValidateUser(userDto, true);
            if (!isValidUserRequrst)
                return isValidUserRequrst;

            User user = await UnitOfWork.UserRepository.FirstOrDefaultAsync(user => user.Id == userDto.Id);
            if (user is null)
                throw new ObjectNotFoundException("User not found !");

            MapUser(user, userDto);
            UnitOfWork.UserRepository.Update(user);

            return await UnitOfWork.CommitAsync() > default(int);
        }

        public async Task<bool> Delete(int id)
        {
            if (id is default(int))
                throw new InvalidRequestException();

            User user = await UnitOfWork.UserRepository.FirstOrDefaultAsync(user => user.Id == id);
            if (user is null)
                throw new ObjectNotFoundException("User not found");

            UnitOfWork.UserRepository.Delete(user);

            return await UnitOfWork.CommitAsync() > default(int);
        }

        public async Task<UserDto> GetById(int id)
        {
            if (id is default(int))
                throw new InvalidRequestException();

            User user = await UnitOfWork.UserRepository.FirstOrDefaultAsync(user => user.Id == id);
            if (user is null)
                throw new ObjectNotFoundException("User not found");

            return MapUserDto(user);
        }
        public async Task<List<UserDto>> GetAll()
        {
            List<User> users = await UnitOfWork.UserRepository.GetAllAsync();
            if (users?.Count > default(int))
            {
                return MapUserDtos(users);
            }
            return new List<UserDto>();
        }

        #endregion

        #region Private Methods
        private static bool ValidateUser(UserDto userDto, bool isUpdate = false)
        {
            bool isValid = false;
            if (userDto is null)
                throw new InvalidRequestException();

            if (string.IsNullOrWhiteSpace(userDto.FirstName))
                throw new NameRequiredException("Please enter first name");

            if (string.IsNullOrWhiteSpace(userDto.LastName))
                throw new NameRequiredException("Please enter last name");

            if (string.IsNullOrWhiteSpace(userDto.Email))
                throw new InvalidRequestException("Please enter email");

            if (string.IsNullOrWhiteSpace(userDto.UserName))
                throw new NameRequiredException("Please enter user name");

            if (string.IsNullOrWhiteSpace(userDto.Password) && !isUpdate)
                throw new NameRequiredException("Please enter password");

            //if (Regex.IsMatch(userDto.Email, @"^[^\s@]+@([^\s@.,]+\.)+[^\s@.,]{2,}$"))
            //    throw new NameRequiredException("Please enter valid email");

            return !isValid;
        }
        private static User MapUser(UserDto userDto)
        {
            return new User()
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                UserName = userDto.UserName,
                Password = HashPassword(userDto.Password)
            };
        }
        private static void MapUser(User user, UserDto userDto)
        {
            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.UserName = userDto.UserName;
            user.Email = userDto.Email;
            if (!string.IsNullOrWhiteSpace(userDto.Password))
                user.Password = HashPassword(userDto.Password);
        }
        private static UserDto MapUserDto(User user)
        {
            return new UserDto()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName,
                Password = user.Password
            };
        }
        private static List<UserDto> MapUserDtos(List<User> users)
        {
            List<UserDto> usersDto = new();
            foreach (User user in users)
            {
                usersDto.Add(new UserDto()
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName
                });
            }
            return usersDto;
        }
        private static string HashPassword(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            return Convert.ToBase64String(hashBytes);
        }

        #endregion
    }
}
