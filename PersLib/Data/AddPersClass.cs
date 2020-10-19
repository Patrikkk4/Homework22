using PersLib.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersLib.Data
{
    public class AddPersClass
    {
        /// <summary>
        /// Свойство нового персонажа
        /// </summary>
        public Pers NewPers { get; set; }

        /// <summary>
        /// Свойство оригинального названия файла изображения
        /// </summary>
        public string imageName { get; set; }

        /// <summary>
        /// Свойство массива байтов передаваемого изображения
        /// </summary>
        public byte[] Upload { get; set; }
    }
}
