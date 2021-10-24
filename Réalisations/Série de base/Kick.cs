using Core;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Réalisations
{
    /// <summary>
    /// Réalisation de l'interface IKick. Cette classe devrait être la solution par défaut.
    /// </summary>
    class Kick : IKick
    {
        public ulong UserID { get; private set; }
        public DateTime Sanction { get; private set; }

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

            return Equals(other as IKick);
        }

        /// <summary>
        /// Réécriture du protocole d'égalité.
        /// </summary>
        /// <param name="other">Objet à comparer.</param>
        /// <returns>Renvoi vrai si les deux objets sont identiques.</returns>
        public bool Equals(IKick other)
        {
            return UserID == other.UserID && Sanction.Equals(other.Sanction);
        }

        /// <summary>
        /// Réécriture du protocole d'égalité.
        /// </summary>
        /// <returns>Renvoi le HashCode de l'objet.</returns>
        public override int GetHashCode()
        {
            return Convert.ToInt32(UserID);
        }

        /// <summary>
        /// Contructeur.
        /// </summary>
        /// <param name="userID">Id de l'utilisateur à sanctionner.</param>
        /// <param name="sanction">Date de fin de la sanction.</param>
        internal Kick(ulong userID, DateTime sanction)
        {
            UserID = userID;
            Sanction = sanction;
        }
    }
}
