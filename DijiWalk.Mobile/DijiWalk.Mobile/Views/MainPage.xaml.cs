using DijiWalk.Common.Animations;
using Flex.Controls;
using System;
using Xamarin.Forms;

namespace DijiWalk.Mobile.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void BtnActualGame_TouchedDown(object sender, EventArgs e)
        {
            ButtonAnimation.TouchedBtn(sender, true); //Animation Down 
        }

        private void BtnActualGame_TouchedUp(object sender, EventArgs e)
        {
            ButtonAnimation.TouchedBtn(sender, false); //Animation UP 
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
            ButtonAnimation.TouchedBtn(sender, false); //Animation UP 

        }

        private void BtnClassement_TouchedDown(object sender, EventArgs e)
        {
            ButtonAnimation.TouchedBtn(sender, true); //Animation Down 
        }

       
    }
}