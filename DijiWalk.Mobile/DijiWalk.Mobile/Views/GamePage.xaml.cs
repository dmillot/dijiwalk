using DijiWalk.Common.Animations;
using DijiWalk.Mobile.ViewModels;
using DijiWalk.Mobile.ViewModels.ViewEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DijiWalk.Mobile.Views
{
    public partial class GamePage : ContentPage
    {

        public GamePage()
        {
            InitializeComponent();
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