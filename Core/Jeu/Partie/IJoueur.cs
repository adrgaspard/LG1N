using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Core
{
    /// <summary>
    /// Interface qui contrôle les joueurs.
    /// </summary>
    public interface IJoueur : IEquatable<IJoueur>
    {
        ulong UserID { get; }
        Rôle RôleInitial { get; }
        Rôle RôleFinal { get; }
        IJoueur Vote { get; }

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
    }
}
