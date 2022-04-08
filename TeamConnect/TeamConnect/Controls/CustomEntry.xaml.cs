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

        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(
            propertyName: nameof(BorderColor),
            returnType: typeof(Color),
            declaringType: typeof(CustomEntry));

        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

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

        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(
            propertyName: nameof(FontFamily),
            returnType: typeof(string),
            declaringType: typeof(CustomEntry));

        public string FontFamily
        {
            get => (string)GetValue(FontFamilyProperty);
            set => SetValue(FontFamilyProperty, value);
        }

        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(
            propertyName: nameof(FontSize),
            returnType: typeof(double),
            declaringType: typeof(CustomEntry));

        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(
            propertyName: nameof(TextColor),
            returnType: typeof(Color),
            declaringType: typeof(CustomEntry));

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        public static readonly BindableProperty PlaceholderColorProperty = BindableProperty.Create(
            propertyName: nameof(PlaceholderColor),
            returnType: typeof(Color),
            declaringType: typeof(CustomEntry));

        public Color PlaceholderColor
        {
            get => (Color)GetValue(PlaceholderColorProperty);
            set => SetValue(PlaceholderColorProperty, value);
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

        public static readonly BindableProperty NotPasswordIconProperty = BindableProperty.Create(
            propertyName: nameof(NotPasswordIcon),
            returnType: typeof(ImageSource),
            declaringType: typeof(CustomEntry));

        public ImageSource NotPasswordIcon
        {
            get => (ImageSource)GetValue(NotPasswordIconProperty);
            set => SetValue(NotPasswordIconProperty, value);
        }

        public static readonly BindableProperty PasswordIconProperty = BindableProperty.Create(
            propertyName: nameof(PasswordIcon),
            returnType: typeof(ImageSource),
            declaringType: typeof(CustomEntry));

        public ImageSource PasswordIcon
        {
            get => (ImageSource)GetValue(PasswordIconProperty);
            set => SetValue(PasswordIconProperty, value);
        }

        public static readonly BindableProperty FocusedColorProperty = BindableProperty.Create(
            propertyName: nameof(FocusedColor),
            returnType: typeof(Color),
            declaringType: typeof(CustomEntry));

        public Color FocusedColor
        {
            get => (Color)GetValue(FocusedColorProperty);
            set => SetValue(FocusedColorProperty, value);
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

        public static readonly BindableProperty ErrorFontFamilyProperty = BindableProperty.Create(
            propertyName: nameof(ErrorFontFamily),
            returnType: typeof(string),
            declaringType: typeof(CustomEntry));

        public string ErrorFontFamily
        {
            get => (string)GetValue(ErrorFontFamilyProperty);
            set => SetValue(ErrorFontFamilyProperty, value);
        }

        public static readonly BindableProperty ErrorFontSizeProperty = BindableProperty.Create(
            propertyName: nameof(ErrorFontSize),
            returnType: typeof(double),
            declaringType: typeof(CustomEntry));

        public double ErrorFontSize
        {
            get => (double)GetValue(ErrorFontSizeProperty);
            set => SetValue(ErrorFontSizeProperty, value);
        }

        public static readonly BindableProperty ErrorColorProperty = BindableProperty.Create(
            propertyName: nameof(ErrorColor),
            returnType: typeof(Color),
            declaringType: typeof(CustomEntry));

        public Color ErrorColor
        {
            get => (Color)GetValue(ErrorColorProperty);
            set => SetValue(ErrorColorProperty, value);
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

            if (propertyName == BorderColorProperty.PropertyName)
            {
                SetFrameColor();
            }
            else if (propertyName == IsEntryFocusedProperty.PropertyName)
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
                passwordButton.Source = IsPassword ? PasswordIcon : NotPasswordIcon;
            }
        }

        #endregion

        #region -- Private helpers --

        private void SetFrameColor()
        {
            frame.RemoveDynamicResource(Frame.BorderColorProperty);
            frame.BorderColor = IsEntryFocused ? FocusedColor : IsError ? ErrorColor : BorderColor;
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
