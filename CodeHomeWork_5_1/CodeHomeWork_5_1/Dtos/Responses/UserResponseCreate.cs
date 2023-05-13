using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeHomeWork_5_1.Dtos.Responses
{
    public class UserResponseCreate : UserResponse
    {
        public int Id { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
    }
}
