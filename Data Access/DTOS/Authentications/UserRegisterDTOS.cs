using System.ComponentModel.DataAnnotations;

namespace RepositoryCourses.Data_Access.DTOS.Authentications
{
    public class UserRegisterDTOS : UserLoginDTO
    {
        [Required]
        public string Name { get; set; }

    }
}
