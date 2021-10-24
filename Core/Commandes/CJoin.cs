using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class CJoin : ModuleBase<SocketCommandContext>
    {
        [Command("join"), Summary("Rejoindre une partie de Loup-Garou pour une nuit.")]
        public async Task Commande()
        {
            // Initialisation
            IPartie partie = Utils.PartieDuChannel(Context.Channel.Id);
            await Context.Message.DeleteAsync();

            // Vérification du channel : Utilisé par une partie.
            if (partie == null)
            {
                await Utils.EnvoyerEmbedUser(Context.User.Id, Configuration.CJoin_PasDePartie(Context));
                return;
            }

            // Vérification de l'utilisateur : N'est pas kick.
            if (Utils.VérifierJoueurKick(Context.User.Id, Context.Guild.Id).Item1)
            {
                await Utils.EnvoyerEmbedUser(Context.User.Id, Configuration.CJoin_JoueurKick(Context));
                return;
            }

            // Vérification de l'utilisateur : N'est pas dans une autre partie.
            if (Utils.PartieDuJoueur(Context.User.Id, Context.Guild.Id) != null)
            {
                await Utils.EnvoyerEmbedUser(Context.User.Id, Configuration.CJoin_DéjàDansUnePartie(Context));
                return;
            }

            // Vérification de la partie : GameState = En attente de joueurs.
            if (partie.Etat != GameState.AttenteDeJoueur)
            {
                partie.AjouterJoueur(Context.User.Id);
                await Utils.EnvoyerEmbedUser(Context.User.Id, Configuration.CJoin_PartieEnCours(Context));
                return;
            }

            // Vérification de la partie : Place disponible.
            if (partie.Taille >= Configuration.joueursMax)
            {
                
                partie.AjouterJoueur(Context.User.Id);
                await Utils.EnvoyerEmbedUser(Context.User.Id, Configuration.CJoin_PartieComplète(Context));
                return;
            }

            // Ajout de l'utilisateur.
            partie.AjouterJoueur(Context.User.Id);
            await Utils.EnvoyerEmbedChannel(Context.Channel.Id, Configuration.CJoin_Succès(Context, partie));
            return;
        }
    }
}
