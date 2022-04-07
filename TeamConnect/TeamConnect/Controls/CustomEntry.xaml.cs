using System;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace TeamConnect.Controls
{
    public partial class CustomEntry
    {
        public CustomEntry()
        {
            InitializeComponent();
        }

        #region -- Public properties --

        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            propertyName: nameof(Text),
            returnType: typeof(string),
            declaringType: typeof(CustomEntry),
            defaultBindingMode: BindingMode.TwoWay);

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
            propertyName: nameof(Placeholder),
            returnType: typeof(string),
            declaringType: typeof(CustomEntry));

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        public static readonly BindableProperty IconProperty = BindableProperty.Create(
            propertyName: nameof(Icon),
            returnType: typeof(string),
            declaringType: typeof(CustomEntry));

        public string Icon
        {
            get => (string)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public static readonly BindableProperty FocusedIconProperty = BindableProperty.Create(
            propertyName: nameof(FocusedIcon),
            returnType: typeof(string),
            declaringType: typeof(CustomEntry));

        public string FocusedIcon
        {
            get => (string)GetValue(FocusedIconProperty);
            set => SetValue(FocusedIconProperty, value);
        }

        public static readonly BindableProperty IsPasswordProperty = BindableProperty.Create(
            propertyName: nameof(IsPassword),
            returnType: typeof(bool),
            declaringType: typeof(CustomEntry));

        public bool IsPassword
        {
            get => (bool)GetValue(IsPasswordProperty);
            set => SetValue(IsPasswordProperty, value);
        }

        public static readonly BindableProperty IsPasswordButtonVisibleProperty = BindableProperty.Create(
            propertyName: nameof(IsPasswordButtonVisible),
            returnType: typeof(bool),
            declaringType: typeof(CustomEntry));

        public bool IsPasswordButtonVisible
        {
            get => (bool)GetValue(IsPasswordButtonVisibleProperty);
            set => SetValue(IsPasswordButtonVisibleProperty, value);
        }

        public static readonly BindableProperty ButtonIconProperty = BindableProperty.Create(
            propertyName: nameof(ButtonIcon),
            returnType: typeof(ImageSource),
            declaringType: typeof(CustomEntry));

        private ImageSource ButtonIcon
        {
            get => (ImageSource)GetValue(ButtonIconProperty);
            set => SetValue(ButtonIconProperty, value);
        }

        public static readonly BindableProperty IsErrorProperty = BindableProperty.Create(
            propertyName: nameof(IsError),
            returnType: typeof(bool),
            declaringType: typeof(CustomEntry),
            defaultBindingMode: BindingMode.TwoWay);

        public bool IsError
        {
            get => (bool)GetValue(IsErrorProperty);
            set => SetValue(IsErrorProperty, value);
        }

        public static readonly BindableProperty ErrorTextProperty = BindableProperty.Create(
            propertyName: nameof(ErrorText),
            returnType: typeof(string),
            declaringType: typeof(CustomEntry));

        public string ErrorText
        {
            get => (string)GetValue(ErrorTextProperty);
            set => SetValue(ErrorTextProperty, value);
        }

        public static readonly BindableProperty IsEntryFocusedProperty = BindableProperty.Create(
            propertyName: nameof(IsEntryFocused),
            returnType: typeof(bool),
            declaringType: typeof(CustomEntry));

        private bool IsEntryFocused
        {
            get => (bool)GetValue(IsEntryFocusedProperty);
            set => SetValue(IsEntryFocusedProperty, value);
        }

        public static readonly BindableProperty KeyboardProperty = BindableProperty.Create(
            propertyName: nameof(Keyboard),
            returnType: typeof(Keyboard),
            declaringType: typeof(CustomEntry));

        public Keyboard Keyboard
        {
            get => (Keyboard)GetValue(KeyboardProperty);
            set => SetValue(KeyboardProperty, value);
        }

        #endregion

        #region -- Overrides --

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == IsEntryFocusedProperty.PropertyName)
            {
                IsError = false;
                SetFrameColor();
            }
            else if (propertyName == IsErrorProperty.PropertyName)
            {
                SetFrameColor();
            }
            else if (propertyName == IsPasswordProperty.PropertyName)
            {
                passwordButton.RemoveDynamicResource(Image.SourceProperty);
                string imageRes = IsPassword ? "ic_eye_off" : "ic_eye";

                passwordButton.SetDynamicResource(Image.SourceProperty, imageRes);
            }
        }

        #endregion

        #region -- Private helpers --

        private void SetFrameColor()
        {
            frame.RemoveDynamicResource(Frame.BorderColorProperty);
            string colorRes = IsEntryFocused ? "appcolor_i16" : IsError ? "appcolor_i22" : "appcolor_i4";

            frame.SetDynamicResource(Frame.BorderColorProperty, colorRes);
        }

        private void PasswordButtonTapped(object sender, EventArgs e)
        {
            IsPassword = !IsPassword;
        }

        private void EntryFocused(object sender, FocusEventArgs e)
        {
            IsEntryFocused = true;
        }

        private void EntryUnfocused(object sender, FocusEventArgs e)
        {
            IsEntryFocused = false;
        }

        private void EntryTapped(object sender, EventArgs e)
        {
            entry.Focus();
        }

        #endregion
    }
}
