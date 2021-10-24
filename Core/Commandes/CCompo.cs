using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class CCompo : ModuleBase<SocketCommandContext>
    {
        [Command("compo"), Summary("Détailler une certaine composition.")]
        public async Task Commande([Remainder] string input = "")
        {
            // Initialisation
            IServeur serveur = Utils.ServeurAvecId(Context.Guild.Id);
            List<string> args = Utils.SéparationArguments(input) as List<string>;
            IComposition composition = null;
            await Context.Message.DeleteAsync();

            // Vérification du bon nombre d'argument : == 1
            if(args.Count != 1)
            {
                return;
            }

            // Vérification de la IComposition : Existe bien
            foreach(IComposition compo in serveur.Compositions)
            {
                if (compo.Nom == args.ElementAt(0))
                {
                    composition = compo;
                    break;
                }
            }
            if(composition == null)
            {
                return;
            }

            // Affichage de la IComposition
            //await Utils.EnvoyerEmbedUser(Context.User.Id, Configuration.CCompo(Context, composition));
            return;
        }
    }
}

