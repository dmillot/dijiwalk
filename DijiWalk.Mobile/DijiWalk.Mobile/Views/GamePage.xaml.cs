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

        private void BtnActualStep_TouchedDown(object sender, EventArgs e)
        {
            ButtonAnimation.TouchedBtn(sender, true); //Animation Down 
        }

        private void BtnActualStep_TouchedUp(object sender, EventArgs e)
        {
            
            ButtonAnimation.TouchedBtn(sender, false); //Animation UP 
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

        private void ListViewGroupes_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (((sender as ListView).SelectedItem as ViewTeam).IsSelected)
                ((sender as ListView).SelectedItem as ViewTeam).IsSelected = false;
            else
            {
                foreach (var groupe in ((GamePageViewModel)BindingContext).Groupes)
                {
                    groupe.IsSelected = false;
                }
                ((sender as ListView).SelectedItem as ViewTeam).IsSelected = true;
            }
        }

      
    }
}