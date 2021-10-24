using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Core
{
    /// <summary>
    /// Classe qui sert pour la sérialisation des serveurs.
    /// </summary>
    [DataContract (Name = "IKick")]
    public class BuilderIKick
    {
        [DataMember(EmitDefaultValue = true, IsRequired = true, Name = "UserID", Order = 1)]
        public ulong UserID { get; private set; }

        [DataMember(EmitDefaultValue = true, IsRequired = true, Name = "Sanction", Order = 2)]
        public DateTime Sanction { get; private set; }

        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="kick">Kick à sérialiser.</param>
        public BuilderIKick(IKick kick)
        {
            UserID = kick.UserID;
            Sanction = kick.Sanction;
        }
    }
}