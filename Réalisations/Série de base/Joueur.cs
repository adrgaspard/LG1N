using Core;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Réalisations
{
    /// <summary>
    /// Réalisation basique de l'interface IJoueur. Cette classe devrait être la solution par défaut.
    /// </summary>
    class Joueur : IJoueur
    {
        public ulong UserID { get; private set; }
        public Rôle RôleInitial { get; private set; }
        public Rôle RôleFinal { get; private set; }
        public IJoueur Vote { get; private set; }

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

            return Equals(other as IJoueur);
        }

        /// <summary>
        /// Réécriture du protocole d'égalité.
        /// </summary>
        /// <param name="other">Objet à comparer.</param>
        /// <returns>Renvoi vrai si les deux objets sont identiques.</returns>
        public bool Equals(IJoueur other)
        {
            return UserID == other.UserID;
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
        /// Constructeur.
        /// </summary>
        /// <param name="userID">Id d'utilisateur Discord du joueur.</param>
        /// <param name="rôleInitial">Rôle initial du joueur.</param>
        internal Joueur(ulong userID, Rôle rôleInitial = 0)
        {
            UserID = userID;
            RôleInitial = rôleInitial;
            RôleFinal = 0;
            Vote = null;
        }
    }
}
