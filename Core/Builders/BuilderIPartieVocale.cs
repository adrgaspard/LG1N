using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Core
{
    /// <summary>
    /// Classe qui sert pour la sérialisation des parties vocales.
    /// </summary>
    [DataContract (Name = "IPartieVocale")]
    public class BuilderIPartieVocale : BuilderIPartie
    {
        [DataMember(EmitDefaultValue = true, IsRequired = true, Name = "ChannelVocal", Order = 3)]
        public ulong ChannelVocal { get; private set; }

        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="partieVocale">Partie vocale à sérialiser.</param>
        public BuilderIPartieVocale(IPartieVocale partieVocale)
        {
            GuildID = partieVocale.GuildID;
            Channel = partieVocale.Channel;
            ChannelVocal = partieVocale.ChannelVocal;
            Automatique = partieVocale.Automatique;
        }
    }
}
