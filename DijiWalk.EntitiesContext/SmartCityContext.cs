using System;
using DijiWalk.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DijiWalk.EntitiesContext
{
    public partial class SmartCityContext : DbContext
    {
        public SmartCityContext()
        {
        }

        public SmartCityContext(DbContextOptions<SmartCityContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Administrator> Administrators { get; set; }
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Mission> Missions { get; set; }
        public virtual DbSet<Organizer> Organizers { get; set; }
        public virtual DbSet<Play> Plays { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<Route> Routes { get; set; }
        public virtual DbSet<RouteStep> Routesteps { get; set; }
        public virtual DbSet<RouteTag> Routetags { get; set; }
        public virtual DbSet<StepTag> Steptags { get; set; }
        public virtual DbSet<Step> Steps { get; set; }
        public virtual DbSet<StepValidation> StepValidations { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<TeamAnswer> Teamanswers { get; set; }
        public virtual DbSet<TeamPlayer> Teamplayers { get; set; }
        public virtual DbSet<TeamRoute> Teamroutes { get; set; }
        public virtual DbSet<Transport> Transports { get; set; }
        public virtual DbSet<Trial> Trials { get; set; }
        public virtual DbSet<Entities.Type> Types { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrator>(entity =>
            {
                entity.ToTable("ADMINISTRATOR");

                entity.Property(e => e.Id).HasColumnName("Administrator_Id");

                entity.Property(e => e.Email)
                    .HasColumnName("Administrator_Email")
                    .HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .HasColumnName("Administrator_FirstName")
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .HasColumnName("Administrator_LastName")
                    .HasMaxLength(50);

                entity.Property(e => e.Login)
                    .HasColumnName("Administrator_Login")
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .HasColumnName("Administrator_Password")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Answer>(entity =>
            {
                entity.ToTable("ANSWER");

                entity.Property(e => e.Id).HasColumnName("Answer_Id");

                entity.Property(e => e.IdTrial).HasColumnName("Answer_fk_Trial_Id");

                entity.Property(e => e.Libelle)
                    .HasColumnName("Answer_Libelle")
                    .HasMaxLength(50);

                entity.Property(e => e.Picture).HasColumnName("Answer_Picture");

                entity.HasOne(d => d.Trial)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.IdTrial)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ANSWER_TRIAL");
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.ToTable("GAME");

                entity.Property(e => e.Id).HasColumnName("Game_Id");

                entity.Property(e => e.CreationDate)
                    .HasColumnName("Game_CreationDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.FinalScore).HasColumnName("Game_FinalScore");

                entity.Property(e => e.FinalTime)
                    .HasColumnName("Game_FinalTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdOrganizer).HasColumnName("Game_fk_Organizer_Id");

                entity.Property(e => e.IdRoute).HasColumnName("Game_fk_Route_Id");

                entity.Property(e => e.IdTransport).HasColumnName("Game_fk_Transport_Id");

                entity.HasOne(d => d.Organizer)
                    .WithMany(p => p.Games)
                    .HasForeignKey(d => d.IdOrganizer)
                    .HasConstraintName("FK_GAME_ORGANIZER");

                entity.HasOne(d => d.Transport)
                    .WithMany(p => p.Games)
                    .HasForeignKey(d => d.IdTransport)
                    .HasConstraintName("FK_GAME_TRANSPORT");


                entity.HasOne(d => d.Route)
                    .WithMany(p => p.Games)
                    .HasForeignKey(d => d.IdRoute)
                    .HasConstraintName("FK_GAME_ROUTE");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("MESSAGE");

                entity.Property(e => e.Id)
                    .HasColumnName("Message_Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Content)
                    .HasColumnName("Message_Content")
                    .HasMaxLength(50);

                entity.Property(e => e.Date).HasColumnName("Message_Date");

                entity.Property(e => e.IdOrganizer).HasColumnName("Message_fk_Organizer_Id");

                entity.Property(e => e.IdPlayer).HasColumnName("Message_fk_Player_Id");

                entity.Property(e => e.FromOrganiser).HasColumnName("Message_FromOrganiser");

                entity.HasOne(d => d.Organizer)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.IdOrganizer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MESSAGE_ORGANIZER");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.IdPlayer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MESSAGE_PLAYER");
            });

            modelBuilder.Entity<Mission>(entity =>
            {
                entity.ToTable("MISSION");

                entity.Property(e => e.Id).HasColumnName("Mission_Id");

                entity.Property(e => e.Description).HasColumnName("Mission_Description");

                entity.Property(e => e.IdStep).HasColumnName("Mission_fk_Step_Id");

                entity.Property(e => e.Name)
                    .HasColumnName("Mission_Name")
                    .HasMaxLength(50);

                entity.Property(e => e.Score).HasColumnName("Mission_Score");

                entity.Property(e => e.Time).HasColumnName("Mission_Time");

                entity.HasOne(d => d.Step)
                    .WithMany(p => p.Missions)
                    .HasForeignKey(d => d.IdStep)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_MISSION_STEP");
            });

            modelBuilder.Entity<Organizer>(entity =>
            {
                entity.ToTable("ORGANIZER");

                entity.Property(e => e.Id).HasColumnName("Organizer_Id");

                entity.Property(e => e.OrganizerEmail)
                    .HasColumnName("Organizer_Email")
                    .HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .HasColumnName("Organizer_FirstName")
                    .HasMaxLength(50);

                entity.Property(e => e.IdAdministrator).HasColumnName("Organizer_fk_Administrator_Id");

                entity.Property(e => e.LastName)
                    .HasColumnName("Organizer_LastName")
                    .HasMaxLength(50);

                entity.Property(e => e.Login)
                    .HasColumnName("Organizer_Login")
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .HasColumnName("Organizer_Password")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Administrator)
                    .WithMany(p => p.Organizers)
                    .HasForeignKey(d => d.IdAdministrator)
                    .HasConstraintName("FK_ORGANIZER_ADMINISTRATOR");
            });

            modelBuilder.Entity<Play>(entity =>
            {
                entity.HasKey(e => new { e.IdGame, e.IdTeam });

                entity.ToTable("PLAY");

                entity.Property(e => e.IdGame).HasColumnName("Play_fk_Game_Id");

                entity.Property(e => e.IdTeam).HasColumnName("Play_fk_Team_Id");

                entity.Property(e => e.EndDate)
                    .HasColumnName("Play_EndDate")
                    .HasColumnType("date");

                entity.Property(e => e.Score).HasColumnName("Play_Score");

                entity.Property(e => e.StartDate)
                    .HasColumnName("Play_StartDate")
                    .HasColumnType("date");

                entity.Property(e => e.Time).HasColumnName("Play_Time");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.Plays)
                    .HasForeignKey(d => d.IdGame)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PLAY_GAME");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.Plays)
                    .HasForeignKey(d => d.IdTeam)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PLAY_TEAM");
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.ToTable("PLAYER");

                entity.Property(e => e.Id).HasColumnName("Player_Id");

                entity.Property(e => e.Email)
                    .HasColumnName("Player_Email")
                    .HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .HasColumnName("Player_FirstName")
                    .HasMaxLength(50);

                entity.Property(e => e.IdOrganizer).HasColumnName("Player_fk_Organizer_Id");

                entity.Property(e => e.LastName)
                    .HasColumnName("Player_LastName")
                    .HasMaxLength(50);

                entity.Property(e => e.Login)
                    .HasColumnName("Player_Login")
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .HasColumnName("Player_Password")
                    .HasMaxLength(50);

                entity.Property(e => e.Picture).HasColumnName("Player_Picture");

                entity.HasOne(d => d.Organizer)
                    .WithMany(p => p.Players)
                    .HasForeignKey(d => d.IdOrganizer)
                    .HasConstraintName("FK_PLAYER_ORGANIZER");
            });

            modelBuilder.Entity<Route>(entity =>
            {
                entity.ToTable("ROUTE");

                entity.Property(e => e.Id).HasColumnName("Route_Id");

                entity.Property(e => e.Description)
                    .HasColumnName("Route_Description")
                    .HasMaxLength(255);

                entity.Property(e => e.Distance)
                    .HasColumnName("Route_Distance")
                    .HasMaxLength(50);

                entity.Property(e => e.IdOrganizer).HasColumnName("Route_fk_Organizer_Id");

                entity.Property(e => e.Handicap).HasColumnName("Route_Handicap");

                entity.Property(e => e.Name)
                    .HasColumnName("Route_Name")
                    .HasMaxLength(50);

                entity.Property(e => e.Time).HasColumnName("Route_Time");

                entity.HasOne(d => d.Organizer)
                    .WithMany(p => p.Routes)
                    .HasForeignKey(d => d.IdOrganizer)
                    .HasConstraintName("FK_ROUTE_ORGANIZER");


            });

            modelBuilder.Entity<RouteStep>(entity =>
            {
                entity.HasKey(e => new { e.IdRoute, e.IdStep });

                entity.ToTable("ROUTESTEP");

                entity.Property(e => e.IdRoute).HasColumnName("RouteStep_fk_Route_Id");

                entity.Property(e => e.IdStep).HasColumnName("RouteStep_fk_Step_Id");

                entity.Property(e => e.Order).HasColumnName("RouteStep_Order");

                entity.HasOne(d => d.Route)
                    .WithMany(p => p.RouteSteps)
                    .HasForeignKey(d => d.IdRoute)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ROUTESTEP_ROUTE");

                entity.HasOne(d => d.Step)
                    .WithMany(p => p.RouteSteps)
                    .HasForeignKey(d => d.IdStep)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ROUTESTEP_STEP");
            });

            modelBuilder.Entity<RouteTag>(entity =>
            {
                entity.HasKey(e => new { e.IdRoute, e.IdTag })
                    .HasName("PK_STEPTAG");

                entity.ToTable("ROUTETAG");

                entity.Property(e => e.IdRoute).HasColumnName("RouteTag_fk_Route_Id");

                entity.Property(e => e.IdTag).HasColumnName("RouteTag_fk_Tag_Id");

                entity.HasOne(d => d.Route)
                    .WithMany(p => p.RouteTags)
                    .HasForeignKey(d => d.IdRoute)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ROUTETAG_ROUTE");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.RouteTags)
                    .HasForeignKey(d => d.IdTag)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_STEPTAG_TAG");
            });

            modelBuilder.Entity<StepTag>(entity =>
            {
                entity.HasKey(e => new { e.IdStep, e.IdTag })
                    .HasName("PK_STEP_TAG");

                entity.ToTable("STEPTAG");

                entity.Property(e => e.IdStep).HasColumnName("StepTag_fk_Step_Id");

                entity.Property(e => e.IdTag).HasColumnName("StepTag_fk_Tag_Id");

                entity.HasOne(d => d.Step)
                    .WithMany(p => p.StepTags)
                    .HasForeignKey(d => d.IdStep)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_STEP_TAG_STEP");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.StepTags)
                    .HasForeignKey(d => d.IdTag)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_STEP_TAG_TAG");
            });

            modelBuilder.Entity<Step>(entity =>
            {
                entity.ToTable("STEP");

                entity.Property(e => e.Id).HasColumnName("Step_Id");

                entity.Property(e => e.CreationDate)
                    .HasColumnName("Step_CreationDate")
                    .HasColumnType("date");

                entity.Property(e => e.Description)
                    .HasColumnName("Step_Description")
                    .HasMaxLength(50);

                entity.Property(e => e.Lat).HasColumnName("Step_Lat");

                entity.Property(e => e.Lng).HasColumnName("Step_Lng");

                entity.Property(e => e.Name)
                    .HasColumnName("Step_Name")
                    .HasMaxLength(50);

                entity.Property(e => e.Validation)
                    .HasColumnName("Step_Validation")
                    .HasMaxLength(50);

            });

            modelBuilder.Entity<StepValidation>(entity =>
            {
                entity.ToTable("STEPVALIDATION");

                entity.Property(e => e.Id).HasColumnName("StepValidation_Id");

                entity.Property(e => e.IdStep).HasColumnName("StepValidation_fk_Step_Id");

                entity.Property(e => e.Description).HasColumnName("StepValidation_Description");

                entity.Property(e => e.Score).HasColumnName("StepValidation_Score");

                entity.HasOne(e => e.Step)
                   .WithMany(s => s.StepValidations)
                   .HasForeignKey(d => d.IdStep)
                   .HasConstraintName("FK_STEPVALIDATION_STEP");

            });


            modelBuilder.Entity<Tag>(entity =>
            {
                entity.ToTable("TAG");

                entity.Property(e => e.Id).HasColumnName("Tag_Id");

                entity.Property(e => e.Color)
                    .HasColumnName("Tag_Color")
                    .HasMaxLength(50);

                entity.Property(e => e.Description).HasColumnName("Tag_Description");

                entity.Property(e => e.Name)
                    .HasColumnName("Tag_Name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.ToTable("TEAM");

                entity.Property(e => e.Id).HasColumnName("Team_Id");

                entity.Property(e => e.IdCaptain).HasColumnName("Team_fk_Captain_Id");

                entity.Property(e => e.IdOrganizer).HasColumnName("Team_fk_Organizer_Id");

                entity.Property(e => e.Name)
                    .HasColumnName("Team_Name")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Captain)
                    .WithMany(p => p.Teams)
                    .HasForeignKey(d => d.IdCaptain)
                    .HasConstraintName("FK_TEAM_PLAYER");

                entity.HasOne(d => d.Organizer)
                    .WithMany(p => p.Teams)
                    .HasForeignKey(d => d.IdOrganizer)
                    .HasConstraintName("FK_TEAM_ORGANIZER");
            });

            modelBuilder.Entity<TeamAnswer>(entity =>
            {
                entity.ToTable("TEAMANSWER");

                entity.Property(e => e.Id)
                    .HasColumnName("TeamAnswer_Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Date)
                    .HasColumnName("TeamAnswer_Date")
                    .HasColumnType("date");

                entity.Property(e => e.IdAnswer).HasColumnName("TeamAnswer_fk_Answer_Id");

                entity.Property(e => e.IdGame).HasColumnName("TeamAnswer_fk_Game_Id");

                entity.Property(e => e.IdTeam).HasColumnName("TeamAnswer_fk_Team_Id");

                entity.Property(e => e.IdTrial).HasColumnName("TeamAnswer_fk_Trial_Id");

                entity.Property(e => e.Good).HasColumnName("TeamAnswer_Good");

                entity.Property(e => e.TextAnswer).HasColumnName("TeamAnswer_TextAnswer");

                entity.HasOne(d => d.Answer)
                    .WithMany(p => p.TeamAnswers)
                    .HasForeignKey(d => d.IdAnswer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TEAMANSWER_ANSWER");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.TeamAnswers)
                    .HasForeignKey(d => d.IdGame)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TEAMANSWER_GAME");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.TeamAnswers)
                    .HasForeignKey(d => d.IdTeam)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TEAMANSWER_TEAM");

                entity.HasOne(d => d.Trial)
                    .WithMany(p => p.TeamAnswers)
                    .HasForeignKey(d => d.IdTrial)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TEAMANSWER_TRIAL");
            });

            modelBuilder.Entity<TeamPlayer>(entity =>
            {
                entity.HasKey(e => new { e.IdTeam, e.IdPlayer })
                    .HasName("PK_TeamPlayer");

                entity.ToTable("TEAMPLAYER");

                entity.Property(e => e.IdTeam).HasColumnName("TeamPlayer_fk_Team_Id");

                entity.Property(e => e.IdPlayer).HasColumnName("TeamPlayer_fk_Player_Id");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.TeamPlayers)
                    .HasForeignKey(d => d.IdPlayer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TEAMPLAYER_PLAYER");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.TeamPlayers)
                    .HasForeignKey(d => d.IdTeam)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TEAMPLAYER_TEAM");
            });

            modelBuilder.Entity<TeamRoute>(entity =>
            {
                entity.HasKey(e => new { e.IdGame, e.IdTeam, e.StepOrder });

                entity.ToTable("TEAMROUTE");

                entity.Property(e => e.Id).HasColumnName("TeamRoute_Id");

                entity.Property(e => e.IdGame).HasColumnName("TeamRoute_fk_Game_Id");

                entity.Property(e => e.IdTeam).HasColumnName("TeamRoute_fk_Team_Id");

                entity.Property(e => e.StepOrder).HasColumnName("TeamRoute_StepOrder");

                entity.Property(e => e.IdRoute).HasColumnName("TeamRoute_fk_Route_Id");

                entity.Property(e => e.IdStep).HasColumnName("TeamRoute_fk_Step_Id");

                entity.Property(e => e.ValidationDate)
                    .HasColumnName("TeamRoute_ValidationDate")
                    .HasColumnType("date");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.TeamRoutes)
                    .HasForeignKey(d => d.IdGame)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TEAMROUTE_GAME");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.TeamRoutes)
                    .HasForeignKey(d => d.IdTeam)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TEAMROUTE_TEAM");

                entity.HasOne(d => d.RouteStep)
                    .WithMany(p => p.TeamRoutes)
                    .HasForeignKey(d => new { d.IdRoute, d.IdStep })
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TEAMROUTE_ROUTESTEP");
            });

            modelBuilder.Entity<Transport>(entity =>
            {
                entity.ToTable("TRANSPORT");

                entity.Property(e => e.Id).HasColumnName("Transport_Id");

                entity.Property(e => e.Description)
                    .HasColumnName("Transport_Description")
                    .HasMaxLength(50);

                entity.Property(e => e.Libelle)
                    .HasColumnName("Transport_Libelle")
                    .HasMaxLength(50);

            });

            modelBuilder.Entity<Trial>(entity =>
            {
                entity.ToTable("TRIAL");

                entity.Property(e => e.Id).HasColumnName("Trial_Id");

                entity.Property(e => e.IdCorrectAnswer).HasColumnName("Trial_fk_CorrectAnswer_Id");

                entity.Property(e => e.IdMission).HasColumnName("Trial_fk_Mission_Id");

                entity.Property(e => e.IdType).HasColumnName("Trial_fk_Type_Id");

                entity.Property(e => e.Libelle)
                    .HasColumnName("Trial_Libelle")
                    .HasMaxLength(50);

                entity.Property(e => e.Score).HasColumnName("Trial_Score");

                entity.HasOne(d => d.CorrectAnswer)
                    .WithMany(p => p.Trials)
                    .HasForeignKey(d => d.IdCorrectAnswer)
                    .HasConstraintName("FK_TRIAL_ANSWER");

                entity.HasOne(d => d.Mission)
                    .WithMany(p => p.Trials)
                    .HasForeignKey(d => d.IdMission)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TRIAL_MISSION");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Trials)
                    .HasForeignKey(d => d.IdType)
                    .HasConstraintName("FK_TRIAL_TYPE");
            });

            modelBuilder.Entity<Entities.Type>(entity =>
            {
                entity.ToTable("TYPE");

                entity.Property(e => e.Id).HasColumnName("Type_Id");

                entity.Property(e => e.Description).HasColumnName("Type_Description");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
