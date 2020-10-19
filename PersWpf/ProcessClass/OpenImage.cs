using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersWpf.ProcessClass
{
    class OpenImage
    {
        /// <summary>
        /// Метод получения физического пути к файлу изображения
        /// </summary>
        /// <returns></returns>
        public string SchouseImageFile()
        {
            // Объявление экзкмпляра класса диалогового окна выбора файла
            OpenFileDialog ofd = new OpenFileDialog();

            // Фильтр расширений файлов
            ofd.Filter = "Изображение (*.jpeg,*.jpg,*.gif,*.png,*.bmp)|*.jpeg;*.jpg;*.gif;*.png;*.bmp";

            // Условие подтверждение выбора файла
            if (ofd.ShowDialog() == true)
            {
                return ofd.FileName;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
