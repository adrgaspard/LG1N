using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Text;

namespace Core
{
    /// <summary>
    /// Classe qui sert pour la sérialisation des compositions.
    /// </summary>
    [DataContract (Name = "IComposition")]
    public class BuilderIComposition
    {
        [DataMember (EmitDefaultValue = true, IsRequired = true, Name = "Nom", Order = 1)]
        public string Nom { get; private set; }

        [DataMember(EmitDefaultValue = true, IsRequired = true, Name = "Rôles", Order = 2)]
        public ReadOnlyDictionary<Rôle, int> Rôles { get; private set; }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="nom">Nom de la composition.</param>
        /// <param name="rôles">Définition de la composition.</param>
        public BuilderIComposition(IComposition composition)
        {
            Nom = composition.Nom;
            Dictionary<Rôle, int> dico = new Dictionary<Rôle, int>();
            foreach(KeyValuePair<Rôle, int> pair in composition.Rôles)
            {
                dico.Add(pair.Key, pair.Value);
            }
            Rôles = new ReadOnlyDictionary<Rôle, int>(dico);
        }
    }
}
