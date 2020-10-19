using Microsoft.AspNetCore.Http;
using PersLib.Data;
using PersLib.Interfaces;
using PersLib.Model;
using PersLib.ProcessClass;
using PersWpf.Models;
using PersWpf.ProcessClass;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace PersWpf.ViewModels
{
    class MainViewModel : PropChange
    {
        /// <summary>
        /// Свойство экземпляра класса получения\загрузки данных
        /// </summary>
        private GetUploadPers GetUpload { get; set; }

        private ObservableCollection<Pers> pers;
        /// <summary>
        /// Свойтсво коллекция полученных данных из бд
        /// </summary>
        public ObservableCollection<Pers> Pers
        {
            get => pers;
            set { pers = value; OnPropertyChange(nameof(Pers)); }
        }

        /// <summary>
        /// Свойство нового персонажа
        /// </summary>
        public Pers NewPers { get; set; }

        private string imagePath;
        /// <summary>
        /// Свойство пути к файлу изображения
        /// </summary>
        public string ImagePath
        {
            get => imagePath;
            set { imagePath = value; OnPropertyChange(nameof(ImagePath)); }
        }

        private string infMessage;
        /// <summary>
        /// Свойство информационных сообщений
        /// </summary>
        public string InfMessage
        {
            get => infMessage;
            set { infMessage = value; OnPropertyChange(nameof(InfMessage)); }
        }

        /// <summary>
        /// Команда выбора файла изображения
        /// </summary>
        public ICommand ChouseImage => new CommandClass<object>(obj =>
        {
            OpenImage open = new OpenImage();

            ImagePath = open.SchouseImageFile();
        });

        /// <summary>
        /// Команда добавления нового персонажа
        /// </summary>
        public ICommand AddPers => new CommandClass<object>(async obj =>
        {
            InfMessage = "Отправка данных";

            GetUpload = new GetUploadPers(NewPers, ImagePath);

            if (await GetUpload.UploadPers())
            {
                // Обновление коллекции записей из БД для отображения
                Pers = await GetPersCol();

                InfMessage = $@"Персонаж ""{NewPers.Name}"" добавлен";
            }
            else
            {
                InfMessage = "Ошибка добавления персонажа, проверьте соединение";
            }
        });

        /// <summary>
        /// Команда обновления коллекции записей
        /// </summary>
        public ICommand Refresh => new CommandClass<object>(async obj =>
        {
            // обновление коллекции записей из БД для отображения
            Pers = await GetPersCol();
        });

        public MainViewModel()
        {
            // Экземпляр класса методов получения\загрузки данных
            GetUpload = new GetUploadPers();

            // Экзкмпляр класа нового персонажа для добавления
            NewPers = new Pers();

            // Первое получение заисей из БД для отображения
            Task.Run(async () => Pers = await GetPersCol());
        }

        /// <summary>
        /// Метод полчения данных для отображения
        /// </summary>
        private async Task<ObservableCollection<Pers>> GetPersCol()
        {
            InfMessage = "Соединение с сервером...";

            // Получение записей из БД
            var temp = await Task.Run(() => GetUpload.GetAllPers());

            if (temp != null)
            {
                InfMessage = "Соединение с установлено";

                return new ObservableCollection<Pers>(temp);
            }
            else
            {
                InfMessage = "Соединение с отсутствует";

                return temp = null;
            }
        }
    }
}
