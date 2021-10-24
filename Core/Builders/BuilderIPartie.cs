using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Core
{
    /// <summary>
    /// Classe qui sert pour la sérialisation des parties.
    /// </summary>
    [DataContract (Name = "IPartie")]
    public class BuilderIPartie
    {
        [DataMember(EmitDefaultValue = true, IsRequired = true, Name = "GuildID", Order = 1)]
        public ulong GuildID { get; protected set; }

        [DataMember(EmitDefaultValue = true, IsRequired = true, Name = "Channel", Order = 2)]
        public ulong Channel { get; protected set; }

        [DataMember(EmitDefaultValue = true, IsRequired = true, Name = "Automatique", Order = 4)]
        public bool Automatique { get; protected set; }

        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="partie">Partie vocale à sérialiser.</param>
        public BuilderIPartie(IPartie partie)
        {
            GuildID = partie.GuildID;
            Channel = partie.Channel;
            Automatique = partie.Automatique;
        }

        /// <summary>
        /// Constructeur mis en place pour la classe BuilderIPartieVocale.
        /// </summary>
        protected BuilderIPartie() { }
    }
}
