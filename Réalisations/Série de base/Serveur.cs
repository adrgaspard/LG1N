using Core;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Réalisations
{
    /// <summary>
    /// Réalisation basique de l'interface IServeur. Cette classe devrait être la solution par défaut.
    /// </summary>
    class Serveur : IServeur
    {
        public ulong GuildID { get; private set; }
        public Langue Langue { get; internal set; }
        public IEnumerable<IPartie> Parties { get; private set; }
        public IPartieVocale PartieVocale { get; internal set; }
        public IEnumerable<IComposition> Compositions { get; private set; }
        public IEnumerable<IRang> RangsAdministrateur { get; private set; }
        public IEnumerable<IRang> RangsStaff { get; private set; }
        public IEnumerable<IKick> JoueursKick { get; private set; }

        /// <summary>
        /// Réécriture du protocole d'égalité.
        /// </summary>
        /// <param name="other">Objet à comparer.</param>
        /// <returns>Renvoi vrai si les deux objets sont identiques.</returns>
        public override bool Equals(object other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (GetType() != other.GetType())
            {
                return false;
            }

            return Equals(other as IServeur);
        }

        /// <summary>
        /// Réécriture du protocole d'égalité.
        /// </summary>
        /// <param name="other">Objet à comparer.</param>
        /// <returns>Renvoi vrai si les deux objets sont identiques.</returns>
        public bool Equals(IServeur other)
        {
            return GuildID == other.GuildID;
        }

        /// <summary>
        /// Réécriture du protocole d'égalité.
        /// </summary>
        /// <returns>Renvoi le HashCode de l'objet.</returns>
        public override int GetHashCode()
        {
            return Convert.ToInt32(GuildID);
        }

        /// <summary>
        /// Vérifie la validité de la partie vocale. Elle est supprimé si elle n'a plus lieu d'être (par exemple si un de ses channels a été supprimé).
        /// </summary>
        /// /// <param name="trigger">Source de la vérification.</param>
        public void VérifierPartieVocale(string trigger)
        {
            if(PartieVocale != null)
            {
                if (Utils.ChannelAvecId(PartieVocale.Channel) == null || Utils.ChannelAvecId(PartieVocale.ChannelVocal) == null)
                {
                    PartieVocale = null;
                    Console.WriteLine($"[Info : {DateTime.Now} : System] IPartieVocale supprimée ({trigger}). GuildID : {GuildID}, ChannelID : {PartieVocale.Channel}, ChannelVocalID : {PartieVocale.ChannelVocal}.");
                }
            }
        }

        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="guildID">ID de la guild qu'il représente.</param>
        internal Serveur(ulong guildID)
        {
            GuildID = guildID;
            Langue = Langue.EN;
            Parties = new List<IPartie>();
            PartieVocale = null;
            Compositions = new List<IComposition>();
            RangsAdministrateur = new List<IRang>();
            RangsStaff = new List<IRang>();
            JoueursKick = new List<IKick>();
        }
    }
}
