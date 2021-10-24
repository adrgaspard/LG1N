using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Core
{
    /// <summary>
    /// Interface qui contrôle les compositions d'une partie (répartition des rôles).
    /// </summary>
    public interface IComposition
    {
        string Nom { get; }
        int Taille { get; }
        IReadOnlyDictionary<Rôle, int> Rôles { get; }

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
        /// Initialise le dictionnaire en mettant toutes les valeurs qui n'existent pas à 0.
        /// </summary>
        void InitialiserComposition();
    }
}
