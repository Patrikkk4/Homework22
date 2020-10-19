using PersLib.Interfaces;
using PersLib.ProcessClass;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersLib.Model
{
    public class Pers : PropChange, IPersModel
    {
        /// <summary>
        /// ID персонажа в БД
        /// </summary>
        public int Id 
        {
            get;
            set;
        }

        private string image;
        /// <summary>
        /// Путь к изображению
        /// </summary>
        public string Image
        {
            get => image;
            set { image = value; OnPropertyChange(nameof(Image)); }
        }

        private string name;
        /// <summary>
        /// Имя персонажа
        /// </summary>
        public string Name
        {
            get => name;
            set { name = value; OnPropertyChange(nameof(Name)); }
        }

        private string bio;
        /// <summary>
        /// Описание персонажа
        /// </summary>
        public string Bio
        {
            get => bio;
            set { bio = value; OnPropertyChange(nameof(Bio)); }
        }
    }
}
