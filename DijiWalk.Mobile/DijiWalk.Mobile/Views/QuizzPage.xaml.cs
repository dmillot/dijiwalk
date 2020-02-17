using DijiWalk.Entities;
using DijiWalk.Mobile.ViewModels;
using Plugin.InputKit.Shared.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



using Xamarin.Forms;
using Xamarin.Forms.Xaml;



namespace DijiWalk.Mobile.Views
{
    public partial class QuizzPage : ContentPage
    {
        public QuizzPage()
        {
            InitializeComponent();
            BasicQuestion();
        }
        public StackLayout BasicQuestion()
        {
            ////Création du stack layout englobant tout.
            StackLayout stackLayoutGlobal = new StackLayout();
            stackLayoutGlobal.HorizontalOptions = LayoutOptions.Center;
            stackLayoutGlobal.Margin = new Thickness(10, 0, 10, 10);
            Grid.SetRow(stackLayoutGlobal, 1);
            Grid.SetColumn(stackLayoutGlobal, 1);




            ////Création du label avec la question
            Label labelQuestion = new Label();
            labelQuestion.HorizontalTextAlignment = TextAlignment.Center;
            labelQuestion.TextColor = Color.White;
            labelQuestion.Text = "Combien de Climats sont inscrits sur la Liste du patrimoine mondial?";
            stackLayoutGlobal.Children.Add(labelQuestion);

            RadioButtonGroupView RadioButtonGroupeQuestion = new RadioButtonGroupView();
            //RadioButtonGroupeQuestion.SetBinding(RadioButtonGroupView.SelectedIndexProperty, nameof(trial.Answers));
            stackLayoutGlobal.Children.Add(RadioButtonGroupeQuestion);



            // foreach(Answer answer in trial.Answers)
            // {
            StackLayout stackLayoutResponse1 = new StackLayout();
            stackLayoutResponse1.BackgroundColor = Color.White;
            stackLayoutResponse1.Padding = new Thickness(5, 0);



            RadioButton radioButtonResponse1 = new RadioButton();
            radioButtonResponse1.TextColor = Color.Black;
            radioButtonResponse1.Text = "3 046";
            stackLayoutResponse1.Children.Add(radioButtonResponse1);



            StackLayout stackLayoutResponse2 = new StackLayout();
            stackLayoutResponse2.BackgroundColor = Color.White;
            stackLayoutResponse2.Padding = new Thickness(5, 0);



            RadioButton radioButtonResponse2 = new RadioButton();
            radioButtonResponse2.TextColor = Color.Black;
            radioButtonResponse2.Text = "2 500";
            stackLayoutResponse2.Children.Add(radioButtonResponse2);



            StackLayout stackLayoutResponse3 = new StackLayout();
            stackLayoutResponse3.BackgroundColor = Color.White;
            stackLayoutResponse3.Padding = new Thickness(5, 0);



            RadioButton radioButtonResponse3 = new RadioButton();
            radioButtonResponse3.TextColor = Color.Black;
            radioButtonResponse3.Text = " 1 247";
            stackLayoutResponse3.Children.Add(radioButtonResponse3);


            RadioButtonGroupeQuestion.Children.Add(stackLayoutResponse1);
            RadioButtonGroupeQuestion.Children.Add(stackLayoutResponse2);
            RadioButtonGroupeQuestion.Children.Add(stackLayoutResponse3);




            stackLayoutGlobal.Children.Add(RadioButtonGroupeQuestion);



            Label labelQuestion2 = new Label();
            labelQuestion2.HorizontalTextAlignment = TextAlignment.Center;
            labelQuestion2.TextColor = Color.White;
            labelQuestion2.Text = "Quelle est la définition pour le  métier de Tonnelier ?";
            stackLayoutGlobal.Children.Add(labelQuestion2);



            RadioButtonGroupView RadioButtonGroupeQuestion2 = new RadioButtonGroupView();
            //RadioButtonGroupeQuestion.SetBinding(RadioButtonGroupView.SelectedIndexProperty, nameof(trial.Answers));
            stackLayoutGlobal.Children.Add(RadioButtonGroupeQuestion);



            // foreach(Answer answer in trial.Answers)
            // {
            StackLayout stackLayoutResponse2_1 = new StackLayout();
            stackLayoutResponse2_1.BackgroundColor = Color.White;
            stackLayoutResponse2_1.Padding = new Thickness(5, 0);



            RadioButton radioButtonResponse2_1 = new RadioButton();
            radioButtonResponse2_1.TextColor = Color.Black;
            radioButtonResponse2_1.Text = "Je récolte les raisins mûrs destinés à la production de vin.";
            stackLayoutResponse2_1.Children.Add(radioButtonResponse2_1);



            StackLayout stackLayoutResponse2_2 = new StackLayout();
            stackLayoutResponse2_2.BackgroundColor = Color.White;
            stackLayoutResponse2_2.Padding = new Thickness(5, 0);



            RadioButton radioButtonResponse2_2 = new RadioButton();
            radioButtonResponse2_2.TextColor = Color.Black;
            radioButtonResponse2_2.Text = "J’achète des raisins, je les transforme en vin et le vends.";
            stackLayoutResponse2_2.Children.Add(radioButtonResponse2_2);



            StackLayout stackLayoutResponse2_3 = new StackLayout();
            stackLayoutResponse2_3.BackgroundColor = Color.White;
            stackLayoutResponse2_3.Padding = new Thickness(5, 0);



            RadioButton radioButtonResponse2_3 = new RadioButton();
            radioButtonResponse2_3.TextColor = Color.Black;
            radioButtonResponse2_3.Text = "Je cultive la vigne, produis du vin et le vends.";
            stackLayoutResponse2_3.Children.Add(radioButtonResponse2_3);



            StackLayout stackLayoutResponse2_4 = new StackLayout();
            stackLayoutResponse2_4.BackgroundColor = Color.White;
            stackLayoutResponse2_4.Padding = new Thickness(5, 0);



            RadioButton radioButtonResponse2_4 = new RadioButton();
            radioButtonResponse2_4.TextColor = Color.Black;
            radioButtonResponse2_4.Text = "Artisan spécialiste du bois, je fabrique et répare les tonneaux";
            stackLayoutResponse2_4.Children.Add(radioButtonResponse2_4);



            StackLayout stackLayoutResponse2_5 = new StackLayout();
            stackLayoutResponse2_5.BackgroundColor = Color.White;
            stackLayoutResponse2_5.Padding = new Thickness(5, 0);



            RadioButton radioButtonResponse2_5 = new RadioButton();
            radioButtonResponse2_5.TextColor = Color.Black;
            radioButtonResponse2_5.Text = "Technicien  et chimiste, je dirige la production du vin de la vigne à la bouteille.";
            stackLayoutResponse2_5.Children.Add(radioButtonResponse2_5);




            RadioButtonGroupeQuestion2.Children.Add(stackLayoutResponse2_1);
            RadioButtonGroupeQuestion2.Children.Add(stackLayoutResponse2_2);
            RadioButtonGroupeQuestion2.Children.Add(stackLayoutResponse2_3);
            RadioButtonGroupeQuestion2.Children.Add(stackLayoutResponse2_4);
            RadioButtonGroupeQuestion2.Children.Add(stackLayoutResponse2_5);



            stackLayoutGlobal.Children.Add(RadioButtonGroupeQuestion2);



            //  radioButtonResponse.SetBinding(RadioButton.TextProperty, nameof(trial.Libelle));
            //}



            GridQuizzPage.Children.Add(stackLayoutGlobal);



            return stackLayoutGlobal;
        }







    }
}