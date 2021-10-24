using Core;
using System;
using System.Collections.Generic;

namespace Persistance
{
    /// <summary>
    /// Réalisation de ISérialiseur. Cette classe ne doit être utilisé qu'en cas de test/debug ou de développement.
    /// </summary>
    public class Stub : ISérialiseur
    {
        /// <summary>
        /// Trouve le token du client et le renvoi.
        /// </summary>
        /// <returns>Renvoi le token du client.</returns>
        public string ChargerToken()
        {
            return "NTkzMTA0ODY4MTUyMDQ5NjY2.XRJJ6A.Mto1j3LVpxix-Lgdcae5X8FaJoc";
        }

        /// <summary>
        /// Charge les différents serveurs (guild) sur lesquels le bot est invité en mémoire et les renvoi.
        /// </summary>
        /// <returns>Renvoi les serveurs.</returns>
        public IEnumerable<IServeur> ChargerServeurs()
        {
            IServeur serveur = Factory.Instance.NouveauIServeur(593087279950725150);
            (serveur.Parties as List<IPartie>).Add(Factory.Instance.NouvelleIPartie(serveur.GuildID, 593188179046170625));
            (serveur.RangsAdministrateur as List<IRang>).Add(Factory.Instance.NouveauIRang(593089612583403547));
            (serveur.RangsStaff as List<IRang>).Add(Factory.Instance.NouveauIRang(593089729457553442));
            return new List<IServeur>() { serveur };
        }

        /// <summary>
        /// Sauvegarde les différents serveurs (guild) sur lesquels le bot est invité.
        /// N'a pas à être implémentée pour le Stub.
        /// </summary>
        public void SauvegarderServeurs(IEnumerable<IServeur> serveurs)
        {
            throw new NotImplementedException();
        }
    }
}
