using System;
using System.Collections.Generic;
using System.Text;

namespace DijiWalk.Common.Response
{
    public static class TranslateError
    {

        private static readonly Dictionary<string, string> foreignKey = new Dictionary<string, string>
        {
            {"ANSWER_TRIAL", "La réponse est liée à une question !"},
            {"GAME_ORGANIZER", "Le jeu est lié à un organisateur !"},
            {"GAME_ROUTE", "Le jeu est lié à un parcours !"},
            {"GAME_TRANSPORT", "Le jeu est lié à un jeu !"},
            {"MESSAGE_ORGANIZER", "Le message est lié à un organisateur !"},
            {"MESSAGE_PLAYER", "Le message est lié à un participant !"},
            {"MISSION_STEP", "La mission est liée à une étape !"},
            {"ORGANIZER_ADMINISTRATION", "L'organisateur est liée à un administrateur !"},
            {"PLAY_GAME", "Ce jeu contient des équipes !"},
            {"PLAY_TEAM", "Cette équipe a déjà participée à un jeu !"},
            {"PLAYER_ORGANIZER", "Le participant est lié à un organisateur !"},
            {"ROUTE_ORGANIZER", "Le parcours est lié à un organisateur !"},
            {"ROUTESTEP_ROUTE", "L'étape d'un parcours est liée à un parcours !"},
            {"ROUTESTEP_STEP", "L'étape d'un parcours est liée à une étape !"},
            {"ROUTETAG_ROUTE", "Le tag d'un parcours est lié à un parcours !"},
            {"STEPTAG_TAG", "Le tag d'une étape est lié à un tag !"},
            {"TEAM_ORGANIZER", "L'équipe est liée à un organisateur !"},
            {"TEAM_PLAYER", "L'équipe est liée à un participant !"},
            {"TEAMANSWER_ANSWER", "La réponse d'une équipe est liée à une réponse!"},
            {"TEAMANSWER_GAME", "La réponse d'une équipe est liée à jeu !"},
            {"TEAMANSWER_TEAM", "La réponse d'une équipe est liée à une équipe !"},
            {"TEAMANSWER_TRIAL", "La réponse d'une équipe est liée à question !"},
            {"TEAMPLAYER_PLAYER", "L'équipe du participant est liée à un participant !"},
            {"TEAMPLAYER_TEAM", "L'équipe du participant est liée à une équipe !"},
            {"TEAMROUTE_GAME", "L'équipe du parcours est liée à un jeu !"},
            {"TEAMROUTE_ROUTESTEP", "L'équipe du parcours est liée à une étape d'un parcours !"},
            {"TEAMROUTE_TEAM", "L'équipe est liée à un jeu !"},
            {"TRIAL_ANSWER", "La question est liée à une réponse !"},
            {"TRIAL_MISSION", "La question est liée à une mission !"},
            {"TRIAL_TYPE", "La question est liée à un type d'un question !"}
        };

        /// <summary>
        /// Permet de traduire l'erreur en message d'erreur
        /// </summary>
        /// <param name="error">Message de l'erreur</param>
        /// <returns>ApiReponse</returns>
        public static ApiResponse Convert(Exception error)
        {
            switch (error.InnerException != null ? error.InnerException.Message : error.Message)
            {
                case "Object reference not set to an instance of an object.":
                    return new ApiResponse { Status = ApiStatus.NotSetInstance, Message = "L'élément est introuvable !" };

                case string a when a.Contains("est en conflit avec la contrainte"):
                    return new ApiResponse { Status = ApiStatus.Conflict, Message = foreignKey[a.Substring(a.IndexOf("REFERENCE \"FK_") + 14, a.Substring(a.IndexOf("REFERENCE \"FK_") + 14).IndexOf("\""))] };
                
                case string a when a.Contains("statement conflicted with the REFERENCE constraint"):
                    return new ApiResponse { Status = ApiStatus.Conflict, Message = foreignKey[a.Substring(a.IndexOf("REFERENCE constraint \"FK_") + 25, a.Substring(a.IndexOf("REFERENCE constraint \"FK_") + 25).IndexOf("\""))] };

                case string b when b.Contains("Value cannot be null"):
                    return new ApiResponse { Status = ApiStatus.NullValue, Message = "L'élément que vous voulez supprimer est introuvable !" };
                
                default:
                    return new ApiResponse { Status = ApiStatus.UnknownError, Message = "Erreur inconnu" };
            }
        }
    }
}
