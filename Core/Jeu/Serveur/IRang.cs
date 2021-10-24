using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Core
{
    /// <summary>
    /// Interface qui contrôle les rangs, destinés au système de permission.
    /// </summary>
    public interface IRang : IEquatable<IRang>
    {
        ulong RôleID { get; }

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
