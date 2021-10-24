using Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Réalisations
{
    /// <summary>
    /// Réalisation de l'interface IPartieVocale. Cette classe devrait être la solution par défaut.
    /// </summary>
    class PartieVocale : IPartieVocale
    {
        public ulong GuildID { get; private set; }
        public ulong Channel { get; private set; }
        public ulong ChannelVocal { get; private set; }
        public bool Automatique { get; internal set; }
        public IEnumerable<IJoueur> JoueursEnAttente { get; private set; }
        public IEnumerable<IJoueur> JoueursEnJeu { get; private set; }
        public GameState Etat { get; private set; }
        public int Taille
        {
            get
            {
                return (JoueursEnAttente as List<IJoueur>).Count;
            }
        }
        public Tuple<Rôle, Rôle, Rôle> CartesRestantes { get; private set; }
        public DateTime DébutJour { get; private set; }
        public IReadOnlyDictionary<Rôle, int> Composition { get; private set; }
        public IEnumerable<string> CompteRendu { get; private set; }

        /// <summary>
        /// Créé un dictionnaire contenant tout les rôles en Key, et 0 en Value.
        /// </summary>
        public void InitialiserComposition()
        {
            if (Etat == GameState.AttenteDeJoueur)
            {
                Dictionary<Rôle, int> composition = new Dictionary<Rôle, int>();
                foreach (Rôle rôle in Enum.GetValues(typeof(Rôle)))
                {
                    composition.Add(rôle, 0);
                }
                Composition = new ReadOnlyDictionary<Rôle, int>(composition);
            }
        }

        /// <summary>
        /// Ajoute un joueur dans la liste des joueurs en attente si cela est possible.
        /// </summary>
        /// <param name="userID"></param>
        public void AjouterJoueur(ulong userID)
        {
            foreach (IJoueur joueur in JoueursEnAttente)
            {
                if (joueur.UserID == userID)
                {
                    return;
                }
            }
            if (!(Utils.VérifierJoueurKick(userID, GuildID).Item1) && Utils.PartieDuJoueur(userID, GuildID) == null)
            {
                (JoueursEnAttente as List<IJoueur>).Add(new Joueur(userID));
                Chrono();
            }
        }

        /// <summary>
        /// Retire un joueur de la liste des joueurs en attente si cela est possible.
        /// </summary>
        public void RetirerJoueur(ulong userID)
        {
            foreach (IJoueur joueur in JoueursEnAttente)
            {
                if (joueur.UserID == userID)
                {
                    (JoueursEnAttente as List<IJoueur>).Remove(joueur);
                    return;
                }
            }
        }

        /// <summary>
        /// Cette méthode doit être appellée chaque fois que la liste des joueurs en attente est actualisée.
        /// Gère la phase d'attente de joueur, et détermine quand lancer une partie.
        /// </summary>
        public void Chrono()
        {

        }

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

            return Equals(other as IPartie);
        }

        /// <summary>
        /// Réécriture du protocole d'égalité.
        /// </summary>
        /// <param name="other">Objet à comparer.</param>
        /// <returns>Renvoi vrai si les deux objets sont identiques.</returns>
        public bool Equals(IPartie other)
        {
            if (GuildID == other.GuildID && Channel == other.Channel)
            {
                return Equals(other as IPartieVocale);
            }
            return false;
        }

        /// <summary>
        /// Réécriture du protocole d'égalité.
        /// </summary>
        /// <param name="other">Objet à comparer.</param>
        /// <returns>Renvoi vrai si les deux objets sont identiques.</returns>
        public bool Equals(IPartieVocale other)
        {
            return GuildID == other.GuildID && Channel == other.Channel && ChannelVocal == other.ChannelVocal;
        }

        /// <summary>
        /// Réécriture du protocole d'égalité.
        /// </summary>
        /// <returns>Renvoi le HashCode de l'objet.</returns>
        public override int GetHashCode()
        {
            return Convert.ToInt32(ChannelVocal);
        }

        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="guildID">Id de la guild dans laquelle la partie se trouve.</param>
        /// <param name="channel">Id du channel qui héberge la partie.</param>
        internal PartieVocale(ulong guildID, ulong channel, ulong channelVocal)
        {
            GuildID = guildID;
            Channel = channel;
            ChannelVocal = channelVocal;
            Automatique = true;
            JoueursEnAttente = new List<IJoueur>();
            JoueursEnJeu = new List<IJoueur>();
            Etat = GameState.AttenteDeJoueur;
            CartesRestantes = null;
            DébutJour = new DateTime();
            InitialiserComposition();
            CompteRendu = new List<string>();
        }
    }
}
