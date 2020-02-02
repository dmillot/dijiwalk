using DijiWalk.Entities;
using DijiWalk.Mobile.ViewModels.ViewEntities;
using Prism.Mvvm;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DijiWalk.Mobile.Views.PopUp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StepPopUp : PopupPage, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private ViewStep _step;
        public ViewStep Step
        {
            get { return _step; }
            set {
                _step = value;
                OnPropertyChanged(nameof(Step)); 
            }
        }

        private Color _color;
        public Color Color
        {
            get { return _color; }
            set
            {
                _color = value;
                OnPropertyChanged(nameof(Color));
            }
        }

        private Color _colorDark;
        public Color ColorDark
        {
            get { return _colorDark; }
            set
            {
                _colorDark = value;
                OnPropertyChanged(nameof(ColorDark));
            }
        }

        public StepPopUp(ViewStep step)
        {
            InitializeComponent();
            BindingContext = this;
            Step = step;
            Color = step.Validation ? (Color)Application.Current.Resources["ValidationColor"] : (Color)Application.Current.Resources["ErrorColor"];
            ColorDark = step.Validation ? (Color)Application.Current.Resources["ValidationDarkColor"] : (Color)Application.Current.Resources["ErrorDarkColor"];
            StepName.TextColor = Color;
            PopUp.BorderColor = Color;
        }
        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}