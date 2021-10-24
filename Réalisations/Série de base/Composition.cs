using Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Schema;

namespace Réalisations
{
    /// <summary>
    /// Réalisation basique de l'interface IComposition. Cette classe devrait être la solution par défaut.
    /// </summary>
    class Composition : IComposition
    {
        public string Nom { get; private set; }
        public int Taille
        {
            get
            {
                if(Rôles != null)
                {
                    int taille = -3;
                    foreach(int i in Rôles.Values)
                    {
                        taille = taille + i;
                    }
                    if(taille > 0)
                    {
                        return taille;
                    }
                }
                return 0;
            }
        }
        public IReadOnlyDictionary<Rôle, int> Rôles { get; internal set; }

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

            return Equals(other as IComposition);
        }

        /// <summary>
        /// Réécriture du protocole d'égalité.
        /// </summary>
        /// <param name="other">Objet à comparer.</param>
        /// <returns>Renvoi vrai si les deux objets sont identiques.</returns>
        public bool Equals(IComposition other)
        {
            return Nom == other.Nom;
        }

        /// <summary>
        /// Réécriture du protocole d'égalité.
        /// </summary>
        /// <returns>Renvoi le HashCode de l'objet.</returns>
        public override int GetHashCode()
        {
            return Nom.GetHashCode();
        }

        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="nom">Nom de la composition, il doit être unique pour chaque serveur.</param>
        internal Composition(string nom)
        {
            Nom = nom;
            InitialiserComposition();
        }

        /// <summary>
        /// Initialise le dictionnaire en mettant toutes les valeurs qui n'existent pas à 0.
        /// </summary>
        public void InitialiserComposition()
        {
            Dictionary<Rôle, int> composition = new Dictionary<Rôle, int>();
            if (Rôles == null)
            {
                foreach (Rôle rôle in Enum.GetValues(typeof(Rôle)))
                {
                    composition.Add(rôle, 0);
                }
            }
            else
            {
                foreach(KeyValuePair<Rôle, int> pair in Rôles)
                {
                    composition.Add(pair.Key, pair.Value);
                }
                foreach (Rôle rôle in Enum.GetValues(typeof(Rôle)))
                {
                    if(!composition.ContainsKey(rôle))
                    {
                        composition.Add(rôle, 0);
                    }
                }
            }
            Rôles = new ReadOnlyDictionary<Rôle, int>(composition);
        }
    }
}
