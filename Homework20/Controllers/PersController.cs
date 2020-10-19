using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PersLib.Data;
using PersLib.Interfaces;
using PersLib.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;

namespace Homework20.Controllers
{
    [Authorize]
    public class PersController : Controller
    {
        private readonly IPers ipers;

        /// <summary>
        /// Конструктор получения контекста данных
        /// </summary>
        /// <param name="webHost"></param>
        /// <param name="dataContext"></param>
        public PersController(IPers ipers)
        {
            this.ipers = ipers;
        }

        /// <summary>
        /// Метод отображения страницы персонажей
        /// </summary>
        /// <returns></returns>
        public IActionResult Pers(string search)
        {
            // Условие получения всех или оной записи
            if (string.IsNullOrEmpty(search))
            {
                // Отображение всех записей БД
                return View(ipers.GetPers());
            }
            else
            {
                // Отображение записи по результатамм поиска
                return View(ipers.GetOnePers(search));
            }
        }

        /// <summary>
        /// Метод добавления персонажей
        /// </summary>
        /// <param name="uploadImage"></param>
        /// <param name="name"></param>
        /// <param name="bio"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddPers(IFormFile uploadImage, Pers character)
        {
            // Помещение избображения в массив байтов
            using (var memStream = new MemoryStream())
            {
                // Копирование файла в память
                uploadImage.CopyTo(memStream);
                // Создание массива байтов из памяти
                var imgBytes = memStream.ToArray();
                // Объявление экзкмпляра класса для передачи данных в Web Api
                AddPersClass addPers = new AddPersClass() 
                { 
                    NewPers = character, 
                    imageName = uploadImage.FileName,
                    Upload = imgBytes 
                };

                // получение результата от сервера
                if (ipers.AddPers(addPers).StatusCode == HttpStatusCode.OK)
                {
                    return Redirect("~/Pers/Pers");
                }
            }
          
            // Перенос на страницу персонажей
            return Redirect("~/Pers/Pers");
        }
    }
}
