using Microsoft.AspNetCore.Server.HttpSys;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Homework20.AuthModel
{
    public class RegisterModel
    {
        /// <summary>
        /// Свойство имени пользоввателя для регистрации
        /// </summary>
        [Required(ErrorMessage = "Введите имя пользователя.")]
        public string LoginProp { get; set; }

        /// <summary>
        /// Свойство пароля для регистрации
        /// </summary>
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Свойство повтора пароля для регистрации
        /// </summary>
        [Required, DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
