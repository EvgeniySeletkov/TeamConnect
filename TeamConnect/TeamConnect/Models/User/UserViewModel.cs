using Prism.Mvvm;
using System;

namespace TeamConnect.Models.User
{
    public class UserViewModel : BindableBase
    {
        #region -- Public properties --

        private int _id;
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private string _photo;
        public string Photo
        {
            get => _photo;
            set => SetProperty(ref _photo, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string _surname;
        public string Surname
        {
            get => _surname;
            set => SetProperty(ref _surname, value);
        }

        private DateTime _startWorkTime;
        public DateTime StartWorkTime
        {
            get => _startWorkTime;
            set => SetProperty(ref _startWorkTime, value);
        }

        private DateTime _endWorkTime;
        public DateTime EndWorkTime
        {
            get => _endWorkTime;
            set => SetProperty(ref _endWorkTime, value);
        }

        #endregion
    }
}
