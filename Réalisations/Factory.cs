using Core;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace Réalisations
{
    /// <summary>
    /// Réalisation de la Factory de l'assembly Core.
    /// </summary>
    public class Factory : Core.Factory
    {
        /// <summary>
        /// Méthode basique pour instancier un IServeur.
        /// </summary>
        /// <param name="guildID">Id de la guild qui héberge le IServeur.</param>
        /// <returns>Renvoi un nouveau IServeur.</returns>
        public override IServeur NouveauIServeur(ulong guildID)
        {
            return new Serveur(guildID);
        }

        /// <summary>
        /// Méthode de désérialisation d'un serveur.
        /// </summary>
        /// <param name="builder">Builder du IServeur.</param>
        /// <returns>Renvoi le IServeur opérationnel.</returns>
        public override IServeur NouveauIServeur(BuilderIServeur builder)
        {
            Serveur serveur = new Serveur(builder.GuildID);
            serveur.Langue = builder.Langue;
            foreach (BuilderIPartie builderIPartie in builder.Parties)
            {
                (serveur.Parties as List<IPartie>).Add(NouvelleIPartie(builderIPartie));
            }
            serveur.PartieVocale = NouvelleIPartieVocale(builder.PartieVocale);
            foreach (BuilderIComposition builderIComposition in builder.Compositions)
            {
                (serveur.Compositions as List<IComposition>).Add(NouvelleIComposition(builderIComposition));
            }
            foreach (ulong rang in builder.RangsAdministrateur)
            {
                (serveur.RangsAdministrateur as List<IRang>).Add(NouveauIRang(rang));
            }
            foreach (ulong rang in builder.RangsStaff)
            {
                (serveur.RangsStaff as List<IRang>).Add(NouveauIRang(rang));
            }
            foreach (BuilderIKick builderIKick in builder.JoueursKick)
            {
                if (builderIKick.Sanction > DateTime.Now)
                {
                    (serveur.JoueursKick as List<IKick>).Add(NouveauIKick(builderIKick));
                }
            }
            return serveur;
        }

        /// <summary>
        /// Méthode basique pour instancier une IPartie.
        /// </summary>
        /// <param name="guildID">Id de la guild qui héberge la IPartie.</param>
        /// <param name="channel">Id du channel qui héberge la IPartie.</param>
        /// <returns>Renvoi une nouvelle IPartie.</returns>
        public override IPartie NouvelleIPartie(ulong guildID, ulong channel)
        {
            return new Partie(guildID, channel);
        }

        /// <summary>
        /// Méthode de désérialisation d'une IPartie.
        /// </summary>
        /// <param name="builder">Builder de la IPartie.</param>
        /// <returns>Renvoi la IPartie opérationnelle.</returns>
        public override IPartie NouvelleIPartie(BuilderIPartie builder)
        {
            Partie partie = new Partie(builder.GuildID, builder.Channel);
            partie.Automatique = builder.Automatique;
            return partie;
        }

        /// <summary>
        /// Méthode basique pour instancier une IPartieVocale.
        /// </summary>
        /// <param name="guildID">Id de la guild qui héberge la IPartieVocale.</param>
        /// <param name="channel">Id du channel écrit qui héberge la IPartieVocale.</param>
        /// <param name="channelVocal">Id du channel oral qui héberge la IPartieVocale.</param>
        /// <returns>Renvoi une nouvelle IPartieVocale.</returns>
        public override IPartieVocale NouvelleIPartieVocale(ulong guildID, ulong channel, ulong channelVocal)
        {
            return new PartieVocale(guildID, channel, channelVocal);
        }

        /// <summary>
        /// Méthode de désérialisation d'une IPartieVocale.
        /// </summary>
        /// <param name="builder">Builder de la IPartieVocale.</param>
        /// <returns>Renvoi la IPartieVocale opérationnelle.</returns>
        public override IPartieVocale NouvelleIPartieVocale(BuilderIPartieVocale builder)
        {
            if(builder == null)
            {
                return null;
            }
            PartieVocale partie = new PartieVocale(builder.GuildID, builder.Channel, builder.ChannelVocal);
            partie.Automatique = builder.Automatique;
            return partie;
        }

        /// <summary>
        /// Méthode basique pour instancier une IComposition.
        /// </summary>
        /// <param name="nom">Nom de la composition.</param>
        /// <returns>Renvoi une nouvelle IComposition.</returns>
        public override IComposition NouvelleIComposition(string nom)
        {
            return new Composition(nom);
        }

        /// <summary>
        /// Méthode de désérialisation d'une IComposition.
        /// </summary>
        /// <param name="builder">Builder de la IComposition.</param>
        /// <returns>Renvoi la IComposition opérationnelle.</returns>
        public override IComposition NouvelleIComposition(BuilderIComposition builder)
        {
            Composition composition = new Composition(builder.Nom);
            composition.Rôles = builder.Rôles;
            return composition;
        }

        /// <summary>
        /// Méthode basique pour instancier un IJoueur.
        /// </summary>
        /// <param name="userID">Id d'utilisateur du joueur.</param>
        /// <param name="rôleInitial">Rôle inital du joueur (par défaut mis à 0 s'il n'en a pas).</param>
        /// <returns>Renvoi le nouveau IJoueur.</returns>
        public override IJoueur NouveauJoueur(ulong userID, Rôle rôleInitial = (Rôle)0)
        {
            return new Joueur(userID, rôleInitial);
        }

        /// <summary>
        /// Méthode basique pour instancier un IKick.
        /// </summary>
        /// <param name="userID">Id de l'utilisateur à sanctionner.</param>
        /// <param name="sanction">Date de fin de la sanction.</param>
        /// <returns>Renvoi le nouveau IKick.</returns>
        public override IKick NouveauIKick(ulong userID, DateTime sanction)
        {
            if(sanction <= DateTime.Now)
            {
                throw new Exception("La fin de la sanction ne peut pas être inférieure à la date actuelle.");
            }
            return new Kick(userID, sanction);
        }

        /// <summary>
        /// Méthode de désérialisation d'un IKick.
        /// </summary>
        /// <param name="builder">Builder du IKick.</param>
        /// <returns>Renvoi le IKick opérationnel si la sanction est toujours d'actualité, sinon renvoi null.</returns>
        public override IKick NouveauIKick(BuilderIKick builder)
        {
            if(DateTime.Now < builder.Sanction)
            {
                return new Kick(builder.UserID, builder.Sanction);
            }
            return null;
        }

        /// <summary>
        /// Méthode basique pour instancier un IRang.
        /// </summary>
        /// <param name="rôleID">Id de rôle du IRang.</param>
        /// <returns>Renvoi le nouveau IRang.</returns>
        public override IRang NouveauIRang(ulong rôleID)
        {
            return new Rang(rôleID);
        }
    }
}
