using PersLib.Data;
using PersLib.Interfaces;
using PersLib.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PersWpf.Models
{
    class GetUploadPers
    {
        /// <summary>
        /// Свойство экземпляра класса реализущего интерфейс IPers для отправки и получения дланных
        /// </summary>
        public IPers Api { get; set; }

        /// <summary>
        /// Свойство нового персонажа для загрузки на сервер
        /// </summary>
        public Pers NewPers { get; set; }

        /// <summary>
        /// Поле физического пути к файлу изображения
        /// </summary>
        private string imagePath;

        public GetUploadPers(Pers pers, string imagePath)
        {
            NewPers = pers;

            this.imagePath = imagePath;

            Api = new PersApi();
        }

        public GetUploadPers()
        {
        }

        /// <summary>
        /// Метод получения всех записей из БД
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Pers> GetAllPers()
        {
            try
            {
                return new ObservableCollection<Pers>(new PersApi().GetPers());
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Метод формирования экзепляра вспомогательного класса для загрузки данных в БД
        /// </summary>
        /// <returns></returns>
        public async Task<bool> UploadPers()
        {
            try
            {
                // Формирование массива байтов из файла изображения
                byte[] imgBytes = await Task.Run(() => File.ReadAllBytes(imagePath));

                var imgName = Path.GetFileName(imagePath);

                // Объявление экзкмпляра вспомогательного класса для загрузки данных в БД
                AddPersClass addPers = new AddPersClass() { NewPers = this.NewPers, Upload = imgBytes, imageName = imgName };

                // Отправка данных на сервер и получение ответа
                if (Api.AddPers(addPers).StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
