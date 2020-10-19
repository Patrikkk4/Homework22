using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using PersLib.Interfaces;
using PersLib.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PersLib.Data
{
    public class PersApi : IPers
    {
        private HttpClient Client { get; set; }

        /// <summary>
        /// Конструктор создания HttpClient
        /// </summary>
        public PersApi()
        {
            Client = new HttpClient();
        }

        /// <summary>
        /// Метод запроса на получение всех записей БД
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Pers> GetPers()
        {
            // URL запроса
            string url = @"https://localhost:5001/api/GetPers";

            // Получение результата на запрос 
            string result = Client.GetStringAsync(url).Result;

            // Десериализация ответа сервера
            return JsonConvert.DeserializeObject<IEnumerable<Pers>>(result);
        }

        /// <summary>
        /// Метод запроса на получение записи в соотвтствии с поиском
        /// </summary>
        /// <param name="persName"></param>
        /// <returns></returns>
        public IEnumerable<Pers> GetOnePers(string persName)
        {
            // URL запроса
            string url = $@"https://localhost:5001/api/GetPers/GetOne/{persName}";

            // Получение результата на запрос 
            var result = Client.GetStringAsync(url).Result;

            // Десериализация ответа сервера
            return JsonConvert.DeserializeObject<IEnumerable<Pers>>(result);
        }

        // Метод передачи данных в Web Api (отправляется экзкмпляр вспомогательного класса, содержащий данные по персонажу, оригинальное имя файла изображения и массив байтов изображения)
        public HttpResponseMessage AddPers(AddPersClass newPers)
        {
            // URL запроса
            string url = $@"https://localhost:5001/api/GetPers/AddPers/{newPers}";

            // Формирование Post запроса
            var result = Client.PostAsync(
                requestUri: url,
                content: new StringContent(JsonConvert.SerializeObject(newPers), Encoding.UTF8,
                mediaType: "application/json")).Result; // Сериализация экзкмпляра вспомогательного класса с данными для отправки в Web Api и получение ответа

            return result;
        }
    }
}
