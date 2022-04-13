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

        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private double _latitude;
        public double Latitude
        {
            get => _latitude;
            set => SetProperty(ref _latitude, value);
        }

        private double _longitude;
        public double Longitude
        {
            get => _longitude;
            set => SetProperty(ref _longitude, value);
        }

        private string _address;
        public string Address
        {
            get => _address;
            set => SetProperty(ref _address, value);
        }

        private string _timeZoneId;
        public string TimeZoneId
        {
            get => _timeZoneId;
            set => SetProperty(ref _timeZoneId, value);
        }

        private string _countryCode;
        public string CountryCode
        {
            get => _countryCode;
            set => SetProperty(ref _countryCode, value);
        }

        private string _position;
        public string Position
        {
            get => _position;
            set => SetProperty(ref _position, value);
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

        private bool _isAccountCreated;
        public bool IsAccountCreated
        {
            get => _isAccountCreated;
            set => SetProperty(ref _isAccountCreated, value);
        }

        #endregion
    }
}
