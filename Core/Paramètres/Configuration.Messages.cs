using System;
using Discord;
using System.Collections.Generic;
using System.Text;
using Discord.Commands;
using System.Linq;
using Discord.WebSocket;

namespace Core
{
    /// <summary>
    /// Cette partie de la classe contrôle les messages et les embed en fonction des langues.
    /// </summary>
    static partial class Configuration
    {
        //---------------------------------------- GENERAL ----------------------------------------//


        /// <summary>
        /// Construit un embed et l'envoi en fonction de la langue du serveur.
        /// Cette méthode est appellée lorsqu'un joueur effectue une commande dont il n'a pas l'accès.
        /// </summary>
        /// <param name="Context">Contexte de l'embed.</param>
        /// <returns>Renvoi l'embed construit.</returns>
        internal static EmbedBuilder General_NoPerm(SocketCommandContext Context)
        {
            Langue langue = Utils.ServeurAvecId(Context.Guild.Id).Langue;
            EmbedBuilder embed;
            switch (langue)
            {
                case Langue.EN:
                    embed = Utils.CréerEmbed(Context.Guild.Id, "Error", Context.Client.CurrentUser.GetAvatarUrl(), CouleurErreur);
                    embed.AddField("Access denied", "You don't have the permission to perform this command.");
                    return embed;
                case Langue.FR:
                    embed = Utils.CréerEmbed(Context.Guild.Id, "Erreur", Context.Client.CurrentUser.GetAvatarUrl(), CouleurErreur);
                    embed.AddField("Accès refusé", "Vous n'avez pas la permission d'éffectuer cette commande.");
                    return embed;
                default:
                    throw new NotImplementedException();
            }
        }


        //------------------------------------- CJOIN COMMAND -------------------------------------//



