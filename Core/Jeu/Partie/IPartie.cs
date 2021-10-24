using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Core
{
    /// <summary>
    /// Interface qui contrôle toutes les parties.
    /// </summary>
    public interface IPartie : IEquatable<IPartie>
    {
        ulong GuildID { get; }
        ulong Channel { get; }
        bool Automatique { get; }
        IEnumerable<IJoueur> JoueursEnAttente { get; }
        IEnumerable<IJoueur> JoueursEnJeu { get; }
        GameState Etat { get; }
        int Taille { get; }
        Tuple<Rôle, Rôle, Rôle> CartesRestantes { get; }
        DateTime DébutJour { get; }
        IReadOnlyDictionary<Rôle, int> Composition { get; }
        IEnumerable<string> CompteRendu { get; }

        /// <summary>
        /// Créé un dictionnaire contenant tout les rôles en Key, et 0 en Value.
        /// </summary>
        void InitialiserComposition();

        /// <summary>
        /// Ajoute un joueur dans la liste des joueurs en attente si cela est possible.
        /// </summary>
        /// <param name="userID"></param>
        void AjouterJoueur(ulong userID);

        /// <summary>
        /// Retire un joueur de la liste des joueurs en attente ou de la liste des joueurs en jeu si cela est possible.
        /// </summary>
        void RetirerJoueur(ulong userID);

        /// <summary>
        /// Cette méthode doit être appellée chaque fois que la liste des joueurs en attente est actualisée.
        /// Gère la phase d'attente de joueur, et détermine quand lancer une partie.
        /// </summary>
        void Chrono();

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
