using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace Core
{
    /// <summary>
    /// Façade de l'assemply "Core", cette classe contrôle le client, et toutes les requêtes passe par lui.
    /// </summary>
    public partial class Manager
    {
        private static bool EnExécution = false;
        private static DiscordSocketClient Client { get; set; }
        private CommandService CommandeManager { get; set; }
        private ISérialiseur Sérialiseur { get; set; }
        private static IEnumerable<IServeur> Serveurs { get; set; }
        private bool ClientEstPrêt { get; set; }
        private bool PurgeFinie { get; set; }
        private bool VérificationFinie { get; set; }

        /// <summary>
        /// Initialise le manager avec un mode persistance qui lui est inconnu.
        /// </summary>
        /// <param name="sérialiseur">Mode de persistance à utiliser</param>
        public Manager(ISérialiseur sérialiseur)
        {
            //Vérification qu'aucune tâche MainAsync() ne soit commencée.
            if(EnExécution)
            {
                throw new Exception("Vous ne pouvez pas initialiser un autre manager une fois la tâche MainAsync() lancée.");
            }

            //Configuration des paramètres.
            Sérialiseur = sérialiseur;
            Serveurs = Sérialiseur.ChargerServeurs();
            Utils.Serveurs = Serveurs;
            ClientEstPrêt = false;
            PurgeFinie = false;
            VérificationFinie = false;
            EnvoyerLog(LogSeverity.Verbose, "System", $"Etape 0 --- Manager instancié. [ OK ]");
        }

        /// <summary>
        /// Exécutée une seule fois, au démarrage de l'application.
        /// </summary>
        public async Task MainAsync()
        {
            if(!EnExécution)
            {
                EnExécution = true;

                // Définir le client et le commande manager.
                Task discordBase = Task.Run(() =>
                {
                    Client = new DiscordSocketClient(new DiscordSocketConfig
                    {
                        LogLevel = LogSeverity.Debug // Quantité d'informations reçues avec les sockets, ne pas toucher.
                    });

                    CommandeManager = new CommandService(new CommandServiceConfig
                    {
                        CaseSensitiveCommands = true,   // EqualsIgnoreCase pour les commandes.
                        DefaultRunMode = RunMode.Async, // Mode d'exécution.
                        LogLevel = LogSeverity.Debug    // Quantité d'informations reçues avec les commandes.
                    });
                });
                discordBase.Wait();
                EnvoyerLog(LogSeverity.Verbose, "System", $"Etape 1 --- Client et CommandeManager instanciés. [ OK ]");

                // Abonner le Manager aux événements du clients.
                Task events = Task.Run(() =>
                {
                    Client.MessageReceived += OnMessageReceived;
                    Client.Ready += OnClientReady;
                    Client.Log += OnClientLog;
                    Client.RoleDeleted += OnRangDeleted;
                    Client.ChannelDestroyed += OnChannelDestroyed;
                    Client.JoinedGuild += OnGuildJoin;
                    Client.LeftGuild += OnGuildLeft;
                    CommandeManager.AddModulesAsync(Assembly.Load(Configuration.coreAssembly), null);
                });
                events.Wait();
                EnvoyerLog(LogSeverity.Verbose, "System", $"Etape 2 --- Manager abonné aux événements du client. [ OK ]");

                // Mettre en route le client.
                Task synchronisation = Task.Run(() =>
                {
                    Client.LoginAsync(TokenType.Bot, Sérialiseur.ChargerToken());
                    Client.StartAsync();
                });
                synchronisation.Wait();
                while (!ClientEstPrêt);
                EnvoyerLog(LogSeverity.Verbose, "System", $"Etape 3 --- Client authentifié et connecté. [ OK ]");


                // Faire des vérifications : supprimer les IServeurs inutiles.
                Task purge = Task.Run(() =>
                {
                    PurgeIServeur();
                });
                purge.Wait();
                while (!PurgeFinie);
                EnvoyerLog(LogSeverity.Verbose, "System", $"Etape 4 --- Purge des IServeurs inutiles terminée. [ OK ]");

                // Faire des vérifications : ajouter les IServeurs manquants.
                Task vérification = Task.Run(() =>
                {
                    VérifierGuilds();
                });
                vérification.Wait();
                while (!VérificationFinie);
                EnvoyerLog(LogSeverity.Verbose, "System", $"Etape 5 --- Vérification des guilds terminée. [ OK ]");

                // Une fois que tout est fini, laisser la Task en suspens.
                EnvoyerLog(LogSeverity.Info, "System", $"Démarrage global du bot : [ OK ]");
                await Task.Delay(-1);
            }
            else
            {
                throw new Exception("Vous ne pouvez pas lancer la tâche MainAsync() plus d'une fois par exécution.");
            }
        }

        /// <summary>
        /// Ecrit une log sur la console.
        /// </summary>
        /// <param name="severity">Sévérité du message.</param>
        /// <param name="source">Origine du message.</param>
        /// <param name="message">Contenu du message.</param>
        private void EnvoyerLog(LogSeverity severity, string source, string message)
        {
            switch (severity)
            {
                case LogSeverity.Critical:
                    BackgroundColor = ConsoleColor.DarkRed;
                    ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LogSeverity.Error:
                    BackgroundColor = ConsoleColor.Yellow;
                    ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case LogSeverity.Warning:
                    BackgroundColor = ConsoleColor.Yellow;
                    ForegroundColor = ConsoleColor.Black;
                    break;
                case LogSeverity.Info:
                    ForegroundColor = ConsoleColor.White;
                    break;
                case LogSeverity.Verbose:
                    ForegroundColor = ConsoleColor.Gray;
                    break;
                case LogSeverity.Debug:
                    ForegroundColor = ConsoleColor.DarkGray;
                    break;
                default:
                    break;
            }
            WriteLine($"[{severity} : {DateTime.Now} : {source}] {message}");
            ResetColor();
        }

        /// <summary>
        /// Exécutée à chaque fois que le client transmet une log.
        /// </summary>
        /// <param name="message">Log en question.</param>
        private async Task OnClientLog(LogMessage message)
        {
            // Ecriture de la log.
            EnvoyerLog(message.Severity, message.Source, message.Message);
            if (message.Source == "Gateway" && message.Message == "Ready")
            {
                ClientEstPrêt = true;
            }
        }

        /// <summary>
        /// Exécutée une fois que le client a réussi à se connecter à discord.
        /// </summary>
        private async Task OnClientReady()
        {
            // Modifications des paramètres visuels lors du lancement.
            Utils.Client = Client;
            await Client.SetGameAsync(Configuration.Activité.Item1, Configuration.Activité.Item2, Configuration.Activité.Item3);
            await Client.SetStatusAsync(UserStatus.DoNotDisturb);
        }

        /// <summary>
        /// Exécutée à chaque fois que le client reçoit un message.
        /// </summary>
        /// <param name="socketMessage">Message reçu par le client.</param>
        private async Task OnMessageReceived(SocketMessage socketMessage)
        {
            // On vérifie que le message ne soit pas un message de bienvenue.
            if(socketMessage.Author.Id == Client.CurrentUser.Id)
            {
                return;
            }

            // Initialisation des différents paramètres.
            SocketUserMessage message = socketMessage as SocketUserMessage;
            SocketCommandContext contexte = new SocketCommandContext(Client, message);
            int argPos = 0;

            // Vérification de la validité du message, et de son auteur.
            if (contexte.Message == null || contexte.Message.Content == "" || contexte.User.IsBot)
            {
                return;
            }

            // Vérification de la conformité du message par rapport au client.
            bool hasStringPrefix = false;
            foreach(string préfix in Configuration.Préfix)
            {
                if(message.HasStringPrefix(préfix, ref argPos))
                {
                    hasStringPrefix = true;
                    break;
                }
            }
            if (!(hasStringPrefix || message.HasMentionPrefix(Client.CurrentUser, ref argPos)))
            {
                return;
            }

            // Exécution de la commande, et vérification du résultat.
            IResult résultat = await CommandeManager.ExecuteAsync(contexte, argPos, null);
            if (!résultat.IsSuccess)
            {
                EnvoyerLog(LogSeverity.Warning, "Commands", $"Quelque chose a échoué lors de la commande : {contexte.Message.Content} | Erreur : {résultat.ErrorReason}");
            }
        }

        /// <summary>
        /// Exécutée à chaque fois que le client détecte qu'un rang vient d'être supprimé.
        /// </summary>
        /// <param name="rang">Rang qui vient d'être supprimé.</param>
        private async Task OnRangDeleted(SocketRole rang)
        {
            await Task.Run(() =>
            {
                // Vérification si c'est un IRang dans un des IServeurs.
                foreach (IServeur serveur in Serveurs)
                {
                    if (serveur.GuildID == rang.Guild.Id)
                    {
                        IRang rangASupprimer = null;

                        // Vérification avec les IRangs administrateurs.
                        foreach (IRang rangServeur in serveur.RangsAdministrateur)
                        {
                            if (rang.Id == rangServeur.RôleID)
                            {
                                rangASupprimer = rangServeur;
                                break;
                            }
                        }
                        if (rangASupprimer != null)
                        {
                            (serveur.RangsAdministrateur as List<IRang>).Remove(rangASupprimer);
                            EnvoyerLog(LogSeverity.Info, "System", $"IRang administrateur supprimé (OnRangDeleted). GuildID : {serveur.GuildID}, RôleID : {rangASupprimer.RôleID}.");
                            return;
                        }

                        // Vérification avec les IRangs staff.
                        foreach (IRang rangServeur in serveur.RangsStaff)
                        {
                            if (rang.Id == rangServeur.RôleID)
                            {
                                rangASupprimer = rangServeur;
                                break;
                            }
                        }
                        if (rangASupprimer != null)
                        {
                            (serveur.RangsStaff as List<IRang>).Remove(rangASupprimer);
                            EnvoyerLog(LogSeverity.Info, "System", $"IRang staff supprimé (OnRangDeleted). GuildID : {serveur.GuildID}, RôleID : {rangASupprimer.RôleID}.");
                        }
                        return;
                    }
                }
            });
        }

        /// <summary>
        /// Exécutée à chaque fois que le client détecte qu'un channel est supprimé.
        /// </summary>
        /// <param name="channel">Channel qui vient d'être supprimé.</param>
        private async Task OnChannelDestroyed(SocketChannel channel)
        {
            await Task.Run(() =>
            {
                // Vérification si c'est une IPartie.
                IServeur serveurResponsable = null;
                IPartie partieASupprimer = null;
                foreach (IServeur serveur in Serveurs)
                {
                    serveur.VérifierPartieVocale("OnChannelDestroyed");
                    foreach (IPartie partie in serveur.Parties)
                    {
                        if (partie.Channel == channel.Id)
                        {
                            partieASupprimer = partie;
                            break;
                        }
                    }
                    if (partieASupprimer != null)
                    {
                        serveurResponsable = serveur;
                        break;
                    }
                }
                if (serveurResponsable != null)
                {
                    (serveurResponsable.Parties as List<IPartie>).Remove(partieASupprimer);
                    EnvoyerLog(LogSeverity.Info, "System", $"IPartie supprimée (PurgeIServeur). GuildID : {serveurResponsable.GuildID}, ChannelID : {partieASupprimer.Channel}.");
                }
            });
        }

        /// <summary>
        /// Exécutée à chaque fois que le client rejoint une guild.
        /// </summary>
        /// <param name="guild">Guild que le client a rejoint.</param>
        private async Task OnGuildJoin(SocketGuild guild)
        {
            // Ajout d'un nouveau IServeur.
            await Task.Run(() => 
            {
                foreach (IServeur serveur in Serveurs)
                {
                    if (serveur.GuildID == guild.Id)
                    {
                        return;
                    }
                }
            (Serveurs as List<IServeur>).Add(Factory.Instance.NouveauIServeur(guild.Id));
            EnvoyerLog(LogSeverity.Info, "System", $"IServeur créé (OnGuildJoin). GuildID : {guild.Id}.");
            });
        }

        /// <summary>
        /// Exécutée à chaque fois que le client rejoint une guild.
        /// </summary>
        /// <param name="guild">Guild que le client a quitté.</param>
        private async Task OnGuildLeft(SocketGuild guild)
        {
            // Vérification si c'est un IServeur.
            await Task.Run(() =>
            {
                foreach (IServeur serveur in Serveurs)
                {
                    if (serveur.GuildID == guild.Id)
                    {
                        (Serveurs as List<IServeur>).Remove(serveur);
                        EnvoyerLog(LogSeverity.Info, "System", $"IServeur supprimé (OnGuildLeft). GuildID : {guild.Id}.");
                        break;
                    }
                }
            });
        }

        /// <summary>
        /// Exécutée une fois le démarrage du bot fini. Permet de supprimer les données inutiles.
        /// </summary>
        private async Task PurgeIServeur()
        {

            // Initialisation.
            List<IServeur> serveursASupprimer = new List<IServeur>();

            // On récupère toutes les ID de guilds du Client.
            List<ulong> guildIDs = new List<ulong>();
            foreach (SocketGuild guild in Client.Guilds)
            {
                guildIDs.Add(guild.Id);
            }

            // Pour chaque IServeur on vérifie que la guild associée existe bien.
            foreach (IServeur serveur in Serveurs)
            {
                if (!guildIDs.Contains(serveur.GuildID))
                {
                    serveursASupprimer.Add(serveur);
                }
            }

            // Si ce n'est pas le cas, on le supprime.
            foreach (IServeur serveur in serveursASupprimer)
            {
                (Serveurs as List<IServeur>).Remove(serveur);
                EnvoyerLog(LogSeverity.Info, "System", $"IServeur supprimé (PurgeIServeur). GuildID : {serveur.GuildID}.");
            }
            

            //--------------------------------------------------------------------------

            // On va ensuite vérifier que chaque IRang et chaque IPartie de chaque IServeur sont valides.
            foreach (IServeur serveur in Serveurs)
            {
                await Task.Run(() =>
                {
                    SocketGuild guild = Client.GetGuild(serveur.GuildID);

                    // Initialisation pour les IRangs.
                    List<IRang> rangsAdministrateurASupprimer = new List<IRang>();
                    List<IRang> rangsStaffASupprimer = new List<IRang>();

                    // On récupère toutes les Id des rôles dans la guild.
                    List<ulong> rôleIDs = new List<ulong>();
                    foreach (SocketRole rôle in guild.Roles)
                    {
                        rôleIDs.Add(rôle.Id);
                    }

                    // Pour chaque IRang Administrateur on vérifie que le rôle associé existe bien.
                    foreach (IRang rang in serveur.RangsAdministrateur)
                    {
                        if (!rôleIDs.Contains(rang.RôleID))
                        {
                            rangsAdministrateurASupprimer.Add(rang);
                        }
                    }

                    // Pour chaque IRang Staff on vérifie que le rôle associé existe bien.
                    foreach (IRang rang in serveur.RangsStaff)
                    {
                        if (!rôleIDs.Contains(rang.RôleID))
                        {
                            rangsStaffASupprimer.Add(rang);
                        }
                    }

                    // Si ce n'est pas le cas pour les deux blocs précédents, on supprime.
                    foreach (IRang rang in rangsAdministrateurASupprimer)
                    {
                        (serveur.RangsAdministrateur as List<IRang>).Remove(rang);
                        EnvoyerLog(LogSeverity.Info, "System", $" IRang administrateur supprimé (PurgeIServeur). GuildID : {serveur.GuildID}, RôleID : {rang.RôleID}.");
                    }
                    foreach (IRang rang in rangsStaffASupprimer)
                    {
                        (serveur.RangsStaff as List<IRang>).Remove(rang);
                        EnvoyerLog(LogSeverity.Info, "System", $" IRang staff supprimé (PurgeIServeur). GuildID : {serveur.GuildID}, RôleID : {rang.RôleID}.");
                    }

                    //--------------------------------------------------------------------------

                    // Vérification de la partie vocale.
                    serveur.VérifierPartieVocale("PurgeIServeur");

                    // Initialisation pour les IParties.
                    List<IPartie> partiesASupprimer = new List<IPartie>();

                    // On récupère toutes les Id des channels
                    List<ulong> channels = new List<ulong>();
                    foreach (SocketGuildChannel channel in guild.Channels)
                    {
                        channels.Add(channel.Id);
                    }

                    // On vérifie ensuite que chaque IPartie possède un channel qui existe bien.
                    foreach (IPartie partie in serveur.Parties)
                    {
                        if (!channels.Contains(partie.Channel))
                        {
                            partiesASupprimer.Add(partie);
                        }
                    }

                    // Si ce n'est pas le cas, on la supprime.
                    foreach (IPartie partie in partiesASupprimer)
                    {
                        EnvoyerLog(LogSeverity.Info, "System", $" IPartie supprimée (PurgeIServeur). GuildID : {serveur.GuildID}, ChannelID : {partie.Channel}.");
                        (serveur.Parties as List<IPartie>).Remove(partie);
                    }
                });
                PurgeFinie = true;
            }
        }

        /// <summary>
        /// Exécutée une fois le démarrage du bot fini. Permet d'ajouter des serveurs manquants.
        /// </summary>
        private async Task VérifierGuilds()
        {
            await Task.Run(() =>
            {
                // Initialisation.
                List<ulong> guilds = new List<ulong>();

                // On récupère l'id de chaque guild.
                foreach (SocketGuild guild in Client.Guilds)
                {
                    guilds.Add(guild.Id);
                }

                // On élimine ensuite les serveurs déjà présents.
                foreach (IServeur serveur in Serveurs)
                {
                    guilds.Remove(serveur.GuildID);
                }

                // On rajoute ensuite les serveurs manquants.
                foreach (ulong guildID in guilds)
                {
                    (Serveurs as List<IServeur>).Add(Factory.Instance.NouveauIServeur(guildID));
                    EnvoyerLog(LogSeverity.Info, "System", $"IServeur créé (VérifierGuilds). GuildID : {guildID}.");
                }
            });
            VérificationFinie = true;
        }
    }
}