        /// <summary>
        /// Construit un embed et l'envoi en fonction de la langue du serveur.
        /// Cette méthode est appellée lorsqu'un joueur essaye de rejoindre une partie qui n'existe pas.
        /// </summary>
        /// <param name="Context">Contexte de l'embed.</param>
        /// <returns>Renvoi l'embed construit.</returns>
        internal static EmbedBuilder CJoin_PasDePartie(SocketCommandContext Context)
        {
            Langue langue = Utils.ServeurAvecId(Context.Guild.Id).Langue;
            EmbedBuilder embed;
            switch (langue)
            {
                case Langue.EN:
                    embed = Utils.CréerEmbed(Context.Guild.Id, "Registration", Context.Client.CurrentUser.GetAvatarUrl(), CouleurErreur);
                    embed.AddField("Channel not recognized by the werewolf for a night", "Check that you are on a channel that hosts a party of werewolf for one night.");
                    return embed;
                case Langue.FR:
                    embed = Utils.CréerEmbed(Context.Guild.Id, "Inscription", Context.Client.CurrentUser.GetAvatarUrl(), CouleurErreur);
                    embed.AddField("Channel non reconnu par le loup-garou pour une nuit", "Vérifiez que vous êtes bien sur un channel qui héberge une partie de loup-garou pour une nuit.");
                    return embed;
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Construit un embed et l'envoi en fonction de la langue du serveur.
        /// Cette méthode est appellée lorsqu'un joueur essaye de rejoindre une partie alors qu'il est kick.
        /// </summary>
        /// <param name="Context">Contexte de l'embed.</param>
        /// <returns>Renvoi l'embed construit.</returns>
        internal static EmbedBuilder CJoin_JoueurKick(SocketCommandContext Context)
        {
            Langue langue = Utils.ServeurAvecId(Context.Guild.Id).Langue;
            EmbedBuilder embed;
            switch (langue)
            {
                case Langue.EN:
                    embed = Utils.CréerEmbed(Context.Guild.Id, "Registration", Context.Client.CurrentUser.GetAvatarUrl(), CouleurErreur);
                    embed.AddField("You can not join the game", "You were excluded from the werewolf for one night parties because it seems you broke the rules.");
                    return embed;
                case Langue.FR:
                    embed = Utils.CréerEmbed(Context.Guild.Id, "Inscription", Context.Client.CurrentUser.GetAvatarUrl(), CouleurErreur);
                    embed.AddField("Vous ne pouvez pas rejoindre la partie", "Vous avez été kick des parties de loup-garou pour une nuit car vous avez dû enfreindre les règles.");
                    return embed;
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Construit un embed et l'envoi en fonction de la langue du serveur.
        /// Cette méthode est appellée lorsqu'un joueur essaye de rejoindre une partie alors qu'il est déjà dans une partie.
        /// </summary>
        /// <param name="Context">Contexte de l'embed.</param>
        /// <returns>Renvoi l'embed construit.</returns>
        internal static EmbedBuilder CJoin_DéjàDansUnePartie(SocketCommandContext Context)
        {
            Langue langue = Utils.ServeurAvecId(Context.Guild.Id).Langue;
            EmbedBuilder embed;
            switch (langue)
            {
                case Langue.EN:
                    embed = Utils.CréerEmbed(Context.Guild.Id, "Registration", Context.Client.CurrentUser.GetAvatarUrl(), CouleurErreur);
                    embed.AddField("You can not join the game", "It seems that you are already in this game or another game. Leave the current game first and then try joining it again.");
                    return embed;
                case Langue.FR:
                    embed = Utils.CréerEmbed(Context.Guild.Id, "Inscription", Context.Client.CurrentUser.GetAvatarUrl(), CouleurErreur);
                    embed.AddField("Vous ne pouvez pas rejoindre la partie", "Il semble que vous êtes déjà dans [la/une autre] partie. Quittez d'abord la partie en cours puis réessayez de la rejoindre.");
                    return embed;
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Construit un embed et l'envoi en fonction de la langue du serveur.
        /// Cette méthode est appellée lorsqu'un joueur essaye de rejoindre une partie alors qu'elle est en cours.
        /// </summary>
        /// <param name="Context">Contexte de l'embed.</param>
        /// <returns>Renvoi l'embed construit.</returns>
        internal static EmbedBuilder CJoin_PartieEnCours(SocketCommandContext Context)
        {
            Langue langue = Utils.ServeurAvecId(Context.Guild.Id).Langue;
            EmbedBuilder embed;
            switch (langue)
            {
                case Langue.EN:
                    embed = Utils.CréerEmbed(Context.Guild.Id, "Registration", Context.Client.CurrentUser.GetAvatarUrl(), CouleurAttention);
                    embed.AddField("You are on the waiting list", "The game has already started or is not over yet, so you're on the waiting list for the next game.");
                    return embed;
                case Langue.FR:
                    embed = Utils.CréerEmbed(Context.Guild.Id, "Inscription", Context.Client.CurrentUser.GetAvatarUrl(), CouleurAttention);
                    embed.AddField("Vous êtes sur la liste d'attente", "La partie a déjà commencée ou n'est pas encore terminée, vous êtes donc sur la liste d'attente de la prochaine partie.");
                    return embed;
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Construit un embed et l'envoi en fonction de la langue du serveur.
        /// Cette méthode est appellée lorsqu'un joueur essaye de rejoindre une partie alors qu'elle est complète.
        /// </summary>
        /// <param name="Context">Contexte de l'embed.</param>
        /// <returns>Renvoi l'embed construit.</returns>
        internal static EmbedBuilder CJoin_PartieComplète(SocketCommandContext Context)
        {
            Langue langue = Utils.ServeurAvecId(Context.Guild.Id).Langue;
            EmbedBuilder embed;
            switch (langue)
            {
                case Langue.EN:
                    embed = Utils.CréerEmbed(Context.Guild.Id, "Registration", Context.Client.CurrentUser.GetAvatarUrl(), CouleurAttention);
                    embed.AddField("You are on the waiting list", "There is no more free slot for this game because it is complete, so you are registered for the next, until a slot becomes free.");
                    return embed;
                case Langue.FR:
                    embed = Utils.CréerEmbed(Context.Guild.Id, "Inscription", Context.Client.CurrentUser.GetAvatarUrl(), CouleurAttention);
                    embed.AddField("Vous êtes sur la liste d'attente", "Il n'y a plus de place pour cette partie car elle est complète, vous êtes donc inscrit à la prochaine, jusqu'à ce qu'une place se libère.");
                    return embed;
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Construit un embed et l'envoi en fonction de la langue du serveur.
        /// Cette méthode est appellée lorsqu'un joueur arrive à rejoindre une partie sans problème.
        /// </summary>
        /// <param name="Context">Contexte de l'embed.</param>
        /// <param name="partie">Partie que le joueur rejoint.</param>
        /// <returns>Renvoi l'embed construit.</returns>
        internal static EmbedBuilder CJoin_Succès(SocketCommandContext Context, IPartie partie)
        {
            Langue langue = Utils.ServeurAvecId(Context.Guild.Id).Langue;
            EmbedBuilder embed;
            switch (langue)
            {
                case Langue.EN:
                    embed = Utils.CréerEmbed(Context.Guild.Id, "Registration", Context.Client.CurrentUser.GetAvatarUrl(), CouleurInfo);
                    embed.AddField($"{Context.User.Username} joined the game", $"Players : **{partie.Taille} / {joueursMax}**");
                    return embed;
                case Langue.FR:
                    embed = Utils.CréerEmbed(Context.Guild.Id, "Inscription", Context.Client.CurrentUser.GetAvatarUrl(), CouleurInfo);
                    embed.AddField($"{Context.User.Username} a rejoint la partie", $"Nombre de joueur(s) : **{partie.Taille} / {joueursMax}**");
                    return embed;
                default:
                    throw new NotImplementedException();
            }
        }



        //------------------------------------- CQUIT COMMAND -------------------------------------//



        /// <summary>
        /// Construit un embed et l'envoi en fonction de la langue du serveur.
        /// Cette méthode est appellée lorsqu
        /// </summary>
        /// <param name="Context">Contexte de l'embed.</param>
        /// <returns>Renvoi l'embed construit.</returns>
        internal static EmbedBuilder CQuit_PasDePartie(SocketCommandContext Context)
        {
            Langue langue = Utils.ServeurAvecId(Context.Guild.Id).Langue;
            EmbedBuilder embed;
            switch (langue)
            {
                case Langue.EN:
                    embed = Utils.CréerEmbed(Context.Guild.Id, "Unregistration", Context.Client.CurrentUser.GetAvatarUrl(), CouleurErreur);
                    embed.AddField("You are in a werewolf for one night game", "(So ​​you can sign up for any game.)");
                    return embed;
                case Langue.FR:
                    embed = Utils.CréerEmbed(Context.Guild.Id, "Désinscription", Context.Client.CurrentUser.GetAvatarUrl(), CouleurErreur);
                    embed.AddField("Vous n'êtes pas dans une partie de loup-garou pour une nuit", "(Vous pouvez donc vous inscrire à n'importe quelle partie.)");
                    return embed;
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Construit un embed et l'envoi en fonction de la langue du serveur.
        /// Cette méthode est appellée lorsqu
        /// </summary>
        /// <param name="Context">Contexte de l'embed.</param>
        /// <returns>Renvoi l'embed construit.</returns>
        internal static EmbedBuilder CQuit_PartieEnCours(SocketCommandContext Context)
        {
            Langue langue = Utils.ServeurAvecId(Context.Guild.Id).Langue;
            EmbedBuilder embed;
            switch (langue)
            {
                case Langue.EN:
                    embed = Utils.CréerEmbed(Context.Guild.Id, "Unregistration", Context.Client.CurrentUser.GetAvatarUrl(), CouleurErreur);
                    embed.AddField("You can not leave this game", "The game is in progress, you can not leave it before the end of it.");
                    return embed;
                case Langue.FR:
                    embed = Utils.CréerEmbed(Context.Guild.Id, "Désinscription", Context.Client.CurrentUser.GetAvatarUrl(), CouleurErreur);
                    embed.AddField("Vous ne pouvez pas quitter cette partie", "La partie est en cours, vous ne pouvez plus la quitter avant la fin de celle-ci.");
                    return embed;
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Construit un embed et l'envoi en fonction de la langue du serveur.
        /// Cette méthode est appellée lorsqu
        /// </summary>
        /// <param name="Context">Contexte de l'embed.</param>
        /// <returns>Renvoi l'embed construit.</returns>
        internal static EmbedBuilder CQuit_Succès(SocketCommandContext Context, IPartie partie)
        {
            Langue langue = Utils.ServeurAvecId(Context.Guild.Id).Langue;
            EmbedBuilder embed;
            switch (langue)
            {
                case Langue.EN:
                    embed = Utils.CréerEmbed(Context.Guild.Id, "Unregistration", Context.Client.CurrentUser.GetAvatarUrl(), CouleurInfo);
                    embed.AddField($"{Context.User.Username} left the game.", $"Players : **{partie.Taille} / {joueursMax}**");
                    return embed;
                case Langue.FR:
                    embed = Utils.CréerEmbed(Context.Guild.Id, "Désinscription", Context.Client.CurrentUser.GetAvatarUrl(), CouleurInfo);
                    embed.AddField($"{Context.User.Username} a quitté la partie", $"Nombre de joueur(s) : **{partie.Taille} / {joueursMax}**");
                    return embed;
                default:
                    throw new NotImplementedException();
            }
        }



        //------------------------------------- CKICK COMMAND -------------------------------------//



        /// <summary>
        /// Construit un embed et l'envoi en fonction de la langue du serveur.
        /// Cette méthode est appellée lorsqu'un modérateur veut kick un joueur et qu'il n'y a pas le bon nombre d'argument.
        /// </summary>
        /// <param name="Context">Contexte de l'embed.</param>
        /// <returns>Renvoi l'embed construit.</returns>
        internal static EmbedBuilder CKick_NbArgumentsIncorrect(SocketCommandContext Context)
        {
            Langue langue = Utils.ServeurAvecId(Context.Guild.Id).Langue;
            EmbedBuilder embed;
            switch (langue)
            {
                case Langue.EN:
                    embed = Utils.CréerEmbed(Context.Guild.Id, "Kick", Context.Client.CurrentUser.GetAvatarUrl(), CouleurErreur);
                    embed.AddField("Wrong syntax", $"Usage : {Préfix.First()}kick <User's ID> [Duration in minutes]");
                    return embed;
                case Langue.FR:
                    embed = Utils.CréerEmbed(Context.Guild.Id, "Kick", Context.Client.CurrentUser.GetAvatarUrl(), CouleurErreur);
                    embed.AddField("Syntaxe incorrecte", $"Utilisation : {Préfix.First()}kick <ID de l'utilisateur> [Durée en minutes]");
                    return embed;
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Construit un embed et l'envoi en fonction de la langue du serveur.
        /// Cette méthode est appellée lorsqu'un modérateur veut kick (ou consulter le profil d') un
        /// joueur et que l'argument 1 est incorrect.
        /// </summary>
        /// <param name="Context">Contexte de l'embed.</param>
        /// <returns>Renvoi l'embed construit.</returns>
        internal static EmbedBuilder CKick_Arg1Incorrect(SocketCommandContext Context)
        {
            Langue langue = Utils.ServeurAvecId(Context.Guild.Id).Langue;
            EmbedBuilder embed;
            switch (langue)
            {
                case Langue.EN:
                    embed = Utils.CréerEmbed(Context.Guild.Id, "Kick", Context.Client.CurrentUser.GetAvatarUrl(), CouleurErreur);
                    embed.AddField("Invalid argument 1", $"The first argument must be the id of a user who is on the Discord server.");
                    return embed;
                case Langue.FR:
                    embed = Utils.CréerEmbed(Context.Guild.Id, "Kick", Context.Client.CurrentUser.GetAvatarUrl(), CouleurErreur);
                    embed.AddField("L'argument 1 est incorrect", $"Le premier argument doit être l'id d'un utilisateur du serveur Discord.");
                    return embed;
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Construit un embed et l'envoi en fonction de la langue du serveur.
        /// Cette méthode est appellée lorsqu'un modérateur veut kick un joueur et que l'argument 2 est incorrect.
        /// </summary>
        /// <param name="Context">Contexte de l'embed.</param>
        /// <returns>Renvoi l'embed construit.</returns>
        internal static EmbedBuilder CKick_Arg2Incorrect(SocketCommandContext Context)
        {
            Langue langue = Utils.ServeurAvecId(Context.Guild.Id).Langue;
            EmbedBuilder embed;
            switch (langue)
            {
                case Langue.EN:
                    embed = Utils.CréerEmbed(Context.Guild.Id, "Kick", Context.Client.CurrentUser.GetAvatarUrl(), CouleurErreur);
                    embed.AddField("Invalid argument 2", "The second argument must be a positive integer.");
                    return embed;
                case Langue.FR:
                    embed = Utils.CréerEmbed(Context.Guild.Id, "Kick", Context.Client.CurrentUser.GetAvatarUrl(), CouleurErreur);
                    embed.AddField("L'argument 2 est incorrect", "Le deuxième argument doit être un entier positif.");
                    return embed;
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Construit un embed et l'envoi en fonction de la langue du serveur.
        /// Cette méthode est appellée lorsque la cible d'un kick est protégée.
        /// </summary>
        /// <param name="Context">Contexte de l'embed.</param>
        /// <param name="cible">Utilisateur ciblé par le kick.</param>
        /// <returns>Renvoi l'embed construit.</returns>
        internal static EmbedBuilder CKick_JoueurPasKickable(SocketCommandContext Context, SocketGuildUser cible)
        {
            Langue langue = Utils.ServeurAvecId(Context.Guild.Id).Langue;
            EmbedBuilder embed;
            switch (langue)
            {
                case Langue.EN:
                    embed = Utils.CréerEmbed(Context.Guild.Id, "Error", Context.Client.CurrentUser.GetAvatarUrl(), CouleurErreur);
                    embed.AddField($"You can not (un)kick **{cible.Username}**, or check his profile", "The rank of this user is too high.");
                    return embed;
                case Langue.FR:
                    embed = Utils.CréerEmbed(Context.Guild.Id, "Erreur", Context.Client.CurrentUser.GetAvatarUrl(), CouleurErreur);
                    embed.AddField($"Vous ne pouvez pas (un)kick (on consulter le profil de) **{cible.Username}**", "Le rang de cet utilisateur est trop élevé.");
                    return embed;
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Construit un embed et l'envoi en fonction de la langue du serveur.
        /// Cette méthode est appellée lorsqu'un utilisateur veut consulter le profil de kick d'un autre utilisateur.
        /// </summary>
        /// <param name="Context">Contexte de l'embed.</param>
        /// <param name="cible">Utilisateur ciblé.</param>
        /// <param name="profil">Permet de connaître la situation de l'utilisateur.</param>
        /// <returns>Renvoi l'embed construit.</returns>
        internal static EmbedBuilder CKick_Profil(SocketCommandContext Context, SocketGuildUser cible, Tuple<bool, DateTime> profil)
        {
            Langue langue = Utils.ServeurAvecId(Context.Guild.Id).Langue;
            EmbedBuilder embed;
            if (profil.Item1)
            {
                switch (langue)
                {
                    case Langue.EN:
                        embed = Utils.CréerEmbed(Context.Guild.Id, "Kick", Context.Client.CurrentUser.GetAvatarUrl(), CouleurInfo);
                        embed.AddField($"**{cible.Username}** is actually kick", $"End of punishment : {profil.Item2.ToString()}.\nIf you apply another punishment, the current will be erased.");
                        return embed;
                    case Langue.FR:
                        embed = Utils.CréerEmbed(Context.Guild.Id, "Kick", Context.Client.CurrentUser.GetAvatarUrl(), CouleurInfo);
                        embed.AddField($"**{cible.Username}** est actuellement kick", $"Fin de la sanction : {profil.Item2.ToString()}.\n Si vous aplliquez une autre sanction, la précédente sera effacée.");
                        return embed;
                    default:
                        throw new NotImplementedException();
                }
            }
            else
            {
                switch (langue)
                {
                    case Langue.EN:
                        embed = Utils.CréerEmbed(Context.Guild.Id, "Kick", Context.Client.CurrentUser.GetAvatarUrl(), CouleurInfo);
                        embed.AddField($"**{cible.Username}** isn't actually kick", "So you can kick this user without any problem.");
                        return embed;
                    case Langue.FR:
                        embed = Utils.CréerEmbed(Context.Guild.Id, "Kick", Context.Client.CurrentUser.GetAvatarUrl(), CouleurInfo);
                        embed.AddField($"**{cible.Username}** n'est pas kick actuellement", "Vous pouvez donc le kick sans problème.");
                        return embed;
                    default:
                        throw new NotImplementedException();
                }
            }

        }

        /// <summary>
        /// Construit un embed et l'envoi en fonction de la langue du serveur.
        /// Cette méthode est appellée lorsqu'un utilisateur en kick un autre.
        /// </summary>
        /// <param name="Context">Contexte de l'embed.</param>
        /// <param name="kick">Kick effectué.</param>
        /// <returns>Renvoi l'embed construit.</returns>
        internal static EmbedBuilder CKick_Succès(SocketCommandContext Context, IKick kick)
        {
            Langue langue = Utils.ServeurAvecId(Context.Guild.Id).Langue;
            EmbedBuilder embed;
            switch (langue)
            {
                case Langue.EN:
                    embed = Utils.CréerEmbed(Context.Guild.Id, "Kick", Context.Client.CurrentUser.GetAvatarUrl(), CouleurInfo);
                    embed.AddField("The user was succefully kicked", $"End of punishment : {kick.Sanction.ToString()}");
                    return embed;
                case Langue.FR:
                    embed = Utils.CréerEmbed(Context.Guild.Id, "Kick", Context.Client.CurrentUser.GetAvatarUrl(), CouleurInfo);
                    embed.AddField("L'utilisateur a bien été kick", $"Fin de la sanction : {kick.Sanction.ToString()}");
                    return embed;
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Construit un embed et l'envoi en fonction de la langue du serveur.
        /// Cette méthode est appellée lorsqu'un utilisateur enlève un kick d'un autre utilisateur.
        /// </summary>
        /// <param name="Context">Contexte de l'embed.</param>
        /// <returns>Renvoi l'embed construit.</returns>
        internal static EmbedBuilder CKick_Succès_Durée0(SocketCommandContext Context)
        {
            Langue langue = Utils.ServeurAvecId(Context.Guild.Id).Langue;
            EmbedBuilder embed;
            switch (langue)
            {
                case Langue.EN:
                    embed = Utils.CréerEmbed(Context.Guild.Id, "Kick", Context.Client.CurrentUser.GetAvatarUrl(), CouleurInfo);
                    embed.AddField("The user is no longer kicked", "He can join again the werewolf for one night games.");
                    return embed;
                case Langue.FR:
                    embed = Utils.CréerEmbed(Context.Guild.Id, "Kick", Context.Client.CurrentUser.GetAvatarUrl(), CouleurInfo);
                    embed.AddField("L'utilisateur n'est plus kick", $"Il peut de nouveau rejoindre des parties de loup-garou pour une nuit.");
                    return embed;
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Construit un embed et l'envoi en fonction de la langue du serveur.
        /// Cette méthode est appellée lorsqu'un utilisateur veut enlever un kick a un autre alors qu'il n'en a pas.
        /// </summary>
        /// <param name="Context">Contexte de l'embed.</param>
        /// <returns>Renvoi l'embed construit.</returns>
        internal static EmbedBuilder CKick_CiblePasKick(SocketCommandContext Context, SocketGuildUser cible)
        {
            Langue langue = Utils.ServeurAvecId(Context.Guild.Id).Langue;
            EmbedBuilder embed;
            switch (langue)
            {
                case Langue.EN:
                    embed = Utils.CréerEmbed(Context.Guild.Id, "Kick", Context.Client.CurrentUser.GetAvatarUrl(), CouleurErreur);
                    embed.AddField($"You can not unkick **{cible.Username}**", "Why ? Because he isn't kick !");
                    return embed;
                case Langue.FR:
                    embed = Utils.CréerEmbed(Context.Guild.Id, "Kick", Context.Client.CurrentUser.GetAvatarUrl(), CouleurErreur);
                    embed.AddField($"Vous ne pouvez pas enlever un kick de **{cible.Username}**", "Pourquoi ? Car il n'est pas kick !");
                    return embed;
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Construit un embed et l'envoi en fonction de la langue du serveur.
        /// Cette méthode est appellée lorsqu'un utilisateur en kick un autre.
        /// </summary>
        /// <param name="Context">Contexte de l'embed.</param>
        /// <param name="cible">Utilisateur exclu.</param>
        /// <returns>Renvoi l'embed construit.</returns>
        internal static EmbedBuilder CKick_Notification(SocketCommandContext Context, SocketGuildUser cible)
        {
            Langue langue = Utils.ServeurAvecId(Context.Guild.Id).Langue;
            EmbedBuilder embed;
            switch (langue)
            {
                case Langue.EN:
                    embed = Utils.CréerEmbed(Context.Guild.Id, "Kick", Context.Client.CurrentUser.GetAvatarUrl(), CouleurAttention);
                    embed.AddField($"**{cible.Username}** was kicked from the game", "Breaking the rules isn't without consequence...");
                    return embed;
                case Langue.FR:
                    embed = Utils.CréerEmbed(Context.Guild.Id, "Kick", Context.Client.CurrentUser.GetAvatarUrl(), CouleurAttention);
                    embed.AddField($"**{cible.Username}** a été kick", "Enfreindre les règles n'est pas sans conséquence...");
                    return embed;
                default:
                    throw new NotImplementedException();
            }
        }



        //------------------------------------- ----- COMMAND -------------------------------------//



        /*
         * PATTERNE POUR REFAIRE UN EMBED DE COMMANDE
         * 
        /// <summary>
        /// Construit un embed et l'envoi en fonction de la langue du serveur.
        /// Cette méthode est appellée lorsqu
        /// </summary>
        /// <param name="Context">Contexte de l'embed.</param>
        /// <returns>Renvoi l'embed construit.</returns>
        internal static EmbedBuilder C(SocketCommandContext Context)
        {
            Langue langue = Utils.ServeurAvecId(Context.Guild.Id).Langue;
            EmbedBuilder embed;
            switch (langue)
            {
                case Langue.EN:
                    embed = Utils.CréerEmbed(Context.Guild.Id, "", Context.Client.CurrentUser.GetAvatarUrl(), Couleur);
                    embed.AddField("", "");
                    return embed;
                case Langue.FR:
                    embed = Utils.CréerEmbed(Context.Guild.Id, "", Context.Client.CurrentUser.GetAvatarUrl(), Couleur);
                    embed.AddField("", "");
                    return embed;
                default:
                    throw new NotImplementedException();
            }
        }
        */
    }
}
