using DijiWalk.Common.Animations;
using DijiWalk.Mobile.ViewModels;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
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
        private async void TakePhoto_Clicked(object sender, EventArgs e)
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

                var vm = BindingContext as ValidationPageViewModel;
                vm.Photo = file;


            }
        }
        private async void Valider_Clicked(object sender, EventArgs e)
        {
            //await DisplayAlert("Veuillez patienter", "Merci de bien vouloir patienter le temps que nos système analyse l'authenticité de votre photo.", "Attendre");
            //BtnTakePhoto.IsEnabled = false;
            //BtnValider.IsEnabled = false;
            //BtnBack.IsEnabled = false;
            //BtnClose.IsEnabled = false;

            //Device.StartTimer(TimeSpan.FromSeconds(10), () =>
            //{
            //    BtnTakePhoto.IsEnabled = true;
            //    BtnValider.IsEnabled = true;
            //    BtnBack.IsEnabled = true;
            //    BtnClose.IsEnabled = true;
            //    return true;
            //});

        }

        private void BtnBack_Pressed(object sender, EventArgs e)
        {
            ShadowBtnBack.IsVisible = false;
            (sender as ImageButton).Margin = new Thickness(3, 3, 0, 0);
        }

        private void BtnBack_Released(object sender, EventArgs e)
        {
            ShadowBtnBack.IsVisible = true;
            (sender as ImageButton).Margin = new Thickness(0);
        }

        private void BtnClose_Pressed(object sender, EventArgs e)
        {
            ShadowBtnClose.IsVisible = false;
            (sender as ImageButton).Margin = new Thickness(3, 3, 0, 0);
        }

        private void BtnClose_Released(object sender, EventArgs e)
        {
            ShadowBtnClose.IsVisible = true;
            (sender as ImageButton).Margin = new Thickness(0);
        }

        private void BtnChat_TouchedDown(object sender, EventArgs e)
        {
            ButtonAnimation.TouchedBtn(sender, true); //Animation Down 
        }

        private void BtnChat_TouchedUp(object sender, EventArgs e)
        {
            ButtonAnimation.TouchedBtn(sender, false); //Animation UP 
        }

        private void BtnValider_TouchedDown(object sender, EventArgs e)
        {
            ButtonAnimation.TouchedBtn(sender, true); //Animation Down 
        }

        private void BtnValider_TouchedUp(object sender, EventArgs e)
        {
            ButtonAnimation.TouchedBtn(sender, false); //Animation UP 
        }

        private void BtnTakePhoto_TouchedDown(object sender, EventArgs e)
        {
            ButtonAnimation.TouchedBtn(sender, true); //Animation Down 
        }

        private void BtnTakePhoto_TouchedUp(object sender, EventArgs e)
        {
            ButtonAnimation.TouchedBtn(sender, false); //Animation UP 
        }
    }
}