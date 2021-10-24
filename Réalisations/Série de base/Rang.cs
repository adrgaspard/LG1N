using Core;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Réalisations
{
    /// <summary>
    /// Réalisation basique de l'interface IRang. Cette classe devrait être la solution par défaut.
    /// </summary>
    class Rang : IRang
    {
        public ulong RôleID { get; private set; }

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

            return Equals(other as IRang);
        }

        /// <summary>
        /// Réécriture du protocole d'égalité.
        /// </summary>
        /// <param name="other">Objet à comparer.</param>
        /// <returns>Renvoi vrai si les deux objets sont identiques.</returns>
        public bool Equals(IRang other)
        {
            return RôleID == other.RôleID;
        }

        /// <summary>
        /// Réécriture du protocole d'égalité.
        /// </summary>
        /// <returns>Renvoi le HashCode de l'objet.</returns>
        public override int GetHashCode()
        {
            return Convert.ToInt32(RôleID);
        }

        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="rôleID">Id du rôle sur le serveur discord.</param>
        internal Rang(ulong rôleID)
        {
            RôleID = rôleID;
        }
    }
}
