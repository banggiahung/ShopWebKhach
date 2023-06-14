#nullable disable
using Swappa.ApplicationCore.Models;
using Swappa.ApplicationCore.DtoModels;

namespace Swappa.ApplicationCore.ViewModels{
    public class UserVM{
        public UserVM()
        {
        }
        public UserDto Data { get; set; }
        public IEnumerable<UserDto> DataList { get; set; } = new List<UserDto>();
        public int Total { get; set; }
    }
}