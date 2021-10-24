using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    /// <summary>
    /// Classe abstraite qui permet d'instancier les autres, grâce à une Factory concrète dans Réalisations qu'elle ne connaît pas.
    /// </summary>
    public abstract class Factory
    {
        public static Factory Instance
        {
            get
            {
                return instance;
            }
            set
            {
                if(instance == null)
                {
                    instance = value;
                }
            }
        }
        private static Factory instance;

        /// <summary>
        /// Méthode basique pour instancier un IServeur.
        /// </summary>
        /// <param name="guildID">Id de la guild qui héberge le IServeur.</param>
        /// <returns>Renvoi un nouveau IServeur.</returns>
        public abstract IServeur NouveauIServeur(ulong guildID);

        /// <summary>
        /// Méthode de désérialisation d'un serveur.
        /// </summary>
        /// <param name="builder">Builder du IServeur.</param>
        /// <returns>Renvoi le IServeur opérationnel.</returns>
        public abstract IServeur NouveauIServeur(BuilderIServeur builder);

        /// <summary>
        /// Méthode basique pour instancier une IPartie.
        /// </summary>
        /// <param name="guildID">Id de la guild qui héberge la IPartie.</param>
        /// <param name="channel">Id du channel qui héberge la IPartie.</param>
        /// <returns>Renvoi une nouvelle IPartie.</returns>
        public abstract IPartie NouvelleIPartie(ulong guildID, ulong channel);

        /// <summary>
        /// Méthode de désérialisation d'une IPartie.
        /// </summary>
        /// <param name="builder">Builder de la IPartie.</param>
        /// <returns>Renvoi la IPartie opérationnelle.</returns>
        public abstract IPartie NouvelleIPartie(BuilderIPartie builder);

        /// <summary>
        /// Méthode basique pour instancier une IPartieVocale.
        /// </summary>
        /// <param name="guildID">Id de la guild qui héberge la IPartieVocale.</param>
        /// <param name="channel">Id du channel écrit qui héberge la IPartieVocale.</param>
        /// <param name="channelVocal">Id du channel oral qui héberge la IPartieVocale.</param>
        /// <returns>Renvoi une nouvelle IPartieVocale.</returns>
        public abstract IPartieVocale NouvelleIPartieVocale(ulong guildID, ulong channel, ulong channelVocal);

        /// <summary>
        /// Méthode de désérialisation d'une IPartieVocale.
        /// </summary>
        /// <param name="builder">Builder de la IPartieVocale.</param>
        /// <returns>Renvoi la IPartieVocale opérationnelle.</returns>
        public abstract IPartieVocale NouvelleIPartieVocale(BuilderIPartieVocale builder);

        /// <summary>
        /// Méthode basique pour instancier une IComposition.
        /// </summary>
        /// <param name="nom">Nom de la composition.</param>
        /// <returns>Renvoi une nouvelle IComposition.</returns>
        public abstract IComposition NouvelleIComposition(string nom);

        /// <summary>
        /// Méthode de désérialisation d'une IComposition.
        /// </summary>
        /// <param name="builder">Builder de la IComposition.</param>
        /// <returns>Renvoi la IComposition opérationnelle.</returns>
        public abstract IComposition NouvelleIComposition(BuilderIComposition builder);

        /// <summary>
        /// Méthode basique pour instancier un IJoueur.
        /// </summary>
        /// <param name="userID">Id d'utilisateur du joueur.</param>
        /// <param name="rôleInitial">Rôle inital du joueur (par défaut mis à 0 s'il n'en a pas).</param>
        /// <returns>Renvoi le nouveau IJoueur.</returns>
        public abstract IJoueur NouveauJoueur(ulong userID, Rôle rôleInitial = (Rôle)0);

        /// <summary>
        /// Méthode basique pour instancier un IKick.
        /// </summary>
        /// <param name="userID">Id de l'utilisateur à sanctionner.</param>
        /// <param name="sanction">Date de fin de la sanction.</param>
        /// <returns>Renvoi le nouveau IKick.</returns>
        public abstract IKick NouveauIKick(ulong userID, DateTime sanction);

        /// <summary>
        /// Méthode de désérialisation d'un IKick.
        /// </summary>
        /// <param name="builder">Builder du IKick.</param>
        /// <returns>Renvoi le IKick opérationnel si la sanction est toujours d'actualité, sinon renvoi null.</returns>
        public abstract IKick NouveauIKick(BuilderIKick builder);

        /// <summary>
        /// Méthode basique pour instancier un IRang.
        /// </summary>
        /// <param name="rôleID">Id de rôle du IRang.</param>
        /// <returns>Renvoi le nouveau IRang.</returns>
        public abstract IRang NouveauIRang(ulong rôleID);
    }
}
