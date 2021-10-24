using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    /// <summary>
    /// Classe qui contrôle les différents outils personnalisés pour le bot.
    /// </summary>
    public static class Utils
    {
        internal static DiscordSocketClient Client
        {
            private get
            {
                return client;
            }
            set
            {
                if(Client == null)
                {
                    client = value;
                }
            }
        }
        private static DiscordSocketClient client;

        internal static IEnumerable<IServeur> Serveurs
        {
            private get
            {
                return serveurs;
            }
            set
            {
                if(Serveurs == null)
                {
                    serveurs = value;
                }
            }
        }
        private static IEnumerable<IServeur> serveurs;
             
        /// <summary>
        /// Créé un nouvel EmbedBuilder en fonction des paramètres passés.
        /// </summary>
        /// <param name="guildID">Id de la guild courante.</param>
        /// <param name="sujet">Titre de l'embed (facultatif).</param>
        /// <param name="avatarURL">Url de l'image en petit (facultatif).</param>
        /// <param name="couleur">Couleur de l'embed (facultatif).</param>
        /// <param name="description">Description de l'embed (facultatif).</param>
        /// <returns>Renvoi l'EmbedBuilder formé avec ces paramètres.</returns>
        public static EmbedBuilder CréerEmbed(ulong guildID, string sujet = "", string avatarURL = null, Tuple<int, int, int> couleur = null, string description = null)
        {
            EmbedBuilder embed = new EmbedBuilder();
            SocketGuild guild = GuildAvecId(guildID);
            embed.WithAuthor($"{guild.Name} > {Configuration.Nom(ServeurAvecId(guildID).Langue)} > {sujet}", guild.IconUrl);
            embed.WithColor(couleur.Item1, couleur.Item2, couleur.Item3);
            embed.WithFooter($"{Configuration.Nom(ServeurAvecId(guildID).Langue)} - {Configuration.Version}", avatarURL);
            if (description != null)
            {
                embed.WithDescription(description);
            }
            embed.WithCurrentTimestamp();
            return embed;
        }

        /// <summary>
        /// Sépare une chaîne de caractères en une énumération d'autres chaînes de caractères grâce à un séparateur.
        /// </summary>
        /// <param name="chaîne">Chaîne à séparer.</param>
        /// <param name="séparateur">Caractère séparateur.</param>
        /// <returns>Renvoi un énumération de plusieur chaînes séparées.</returns>
        public static IEnumerable<string> SéparationArguments(string chaîne, char séparateur = ' ')
        {
            chaîne = chaîne.Trim(séparateur);
            if(chaîne == "")
            {
                return new List<string>();
            }
            if (!chaîne.Contains($"{séparateur}"))
            {
                return new List<string>() { chaîne };
            }
            List<string> résultat = new List<string>();
            StringBuilder traitement = new StringBuilder();
            bool séparateurDéjàVu = false;
            for (int i = 0; i < chaîne.Length; i++)
            {
                if (chaîne[i] == séparateur)
                {
                    if (!séparateurDéjàVu)
                    {
                        résultat.Add(traitement.ToString());
                        traitement = new StringBuilder();
                        séparateurDéjàVu = true;
                    }
                }
                else
                {
                    séparateurDéjàVu = false;
                    traitement.Append(chaîne[i]);
                }
            }
            résultat.Add(traitement.ToString());
            return résultat;
        }

        /// <summary>
        /// Envoi un message sur un channel.
        /// </summary>
        /// <param name="channel">Id du channel de destination du message.</param>
        /// <param name="message">Contenu du message.</param>
        public static async Task EnvoyerMessageChannel(ulong channel, string message)
        {
            await (Client.GetChannel(channel) as ISocketMessageChannel).SendMessageAsync(message);
        }

        /// <summary>
        /// Envoi un embed sur un channel.
        /// </summary>
        /// <param name="channel">Id du channel de destination du message.</param>
        /// <param name="embed">Embed à envoyer.</param>
        public static async Task EnvoyerEmbedChannel(ulong channel, EmbedBuilder embed)
        {
            await (Client.GetChannel(channel) as ISocketMessageChannel).SendMessageAsync("", false, embed.Build());
        }

        /// <summary>
        /// Envoi un message à un utilisateur.
        /// </summary>
        /// <param name="user">Id de l'utilisateur destinataire du message.</param>
        /// <param name="message">Contenu du message.</param>
        public static async Task EnvoyerMessageUser(ulong user, string message)
        {
            await Client.GetUser(user).SendMessageAsync(message);
        }

        /// <summary>
        /// Envoi un embed à un utilisateur.
        /// </summary>
        /// <param name="user">Id de l'utilisateur destinataire du message.</param>
        /// <param name="embed">Embed à envoyer.</param>
        public static async Task EnvoyerEmbedUser(ulong user, EmbedBuilder embed)
        {
            await Client.GetUser(user).SendMessageAsync("", false, embed.Build());
        }

        /// <summary>
        /// Renvoi un SocketGuild correspondant à l'id de guild entrée.
        /// </summary>
        /// <param name="guildID">Id de guild à chercher.</param>
        /// <returns>SocketGuild lui correspondant.</returns>
        public static SocketGuild GuildAvecId(ulong guildID)
        {
            return Client.GetGuild(guildID);
        }

        /// <summary>
        /// Renvoi un SocketChannel correspondant à l'id de channel entrée.
        /// </summary>
        /// <param name="channelID">Id de channel à chercher.</param>
        /// <returns>SocketChannel lui correspondant.</returns>
        public static SocketChannel ChannelAvecId(ulong channelID)
        {
            return Client.GetChannel(channelID);
        }

        /// <summary>
        /// Indique si un joueur est actuellement kick sur un certain serveur.
        /// </summary>
        /// <param name="userID">Id d'utilisateur du jouer à vérifier.</param>
        /// <param name="guildID">Serveur sur lequel le joueur doit être vérifié.</param>
        /// <returns>Renvoi vrai si le joueur est kick, ainsi que la fin de la sanction.</returns>
        public static Tuple<bool, DateTime> VérifierJoueurKick(ulong userID, ulong guildID)
        {
            foreach (IServeur serveur in Serveurs)
            {
                if (guildID == serveur.GuildID)
                {
                    foreach (IKick kick in serveur.JoueursKick)
                    {
                        if (userID == kick.UserID)
                        {

                            if (kick.Sanction < DateTime.Now)
                            {
                                (serveur.JoueursKick as List<IKick>).Remove(kick);
                                return new Tuple<bool, DateTime>(false, DateTime.MinValue);
                            }
                            return new Tuple<bool, DateTime>(true, kick.Sanction);
                        }
                    }
                    return new Tuple<bool, DateTime>(false, DateTime.MinValue);
                }
            }
            return new Tuple<bool, DateTime>(false, DateTime.MinValue);
        }

        /// <summary>
        /// Indique si un joueur est actuellement en partie sur un certain serveur.
        /// </summary>
        /// <param name="userID">Id d'utilisateur du jouer à vérifier.</param>
        /// <param name="guildID">Serveur sur lequel le joueur doit être vérifié.</param>
        /// <returns>Renvoi la partie sur laquelle le joueur est, sinon renvoi null.</returns>
        public static IPartie PartieDuJoueur(ulong userID, ulong guildID)
        {
            foreach (IServeur serveur in Serveurs)
            {
                if (guildID == serveur.GuildID)
                {
                    if (serveur.PartieVocale != null)
                    {
                        foreach (IJoueur joueur in serveur.PartieVocale.JoueursEnAttente)
                        {
                            if (userID == joueur.UserID)
                            {
                                return serveur.PartieVocale;
                            }
                        }
                        foreach (IJoueur joueur in serveur.PartieVocale.JoueursEnJeu)
                        {
                            if (userID == joueur.UserID)
                            {
                                return serveur.PartieVocale;
                            }
                        }
                    }
                    foreach (IPartie partie in serveur.Parties)
                    {
                        foreach (IJoueur joueur in partie.JoueursEnAttente)
                        {
                            if (userID == joueur.UserID)
                            {
                                return partie;
                            }
                        }
                        foreach (IJoueur joueur in partie.JoueursEnJeu)
                        {
                            if (userID == joueur.UserID)
                            {
                                return partie;
                            }
                        }
                    }
                    return null;
                }
            }
            Console.WriteLine("F");
            return null;
        }

        /// <summary>
        /// Indique si un channel héberge une partie.
        /// </summary>
        /// <param name="channelID">Id du channel à vérifier.</param>
        /// <returns>Renvoi la partie hébergée, sinon renvoi null.</returns>
        public static IPartie PartieDuChannel(ulong channelID)
        {
            SocketGuildChannel channel = Client.GetChannel(channelID) as SocketGuildChannel;
            if (channel != null)
            {
                if (channel.Guild != null)
                {
                    foreach (IServeur serveur in Serveurs)
                    {
                        if (channel.Guild.Id == serveur.GuildID)
                        {
                            if (serveur.PartieVocale != null)
                            {
                                if (serveur.PartieVocale.Channel == channelID || serveur.PartieVocale.ChannelVocal == channelID)
                                {
                                    return serveur.PartieVocale;
                                }
                            }
                            foreach (IPartie partie in serveur.Parties)
                            {
                                if (partie.Channel == channelID)
                                {
                                    return partie;
                                }
                            }
                            return null;
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Indique quel IServeur héberge une certaine IPartie.
        /// </summary>
        /// <param name="partie">IPartie hébergée.</param>
        /// <returns>Renvoi son IServeur hôte.</returns>
        public static IServeur ServeurDeLaPartie(IPartie partie)
        {
            foreach(IServeur serveur in Serveurs)
            {
                if(partie.Equals(serveur.PartieVocale as IPartie))
                {
                    return serveur;
                }
                foreach(IPartie partieHébergée in serveur.Parties)
                {
                    if(partie.Equals(partieHébergée))
                    {
                        return serveur;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Indique quel IServeur est hébergé par une certain guild.
        /// </summary>
        /// <param name="guildID">Id de la guild hôte.</param>
        /// <returns>Renvoi le IServeur hébergé, sinon renvoi null.</returns>
        public static IServeur ServeurAvecId(ulong guildID)
        {
            foreach (IServeur serveur in Serveurs)
            {
                if (serveur.GuildID == guildID)
                {
                    return serveur;
                }
            }
            return null;
        }

        /// <summary>
        /// Indique si un utilisateur est administrateur du bot sur un serveur.
        /// </summary>
        /// <param name="userID">Id de l'utilisateur.</param>
        /// <param name="guildID">Id du serveur.</param>
        /// <returns></returns>
        public static bool EstAdmin(ulong userID, ulong guildID)
        {
            SocketGuild guild = Client.GetGuild(guildID);
            IServeur serveur = ServeurAvecId(guildID);
            if (guild == null || serveur == null)
            {
                throw new NullReferenceException("Le serveur et/ou la guild n'a/n'ont pas été trouvé.");
            }
            SocketGuildUser utilisateur = null;
            foreach(SocketGuildUser user in guild.Users)
            {
                if(user.Id == userID)
                {
                    utilisateur = user;
                    break;
                }
            }
            if(utilisateur == null)
            {
                throw new NullReferenceException("L'utilisateur n'a pas pu être trouvé.");
            }
            foreach(SocketRole rôle in utilisateur.Roles)
            {
                foreach(IRang rang in serveur.RangsAdministrateur)
                {
                    if (rang.RôleID == rôle.Id)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Indique si un utilisateur est modérateur/staff du bot sur un serveur.
        /// </summary>
        /// <param name="userID">Id de l'utilisateur.</param>
        /// <param name="guildID">Id du serveur.</param>
        /// <returns></returns>
        public static bool EstStaff(ulong userID, ulong guildID)
        {
            SocketGuild guild = Client.GetGuild(guildID);
            IServeur serveur = ServeurAvecId(guildID);
            if (guild == null || serveur == null)
            {
                throw new NullReferenceException("Le serveur et/ou la guild n'a/n'ont pas été trouvé.");
            }
            SocketGuildUser utilisateur = null;
            foreach (SocketGuildUser user in guild.Users)
            {
                if (user.Id == userID)
                {
                    utilisateur = user;
                    break;
                }
            }
            if (utilisateur == null)
            {
                throw new NullReferenceException("L'utilisateur n'a pas pu être trouvé.");
            }
            foreach (SocketRole rôle in utilisateur.Roles)
            {
                foreach (IRang rang in serveur.RangsStaff)
                {
                    if (rang.RôleID == rôle.Id)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
