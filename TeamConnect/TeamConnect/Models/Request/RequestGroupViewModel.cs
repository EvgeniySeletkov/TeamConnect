using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TeamConnect.Models.Request
{
    public class RequestGroupViewModel : ObservableCollection<RequestViewModel>
    {
        public RequestGroupViewModel(
            DateTime date)
            : base()
        {
            Date = date;
        }

        #region -- Publlic properties --

        private DateTime _date;
        public DateTime Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
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
