using Core;
using Persistance;
using Réalisations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Root
{
    /// <summary>
    /// Classe de démarrage. Elle ne doit pas avoir d'autre utilité que d'instancier le Manager.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Méthode main, qui instancie le manager.
        /// </summary>
        /// <param name="args">Ne pas utiliser.</param>
        private static void Main(string[] args)
        {
            Core.Factory.Instance = new Réalisations.Factory();
            new Manager(new DataContractXML(Path.Combine(Directory.GetCurrentDirectory(), "../../../../Persistance/Données"), "Serveurs.xml", "Token.xml")).MainAsync().GetAwaiter().GetResult();
        }
    }
}
