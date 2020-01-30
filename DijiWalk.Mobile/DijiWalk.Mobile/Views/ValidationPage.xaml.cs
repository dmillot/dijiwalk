using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DijiWalk.Mobile.Views
{
    public partial class ValidationPage : ContentPage
    {
        public ValidationPage()
        {
            InitializeComponent();
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            if (CrossMedia.Current.IsTakePhotoSupported && !CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Message", "Photo Capture and Pick Not supported", "ok");
                return;
            }
            else
            {
                var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    Directory = "Images",
                    Name = "Photo_du_" + DateTime.Now + ".jpg"
                });

                if (file == null)
                    return;
                await DisplayAlert("Photo prise :", file.Path, "Ok");

                MyImage.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;

                });


            }
        }
        private async void Valider_Button_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Veuillez patienter", "Merci de bien vouloir patienter le temps que nos système analyse l'authenticité de votre photo.", "Attendre");
            TakePhoto.IsEnabled = false;
            Valider.IsEnabled = false;
            Retour.IsEnabled = false;

            Device.StartTimer(TimeSpan.FromSeconds(10), () =>
            {
                TakePhoto.IsEnabled = true;
                Valider.IsEnabled = true;
                Retour.IsEnabled = true;
                return true;
            });


        }

    }
}