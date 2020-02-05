using DijiWalk.Mobile.Services.Interfaces;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace DijiWalk.Mobile.Resources.utils
{
    public class ValidatableObject<T> : BindableBase
    {
        private readonly List<IValidationRule<T>> _validations;
        private List<string> _errors;
        private T _value;
        private bool _isValid;

        public List<IValidationRule<T>> Validations => _validations;

        public List<string> Errors
        {
            get
            {
                return _errors;
            }
            set
            {
                _errors = value;
                SetProperty(ref _errors, value);
            }
        }

        public T Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                SetProperty(ref _value, value);
            }
        }

        public bool IsValid
        {
            get
            {
                return _isValid;
            }
            set
            {
                _isValid = value;
                SetProperty(ref _isValid, value);
            }
        }

        public ValidatableObject()
        {
            _isValid = true;
            _errors = new List<string>();
            _validations = new List<IValidationRule<T>>();
        }

        public bool Validate()
        {
            Errors.Clear();

            Errors = _validations.Where(v => !v.Check(Value)).Select(v => v.ValidationMessage).ToList();

            IsValid = !Errors.Any();

            return IsValid;
        }
    }

}
