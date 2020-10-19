using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PersApi.Context;
using PersLib.Data;
using PersLib.Interfaces;
using PersLib.Model;

namespace PersApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetPersController : Controller
    {
        private DataContext context;

        /// <summary>
        /// Конструктор получения контекста данных
        /// </summary>
        public GetPersController()
        {
            context = new DataContext();
        }

        /// <summary>
        /// Метод получение всех записей из контекста данных
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<IPersModel> Get()
        {
            return new List<Pers>(context.Characters).OrderBy(x => x.Name);
        }

        /// <summary>
        /// Метод получения одной записи в соответствии с поиском
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetOne/{name}")]
        public IEnumerable<Pers> GetOnePers(string name)
        {
            // Получение данных в соответствии с поиском
            List<Pers> tempChar = new List<Pers>(context.Characters.Where(c => c.Name.Contains(name)).ToList());

            // Условие возвращаемой коллекции
            if (tempChar.Count > 0)
            {
                return tempChar;
            }
            else
            {
                return tempChar = null;
            }
        }

        /// <summary>
        /// Метод добавление нового персонажа
        /// </summary>
        /// <param name="pers"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddPers/{pers}")]
        public async Task<bool> AddPers(AddPersClass pers)
        {
            // Присваивание нового имени файла изобрадежения
            string imageName = $"{Path.GetRandomFileName()}-{pers.imageName}";

            // Путь к файу изображения, записываемый в БД
            pers.NewPers.Image = $@"https://localhost:5001/Images/{imageName}";

            // Запись массива байтов на диск
            System.IO.File.WriteAllBytes($"wwwroot/Images/{imageName}", pers.Upload);

            // Добавление нового персонажа в контекст данных
            context.Characters.Add(pers.NewPers);

            // Сохранение изменений в БД
            await context.SaveChangesAsync();

            return true;
        }

        #region _

        #region _
        //// POST: PErsController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
        #endregion

        #region _
        //// GET: PErsController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}
        #endregion

        #region _
        //// POST: PErsController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
        #endregion

        #region _
        //// GET: PErsController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: PErsController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
        #endregion
        #endregion
    }
}
