
using DijiWalk.Mobile.Resources.utils;
using DijiWalk.Mobile.Services.Common;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace DijiWalk.Mobile.ViewModels.ViewEntities
{
    public class Login : INotifyPropertyChanged
    {
        public Login()
        {
            Pseudo = new ValidatableObject<string>();
            Password = new ValidatableObject<string>();
            AddValidations();
        }

        private ValidatableObject<string> _pseudo { get; set; }
        private ValidatableObject<string> _password { get; set; }

        private string _pseudoError { get; set; }
        private string _passwordError { get; set; }

        public string PseudoError
        {
            get { return _pseudoError; }
            set
            {
                _pseudoError = value;
                OnPropertyChanged();
            }
        }

        public string PasswordError
        {
            get { return _passwordError; }
            set
            {
                _passwordError = value;
                OnPropertyChanged();
            }
        }


        public ValidatableObject<string> Pseudo
        {
            get { return _pseudo; }
            set
            {
                _pseudo = value;
                OnPropertyChanged();
            }
        }


        public ValidatableObject<string> Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public bool ValidatePseudo()
        {
            if (!_pseudo.Validate()) PseudoError = _pseudo.Errors.FirstOrDefault(); else PseudoError = "";
            return _pseudo.Validate();
        }

        public bool ValidatePassword()
        {
            if (!_password.Validate()) PasswordError = _password.Errors.FirstOrDefault(); else PasswordError = "";
            return _password.Validate();
        }


        public bool Validate()
        {
            var pseudo = ValidatePseudo();
            var password = ValidatePassword();
            return pseudo && password;
        }

        private void AddValidations()
        {
            _pseudo.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A pseudo is required." });
            _password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A password is required." });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
