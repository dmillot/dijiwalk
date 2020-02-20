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

        private void BtnClose_Released(object sender, EventArgs e)
        {
            ShadowBtnClose.IsVisible = true;
            (sender as ImageButton).Margin = new Thickness(0);
        }

        private void BtnClose_Pressed(object sender, EventArgs e)
        {
            ShadowBtnClose.IsVisible = false;
            (sender as ImageButton).Margin = new Thickness(3, 3, 0, 0);
        }


    }
}