using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Core
{
    /// <summary>
    /// Classe qui sert pour la sérialisation des serveurs.
    /// </summary>
    [DataContract (Name = "IServeur")]
    public class BuilderIServeur
    {
        [DataMember(EmitDefaultValue = true, IsRequired = true, Name = "GuildID", Order = 1)]
        public ulong GuildID { get; private set; }

        [DataMember(EmitDefaultValue = true, IsRequired = true, Name = "Langue", Order = 2)]
        public Langue Langue { get; private set; }

        [DataMember(EmitDefaultValue = true, IsRequired = true, Name = "Parties", Order = 3)]
        public IEnumerable<BuilderIPartie> Parties { get; private set; }

        [DataMember(EmitDefaultValue = true, IsRequired = true, Name = "PartieVocale", Order = 4)]
        public BuilderIPartieVocale PartieVocale { get; private set; }

        [DataMember(EmitDefaultValue = true, IsRequired = true, Name = "Compositions", Order = 5)]
        public IEnumerable<BuilderIComposition> Compositions { get; private set; }

        [DataMember(EmitDefaultValue = true, IsRequired = true, Name = "RangsAdministrateur", Order = 6)]
        public IEnumerable<ulong> RangsAdministrateur { get; private set; }

        [DataMember(EmitDefaultValue = true, IsRequired = true, Name = "RangsStaff", Order = 7)]
        public IEnumerable<ulong> RangsStaff { get; private set; }

        [DataMember(EmitDefaultValue = true, IsRequired = true, Name = "JoueursKick", Order = 8)]
        public IEnumerable<BuilderIKick> JoueursKick { get; private set; }

        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="serveur">Serveur à sérialiser.</param>
        public BuilderIServeur(IServeur serveur)
        {
            GuildID = serveur.GuildID;
            Langue = serveur.Langue;
            Parties = new List<BuilderIPartie>();
            foreach(IPartie partie in serveur.Parties)
            {
                (Parties as List<BuilderIPartie>).Add(new BuilderIPartie(partie));
            }
            if(serveur.PartieVocale != null)
            {
                PartieVocale = new BuilderIPartieVocale(serveur.PartieVocale);
            }
            Compositions = new List<BuilderIComposition>();
            foreach(IComposition composition in serveur.Compositions)
            {
                (Compositions as List<BuilderIComposition>).Add(new BuilderIComposition(composition));
            }
            RangsAdministrateur = new List<ulong>();
            foreach(IRang rang in serveur.RangsAdministrateur)
            {
                (RangsAdministrateur as List<ulong>).Add(rang.RôleID);
            }
            RangsStaff = new List<ulong>();
            foreach (IRang rang in serveur.RangsStaff)
            {
                (RangsStaff as List<ulong>).Add(rang.RôleID);
            }
            JoueursKick = new List<BuilderIKick>();
            foreach(IKick kick in serveur.JoueursKick)
            {
                (JoueursKick as List<BuilderIKick>).Add(new BuilderIKick(kick));
            }
        }

    }
}
