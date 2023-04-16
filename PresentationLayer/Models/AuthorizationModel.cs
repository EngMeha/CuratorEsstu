using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Models
{
    public class AuthorizationModel
    {
        [Required]
        [Display(Name = "Введите Логин")]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Введите Пароль")]
        public string Password { get; set; }
    }
}
