using CodeHomeWork_5_1.Dtos.Responses;
using CodeHomeWork_5_1.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeHomeWork_5_1.Services.Abstractions
{
    public interface IUserService
    {
        Task<PageResponse<UserDto>> GetUsersList();

        Task<UserDto> GetUserById(int id);

        Task<UserResponseCreate> CreateUser(string name, string job);

        Task<UserResponseUpdate> UpdateUser(int id, string name, string job);

        Task<UserResponseUpdate> UpdateUserPatch(int id, string name, string job);

        Task<UserResponseCreate> DeleteUser(int id);
    }
}
