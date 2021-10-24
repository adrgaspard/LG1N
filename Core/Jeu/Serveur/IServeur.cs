using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Core
{
    /// <summary>
    /// Interface qui contrôle les serveurs, et donc l'essemble des données.
    /// </summary>
    public interface IServeur : IEquatable<IServeur>
    {
        ulong GuildID { get; }
        Langue Langue { get; }
        IEnumerable<IPartie> Parties { get; }
        IPartieVocale PartieVocale { get; }
        IEnumerable<IComposition> Compositions { get; }
        IEnumerable<IRang> RangsAdministrateur { get; }
        IEnumerable<IRang> RangsStaff { get; }
        IEnumerable<IKick> JoueursKick { get; }

        /// <summary>
        /// Réécriture du protocole d'égalité.
        /// </summary>
        /// <param name="other">Objet à comparer.</param>
        /// <returns>Renvoi vrai si les deux objets sont identiques.</returns>
        bool Equals(object other);

        /// <summary>
        /// Réécriture du protocole d'égalité.
        /// </summary>
        /// <returns>Renvoi le HashCode de l'objet.</returns>
        int GetHashCode();

        /// <summary>
        /// Vérifie la validité de la partie vocale. Elle est supprimé si elle n'a plus lieu d'être.
        /// </summary>
        /// <param name="trigger">Source de la vérification.</param>
        void VérifierPartieVocale(string trigger);
    }
}
