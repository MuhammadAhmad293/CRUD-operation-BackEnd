using CRUDoperations.DataModel.Entities;
using CRUDoperations.Services.DTOs;

namespace CRUDoperations.Services.UserService
{
    public interface IUserService
    {
        Task<bool> Add(UserDto userDto);
        Task<bool> Update(UserDto userDto);
        Task<bool> Delete(int id);
        Task<UserDto> GetById(int id);
        Task<List<UserDto>> GetAll();

    }
}
