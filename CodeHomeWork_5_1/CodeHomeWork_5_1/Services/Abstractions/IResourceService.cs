using CodeHomeWork_5_1.Dtos;
using CodeHomeWork_5_1.Dtos.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeHomeWork_5_1.Services.Abstractions
{
    public interface IResourceService
    {
        Task<ResourceDto> GetResourceById(int id);

        Task<ResourceListResponse> GetResourceList();
    }
}
