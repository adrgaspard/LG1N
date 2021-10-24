using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class CQuit : ModuleBase<SocketCommandContext>
    {
        [Command("quit"), Summary("Quitter une partie de Loup-Garou pour une nuit qui est en attente de joueur.")]
        public async Task Commande()
        {
            // Initialisation
            IPartie partie = Utils.PartieDuJoueur(Context.User.Id, Context.Guild.Id);
            await Context.Message.DeleteAsync();

            // Vérification du joueur : Dans une partie
            if (partie == null)
            {
                await Utils.EnvoyerEmbedUser(Context.User.Id, Configuration.CQuit_PasDePartie(Context));
                return;
            }

            // Vérification de la partie : En attente de joueur
            if (partie.Etat != GameState.AttenteDeJoueur)
            {
                await Utils.EnvoyerEmbedUser(Context.User.Id, Configuration.CQuit_PartieEnCours(Context));
                return;
            }

            // Retrait de l'utilisateur
            partie.RetirerJoueur(Context.User.Id);
            await Utils.EnvoyerEmbedChannel(partie.Channel, Configuration.CQuit_Succès(Context, partie));
            return;
        }
    }
}
