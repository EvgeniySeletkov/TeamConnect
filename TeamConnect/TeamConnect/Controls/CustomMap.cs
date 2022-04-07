using System;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace TeamConnect.Controls
{
    public class CustomMap : Map
    {
        #region -- Public properties --

        public static readonly BindableProperty PinProperty = BindableProperty.Create(
            propertyName: nameof(Pin),
            returnType: typeof(Pin),
            declaringType: typeof(CustomMap));

        public Pin Pin
        {
            get => (Pin)GetValue(PinProperty);
            set => SetValue(PinProperty, value);
        }

        #endregion

        #region -- Overrides --

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(Pin))
            {
                ClearPins();
                SetPin();
            }
        }

        #endregion

        #region -- Private helpers --

        private void ClearPins()
        {
            Pins.Clear();
        }

        private void SetPin()
        {
            if (Pin is not null)
            {
                Pins.Add(Pin);
            }
        }

        #endregion
    }
}
