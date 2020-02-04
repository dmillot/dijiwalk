using System;
using System.Collections.Generic;
using System.Text;

namespace DijiWalk.Mobile.Services.Interfaces
{
    public interface IValidationRule<T>
    {
        string ValidationMessage { get; set; }
        bool Check(T value);
    }
}
