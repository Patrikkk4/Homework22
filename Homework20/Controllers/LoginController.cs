using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Homework20.AuthModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Homework20.Controllers
{
    public class LoginController : Controller
    {
        /// <summary>
        /// Поле менеджера регистрации
        /// </summary>
        private readonly UserManager<User> _userManager;
        /// <summary>
        /// Поле менеджера входа
        /// </summary>
        private readonly SignInManager<User> _signInManager;

        public LoginController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Метод отображения страницы входа
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginModel
            {
                ReturnUrl = returnUrl
            });
        }

        /// <summary>
        /// Метод входа пользователя
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if(ModelState.IsValid)
            {
                // получение результата сравнения введенных данных (имя пользователя и пароль) с записями в БД
                var loginResult = await _signInManager.PasswordSignInAsync(model.LoginProp, model.Password, false, lockoutOnFailure: false);

                // Условие удачного входа в учетную запись
                if(loginResult.Succeeded)
                {
                    // Условие проверки URL на локальный или host
                    if(Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }

                    // Пренаправление на стартовую страницу
                    return RedirectToAction("Index", "Index");
                }
            }

            // Действие при ошибке ввода
            ModelState.AddModelError("", "Неверные имя пользователя или пароль");
            return View(model);
        }

        /// <summary>
        /// Метод отображения регистрации пользователя
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterModel());
        }

        /// <summary>
        /// Метод регистрации польхователя
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            // Назначение имени нового пользователя
            var user = new User() { UserName = model.LoginProp };
            // Результат создания пользователя
            var createResult = await _userManager.CreateAsync(user, model.Password);

            // Проверки результатов создания пользоваетля
            if(createResult.Succeeded)
            {
                // Вход нового пользователя
                await _signInManager.SignInAsync(user, false);

                // Перенаправление на стартовую страницу
                return RedirectToAction("Index", "Index");
            }
            else
            {
                // Выбор указания ошибки при неудачной регистрации
                foreach (var identity in createResult.Errors)
                {
                    ModelState.AddModelError("", identity.Description);
                }
            }

            return View(model);
        }

        /// <summary>
        /// Метод выхода пользователя
        /// </summary>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // Выход текущего пользователя
            await _signInManager.SignOutAsync();

            // Перенаправление на стартовую страницу
            return RedirectToAction("Index", "Index");
        }
    }
}
