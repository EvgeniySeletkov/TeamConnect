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
            DateTime startWorkTime,
            DateTime endWorkTime)
        {
            StartWorkTime = startWorkTime;
            EndWorkTime = endWorkTime;
        }

        #region -- Publlic properties --

        private DateTime _endWorkTime;
        public DateTime EndWorkTime
        {
            get => _endWorkTime;
            set => SetProperty(ref _endWorkTime, value);
        }

        private DateTime _startWorkTime;
        public DateTime StartWorkTime
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
