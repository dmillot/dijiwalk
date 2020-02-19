using DijiWalk.Entities;
using DijiWalk.Mobile.Resources.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DijiWalk.Mobile.ViewModels.ViewEntities
{
    public class ViewStep
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Boolean Validation { get; set; }
        public DateTime? CreationDate { get; set; }
        public Dictionary<string,string> ColorValidation { get; set; }
        public bool NotFirst { get; set; }
        public bool NotLast { get; set; }

        public ICollection<Clue> Clues { get; set; }

        public ViewStep()
        {
            Validation = false;
            ColorValidation = new Dictionary<string, string>() { { "fill=\"\"", GetRGBFromColor.GetRGBFill((Color)Application.Current.Resources["ErrorColor"]) } };
            NotFirst = true;
            NotLast = true;
        }

    }
}
