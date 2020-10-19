using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Homework20.AuthModel
{
    public class LoginModel
    {
        /// <summary>
        /// Свойство Имент пользователя для входа
        /// </summary>
        [Required(ErrorMessage = "Непраильное имя пользователя")]
        public string LoginProp { get; set; }

        /// <summary>
        /// Свойство пароля для входа
        /// </summary>
        [Required(ErrorMessage = "Непраильный пароль"), DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Свойство возвращаемого адреса
        /// </summary>
        public string ReturnUrl { get; set; }
    }
}
