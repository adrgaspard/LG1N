using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class CKick : ModuleBase<SocketCommandContext>
    {
        [Command("kick"), Summary("Exclure un joueur des parties de loup-garou pour une nuit sur un serveur pendant un certain temps.")]
        public async Task Commande([Remainder] string input = "")
        {
            // Initialisation
            List<string> args = Utils.SéparationArguments(input) as List<string>;
            SocketGuildUser cible = null;
            ulong cibleID = 0;
            if(args.Count > 0)
            {
                if (ulong.TryParse(args.ElementAt(0), out cibleID))
                {
                    foreach (SocketGuildUser user in Context.Guild.Users)
                    {
                        if (user.Id == cibleID)
                        {
                            cible = user;
                            break;
                        }
                    }
                }
            }
            await Context.Message.DeleteAsync();

            // Vérification de la permission : Est Admin || Est Staff.
            bool estAdmin = Utils.EstAdmin(Context.Message.Author.Id, Context.Guild.Id);
            bool estStaff = Utils.EstStaff(Context.Message.Author.Id, Context.Guild.Id);
            if(!estAdmin && !estStaff)
            {
                await Utils.EnvoyerEmbedUser(Context.User.Id, Configuration.General_NoPerm(Context));
                return;
            }

            // Vérification du nombre d'argument : 1 ou 2.
            ulong durée = 0;
            if(args.Count < 1 || args.Count > 2)
            {
                await Utils.EnvoyerEmbedUser(Context.User.Id, Configuration.CKick_NbArgumentsIncorrect(Context));
                return;
            }

            // Vérification de la validité de la cible : existe bien dans la guild.
            if (cible == null)
            {
                await Utils.EnvoyerEmbedUser(Context.User.Id, Configuration.CKick_Arg1Incorrect(Context));
                return;
            }
            IPartie partie = Utils.PartieDuJoueur(cible.Id, Context.Guild.Id);

            // Vérification de l'argument 2 : doit être un ulong.
            if (args.Count == 2 && !ulong.TryParse(args.ElementAt(1), out durée))
            {
                await Utils.EnvoyerEmbedUser(Context.User.Id, Configuration.CKick_Arg2Incorrect(Context));
                return;
            }

            // Vérification de la permission : Est Admin & Cible n'est pas admin || Est Staff & Cible n'est pas staff.
            if (!((estAdmin && !Utils.EstAdmin(cible.Id, Context.Guild.Id)) || (estStaff && !Utils.EstStaff(cible.Id, Context.Guild.Id))))
            {
                await Utils.EnvoyerEmbedUser(Context.User.Id, Configuration.CKick_JoueurPasKickable(Context, cible));
                return;
            }

            // Si il n'y a qu'un seul argument (à savoir la cible) : Donner son statut à l'envoyeur.
            Tuple<bool, DateTime> profil = Utils.VérifierJoueurKick(cible.Id, Context.Guild.Id);
            if (args.Count == 1)
            {
                await Utils.EnvoyerEmbedUser(Context.User.Id, Configuration.CKick_Profil(Context, cible, profil));
                return;
            }

            // Si il y a deux arguments (cible et durée du kick) : Effectuer le kick.
            IKick cibleKick = null;
            List<IKick> kicks = Utils.ServeurAvecId(Context.Guild.Id).JoueursKick as List<IKick>;
            foreach (IKick kick in kicks)
            {
                if(kick.UserID == cible.Id)
                {
                    cibleKick = kick;
                    break;
                }
            }
            if (cibleKick != null)
            {
                kicks.Remove(cibleKick);
            }
            else if (durée == 0)
            {
                await Utils.EnvoyerEmbedUser(Context.User.Id, Configuration.CKick_CiblePasKick(Context, cible));
                return;
            }
            if (durée > 0)
            {
                if (durée > (ulong) int.MaxValue * 60)
                {
                    cibleKick = Factory.Instance.NouveauIKick(cible.Id, DateTime.Now + new TimeSpan((int)(durée / 1440), (int)(durée - durée / 1440), (int)(durée - (durée / 1440) - (durée - durée / 1440)), 0));
                }
                else if (durée > int.MaxValue)
                {
                    cibleKick = Factory.Instance.NouveauIKick(cible.Id, DateTime.Now + new TimeSpan((int)(durée / 60), (int)(durée - durée / 60), 0));
                }
                else
                {
                    cibleKick = Factory.Instance.NouveauIKick(cible.Id, DateTime.Now + new TimeSpan(0, (int)durée, 0));
                }
                kicks.Add(cibleKick);
                if (partie != null)
                {
                    partie.RetirerJoueur(cible.Id);
                    if (durée > 0)
                    {
                        await Utils.EnvoyerEmbedChannel(partie.Channel, Configuration.CKick_Notification(Context, cible));
                    }
                }
                await Utils.EnvoyerEmbedUser(Context.User.Id, Configuration.CKick_Succès(Context, cibleKick));
            }
            else
            {
                await Utils.EnvoyerEmbedUser(Context.User.Id, Configuration.CKick_Succès_Durée0(Context));
            }
            return;
        }
    }
}
