using Flex.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DijiWalk.Common.Animations
{
    public static class  ButtonAnimation
    {

        /// <summary>
        /// Animation déclenché au click des buttons flexbutton
        /// </summary>
        /// <param name="sender">Button sur lequel le clique est effectué</param>
        /// <param name="down">Valeur qui détermine si c'est au clique ou au relachement du clique</param>
        public static void TouchedBtn(object sender, bool down)
        {
            Grid.SetColumnSpan(sender as FlexButton, down ? 2 : 1);
            Grid.SetRowSpan(sender as FlexButton, down ? 2 : 1);
            (sender as FlexButton).BackgroundColor = (Color)Application.Current.Resources[down ? "SecondaryDarkColor" : "SecondaryColor"];
            (sender as FlexButton).ForegroundColor = (Color)Application.Current.Resources[down ? "LightDarkColor" : "LightColor"];

        }

    }
}
