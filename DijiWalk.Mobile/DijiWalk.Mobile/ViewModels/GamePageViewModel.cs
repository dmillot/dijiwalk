using DijiWalk.Entities;
using DijiWalk.Mobile.Resources.Utils;
using DijiWalk.Mobile.Services;
using DijiWalk.Mobile.ViewModels.ViewEntities;
using DijiWalk.Mobile.Views;
using DijiWalk.Mobile.Views.PopUp;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Navigation.Xaml;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace DijiWalk.Mobile.ViewModels
{
    public class GamePageViewModel : BindableBase, INavigationAware
    {
        #region Properties
        public INavigationService NavigationService { get; private set; }

        private Game _actualGame;
        public Game ActualGame
        {
            get { return _actualGame; }
            set
            {
                SetProperty(ref _actualGame, value);
            }
        }

        private readonly PlayerService _playerService;
        public DelegateCommand<object> NavigateToMainPage { get; set; }
        public DelegateCommand<object> NavigateToStepPage { get; set; }
        public DelegateCommand<object> NavigateToLoginPage { get; set; }
        public DelegateCommand<object> PopUpStep { get; set; }

        public Dictionary<string, string>  ColorValidation = new Dictionary<string, string>() { { "fill=\"\"", GetRGBFromColor.GetRGBFill((Color) Application.Current.Resources["ValidationColor"]) }};


        private ObservableCollection<ViewTeam> _groupes = new ObservableCollection<ViewTeam>();
        public ObservableCollection<ViewTeam> Groupes
        {
            get { return _groupes; }
            set { SetProperty(ref _groupes, value); }
        }

        private ObservableCollection<ViewPlayer> _participants = new ObservableCollection<ViewPlayer>();
        public ObservableCollection<ViewPlayer> Participants
        {
            get { return _participants; }
            set { SetProperty(ref _participants, value); }
        }

        private ViewTeam _teamSelected;
        public ViewTeam TeamSelected
        {
            get { return _teamSelected; }
            set { SetProperty(ref _teamSelected, value); }
        }

        private ObservableCollection<ViewStep> _steps = new ObservableCollection<ViewStep>();
        public ObservableCollection<ViewStep> Steps
        {
            get { return _steps; }
            set { SetProperty(ref _steps, value); }
        }
        #endregion

        public GamePageViewModel(PlayerService playerService, INavigationService navigationService)
        {
            _playerService = playerService;
            NavigationService = navigationService;
            Random rnd = new Random();
            List<string> namePrenoms = new List<string>
            {
                "Nathan GAGNIARRE",
                "Damien MILLOT",
                "Clément HUGON",
                "Éric SCHALKE",
                "Kim DOLE",
                "Valentin VIROT",
                "Martin BALME",
                "Mathis ESPANEL",
                "Raphaël DELCROIX",
                "Loïc POPULIER",
                "Baptiste ROUILLON",
                "Geoffrey LIOTE",
                "Charlélie BOURDREUX",
                "Benjamin SORRIAUX",
                "Charly DARCEAUX",
                "Nicolas CIVADE"
            };
            int idPlayer = 1;
            foreach (var namePrenom in namePrenoms)
            {
                string[] names = namePrenom.Split(Convert.ToChar(" "));
                Participants.Add(new ViewPlayer
                {
                    Id = idPlayer,
                    FirstName = names[0],
                    LastName = names[1],
                    Picture = String.Concat("https://i.picsum.photos/id/", idPlayer, "/5616/3744.jpg")
                });
                idPlayer++;
            }

            Groupes = new ObservableCollection<ViewTeam>();
            for (int i = 0; i < 4; i++)
            {
                var team = new ViewTeam { Id = i + 1, Name = String.Concat("Groupe ", i + 1) };
                List<ViewPlayer> participants = new List<ViewPlayer>(Participants).GetRange(i * 4, 4);
                team.TeamPlayers = new List<ViewTeamPlayer>();
                foreach (var participant in participants)
                {

                    team.TeamPlayers.Add(new ViewTeamPlayer
                    {
                        IdPlayer = participant.Id,
                        IdTeam = team.Id,
                        Player = participant,
                        Team = team,
                        IsCapitaine = false
                    });
                }
                ViewTeamPlayer capitaine = team.TeamPlayers.ElementAt(rnd.Next(0, 3));
                capitaine.IsCapitaine = true;
                team.IdCaptain = capitaine.Player.Id;
                team.Captain = capitaine.Player;
                Groupes.Add(team);
            }

            Dictionary<string, string> validationColor = new Dictionary<string, string>() { { "fill=\"\"", GetRGBFromColor.GetRGBFill((Color)Application.Current.Resources["ValidationColor"]) } };
            Steps = new ObservableCollection<ViewStep>
            {
                new ViewStep {Id = 1, Name = "Étape 1", Description="Étape 1 description !", CreationDate = DateTime.Today, ColorValidation = validationColor, NotFirst = false, Validation = true },
                new ViewStep {Id = 2, Name = "Étape 2", Description="Étape 2 description !", CreationDate = DateTime.Today, ColorValidation = validationColor, Validation = true },
                new ViewStep {Id = 3, Name = "Étape 3", Description="Étape 3 description !", CreationDate = DateTime.Today},
                new ViewStep {Id = 4, Name = "Étape 4", Description="Étape 4 description !", CreationDate = DateTime.Today},
                new ViewStep {Id = 5, Name = "Étape 5", Description="Étape 5 description !", CreationDate = DateTime.Today, NotLast = false}
            };

            this.NavigateToMainPage = new DelegateCommand<object>(GoToMain);
            this.NavigateToStepPage = new DelegateCommand<object>(GoToStep);
            this.NavigateToLoginPage = new DelegateCommand<object>(GoToLogin);
            this.PopUpStep = new DelegateCommand<object>(GotToPopUpStep);
        }

        #region NavigationFunction
        /// <summary>
        /// Fonction appelée quand l'utilisateur veut retourner sur la page d'accueil.
        /// </summary>
        /// <param name="parameters">Command parameter</param>
        void GoToMain(object parameters)
        {
            this.NavigationService.NavigateAsync(nameof(MainPage), null);
        }

        /// <summary>
        /// Fonction appelée quand l'utilisateur veut aller sur le détails de l'étape actuelle.
        /// </summary>
        /// <param name="parameters">Command parameter</param>
        void GoToStep(object parameters)
        {
            this.NavigationService.NavigateAsync(nameof(EtapePage), null);
        }

        /// <summary>
        /// Fonction appelée quand l'utilisateur veut se déconnecter.
        /// </summary>
        /// <param name="parameters">Command parameter</param>
        void GoToLogin(object parameters)
        {
            this.NavigationService.NavigateAsync(nameof(LoginPage), null);
        }

        /// <summary>
        /// Fonction appelée quand l'utilisateur veut plus de détails sur une étape déjà réalisé.
        /// </summary>
        /// <param name="parameters">Command parameter</param>
        void GotToPopUpStep(object parameters)
        {
            var popup = new StepPopUp((ViewStep)parameters);
            PopupNavigation.Instance.PushAsync(popup);
        }
        #endregion


        #region OnNavigatedFunction
        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public async void OnNavigatedTo(INavigationParameters parameters)
        {
            Dictionary<string, string> greenColor = new Dictionary<string, string>() { { "fill=\"\"", GetRGBFromColor.GetRGBFill((Color)Application.Current.Resources["ValidationColor"]) } };
            Dictionary<string, string> redColor = new Dictionary<string, string>() { { "fill=\"\"", GetRGBFromColor.GetRGBFill((Color)Application.Current.Resources["ErrorColor"]) } };

            var player = await _playerService.GetPlayerById(App.User.Id);
            this.ActualGame = await _playerService.GetActualGame(App.User.Id);

            //var idTeamPlayer = ActualGame.Plays.Where(p => p.Team.TeamPlayers.);

            //var test = this.ActualGame.Plays.Select(play => play.Team.TeamPlayers.Where(tp => tp.IdPlayer == player.Id).Select(x => x.IdTeam));
            ////var idTeam = player.Teams.Where(t => t.TeamRoutes.Select(tr => tr.IdGame).ToList().Contains(ActualGame.Id)).FirstOrDefault().Id;

            //this.Steps = new ObservableCollection<ViewStep>(ActualGame.TeamRoutes.Where(tr => tr.Team.Id == idTeamPlayer).Select(tr => {
            //    return new ViewStep { Id = tr.RouteStep.Step.Id, Name = tr.RouteStep.Step.Name, Description = tr.RouteStep.Step.Description, CreationDate = tr.AskValidationDate, ColorValidation = (bool)tr.Validate ? greenColor : redColor, NotFirst = tr.StepOrder == 1 ? false : true, Validation = (bool)tr.Validate, NotLast = ActualGame.TeamRoutes.Where(trs => trs.Team.Id == idTeamPlayer).Count() == tr.StepOrder ? false : true };
            //}).ToList());
        }
        #endregion
    }
}
