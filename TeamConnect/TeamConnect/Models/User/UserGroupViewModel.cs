using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TeamConnect.Models.User
{
    public class UserGroupViewModel : ObservableCollection<UserViewModel>
    {
        public UserGroupViewModel(
            TimeSpan startWorkTime,
            TimeSpan endWorkTime)
        {
            StartWorkTime = startWorkTime;
            EndWorkTime = endWorkTime;
        }

        #region -- Publlic properties --

        private TimeSpan _endWorkTime;
        public TimeSpan EndWorkTime
        {
            get => _endWorkTime;
            set => SetProperty(ref _endWorkTime, value);
        }

        private TimeSpan _startWorkTime;
        public TimeSpan StartWorkTime
        {
            get => _startWorkTime;
            set => SetProperty(ref _startWorkTime, value);
        }

        #endregion

        #region -- Private helpers --

        private void SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, newValue))
            {
                field = newValue;
                OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
