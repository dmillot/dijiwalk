using DijiWalk.Entities;
using DijiWalk.Mobile.ViewModels.ViewEntities;
using DijiWalk.Mobile.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace DijiWalk.Mobile.ViewModels
{
    public class GamePageViewModel : BindableBase, INavigationAware
    {
        #region Properties
        public INavigationService NavigationService { get; private set; }
        public DelegateCommand<object> NavigateToMainPage { get; set; }
        public DelegateCommand<object> NavigateToStepPage { get; set; }
        public DelegateCommand<object> NavigateToLoginPage { get; set; }

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
        #endregion

        public GamePageViewModel(INavigationService navigationService)
        {
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
            this.NavigateToMainPage = new DelegateCommand<object>(GoToMain);
            this.NavigateToStepPage = new DelegateCommand<object>(GoToStep);
            this.NavigateToLoginPage = new DelegateCommand<object>(GoToLogin);
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
        #endregion


        #region OnNavigatedFunction
        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {

        }
        #endregion

    }
}
