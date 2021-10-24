using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    /// <summary>
    /// Interface qui contrôle la sérialisation des données.
    /// </summary>
    public interface ISérialiseur
    {
        /// <summary>
        /// Trouve le token du client et le renvoi.
        /// </summary>
        /// <returns>Renvoi le token du client.</returns>
        string ChargerToken();

        /// <summary>
        /// Charge les différents serveurs (guild) sur lesquels le bot est invité en mémoire et les renvoi.
        /// </summary>
        /// <returns>Renvoi les serveurs.</returns>
        IEnumerable<IServeur> ChargerServeurs();

        /// <summary>
        /// Sauvegarde les différents serveurs (guild) sur lesquels le bot est invité.
        /// </summary>
        void SauvegarderServeurs(IEnumerable<IServeur> serveurs);
    }
}
