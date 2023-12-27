using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegisterAndLoginAPI_BL.Dtos
{
    public class UserResponseDTO
    {
        public string? UserName {  get; set; }
        public string? Token { get; set; }
        public Status Status { get; set; }
    }

    public enum Status
    {
        OK,
        NOT_FOUND,
        INVALID
    }
}
