using Flex.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DijiWalk.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }


        private void BtnActualGame_TouchedDown(object sender, EventArgs e)
        {
            TouchedBtn(sender, true);
        }

        private void BtnActualGame_TouchedUp(object sender, EventArgs e)
        {
            TouchedBtn(sender, false);
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

        private void BtnClassement_TouchedUp(object sender, EventArgs e)
        {
            TouchedBtn(sender, false);

        }

        private void BtnClassement_TouchedDown(object sender, EventArgs e)
        {
            TouchedBtn(sender, true);
        }

        private void TouchedBtn(object sender, bool down)
        {
            Grid.SetColumnSpan(sender as FlexButton, down ? 2 : 1);
            Grid.SetRowSpan(sender as FlexButton, down ? 2 : 1);
            (sender as FlexButton).BackgroundColor = (Color)Application.Current.Resources[down ? "SecondaryDarkColor" : "SecondaryColor"];
            (sender as FlexButton).ForegroundColor = (Color)Application.Current.Resources[down ? "LightDarkColor" : "LightColor"];

        }

    }
}