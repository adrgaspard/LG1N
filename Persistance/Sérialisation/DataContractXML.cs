using Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace Persistance
{
    /// <summary>
    /// Réalisation de ISérialiseur. Cette classe devrait être la soluction par défaut.
    /// </summary>
    public class DataContractXML : ISérialiseur
    {
        private string WorkDir { get; set; }
        private string WorkFile { get; set; }
        private string TokenFile { get; set; }

        /// <summary>
        /// Trouve le token du client et le renvoi.
        /// </summary>
        /// <returns>Renvoi le token du client.</returns>
        public string ChargerToken()
        {
            Directory.SetCurrentDirectory(WorkDir);
            DataContractSerializer sérialiseur = new DataContractSerializer(typeof(string), new DataContractSerializerSettings() { PreserveObjectReferences = true });
            string token = "";
            if (File.Exists(TokenFile))
            {
                using (Stream reader = File.OpenRead(TokenFile))
                {
                    token = sérialiseur.ReadObject(reader) as string;
                }
                return token;
            }
            throw new Exception("Le token n'a pas pu être lu avec le DataContractXML.");
        }

        /// <summary>
        /// Charge les différents serveurs (guild) sur lesquels le bot est invité en mémoire et les renvoi.
        /// </summary>
        /// <returns>Renvoi les serveurs.</returns>
        public IEnumerable<IServeur> ChargerServeurs()
        {
            IEnumerable<IServeur> serveurs = new List<IServeur>();
            IEnumerable<BuilderIServeur> builders = new List<BuilderIServeur>();
            Directory.SetCurrentDirectory(WorkDir);
            DataContractSerializer sérialiseur = new DataContractSerializer(typeof(IEnumerable<BuilderIServeur>), new DataContractSerializerSettings() { PreserveObjectReferences = true });
            if (File.Exists(WorkFile))
            {
                using (Stream reader = File.OpenRead(WorkFile))
                {
                    builders = sérialiseur.ReadObject(reader) as IEnumerable<BuilderIServeur>;
                }
                foreach(BuilderIServeur builder in builders)
                {
                    (serveurs as List<IServeur>).Add(Factory.Instance.NouveauIServeur(builder));
                }
            }
            return serveurs;
        }

        /// <summary>
        /// Sauvegarde les différents serveurs (guild) sur lesquels le bot est invité.
        /// </summary>
        public void SauvegarderServeurs(IEnumerable<IServeur> serveurs)
        {
            IEnumerable<BuilderIServeur> builders = new List<BuilderIServeur>();
            foreach(IServeur serveur in serveurs)
            {
                (builders as List<BuilderIServeur>).Add(new BuilderIServeur(serveur));
            }
            Directory.SetCurrentDirectory(WorkDir);
            DataContractSerializer sérialiseur = new DataContractSerializer(typeof(IEnumerable<BuilderIServeur>), new DataContractSerializerSettings() { PreserveObjectReferences = true });
            XmlWriterSettings paramètres = new XmlWriterSettings() { Indent = true };
            using (XmlWriter writer = XmlWriter.Create(WorkFile, paramètres))
            {
                sérialiseur.WriteObject(writer, builders);
            }
        }

        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="workDir">Répertoire de travail. Ce dernier doit être "/Persistance/Données"</param>
        /// <param name="workFile">Fichier de travail. Ce dernier doit se terminer par ".xml".</param>
        /// <param name="tokenFile">Fichier qui contient le token. Ce dernier doit se terminer par ".xml".</param>
        public DataContractXML(string workDir, string workFile, string tokenFile)
        {
            WorkDir = workDir;
            WorkFile = workFile;
            TokenFile = tokenFile;
        }
    }
}