using DijiWalk.Common.Animations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DijiWalk.Mobile.Views
{
    public partial class EtapePage : ContentPage
    {
        public EtapePage()
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