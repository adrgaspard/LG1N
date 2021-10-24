using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Core
{
    /// <summary>
    /// Cette partie de cette classe contrôle les constantes du bot liées au jeu en lui même.
    /// </summary>
    static partial class Configuration
    {
        // Règles du jeu
        internal static readonly int joueursMin = 4;
        internal static readonly int joueursMax = 15;
        internal static readonly TimeSpan duréeTimer = new TimeSpan(0, 0, 15);
        internal static readonly TimeSpan duréeJour = new TimeSpan(0, 15, 0);

        // Constantes de composition par défaut
        internal static readonly IReadOnlyDictionary<Rôle, int> composition4 = new ReadOnlyDictionary<Rôle, int>(new Dictionary<Rôle, int>()
        {
            [Rôle.Villageois] = 1,
            [Rôle.LoupGarou] = 2,
            [Rôle.Voyante] = 1,
            [Rôle.Voleur] = 1,
            [Rôle.Noiseuse] = 1,
            [Rôle.Tanneur] = 0,
            [Rôle.Soûlard] = 0,
            [Rôle.Chasseur] = 1,
            [Rôle.FrancMaçon] = 0,
            [Rôle.Insomniaque] = 0,
            [Rôle.Sbire] = 0,
            [Rôle.Doppelganger] = 0,
        });
        internal static readonly IReadOnlyDictionary<Rôle, int> composition5 = new ReadOnlyDictionary<Rôle, int>(new Dictionary<Rôle, int>()
        {
            [Rôle.Villageois] = 0,
            [Rôle.LoupGarou] = 2,
            [Rôle.Voyante] = 1,
            [Rôle.Voleur] = 1,
            [Rôle.Noiseuse] = 1,
            [Rôle.Tanneur] = 0,
            [Rôle.Soûlard] = 1,
            [Rôle.Chasseur] = 1,
            [Rôle.FrancMaçon] = 0,
            [Rôle.Insomniaque] = 1,
            [Rôle.Sbire] = 0,
            [Rôle.Doppelganger] = 0,
        });
        internal static readonly IReadOnlyDictionary<Rôle, int> composition6 = new ReadOnlyDictionary<Rôle, int>(new Dictionary<Rôle, int>()
        {
            [Rôle.Villageois] = 0,
            [Rôle.LoupGarou] = 2,
            [Rôle.Voyante] = 1,
            [Rôle.Voleur] = 1,
            [Rôle.Noiseuse] = 1,
            [Rôle.Tanneur] = 0,
            [Rôle.Soûlard] = 0,
            [Rôle.Chasseur] = 1,
            [Rôle.FrancMaçon] = 2,
            [Rôle.Insomniaque] = 0,
            [Rôle.Sbire] = 1,
            [Rôle.Doppelganger] = 0,
        });
        internal static readonly IReadOnlyDictionary<Rôle, int> composition7 = new ReadOnlyDictionary<Rôle, int>(new Dictionary<Rôle, int>()
        {
            [Rôle.Villageois] = 0,
            [Rôle.LoupGarou] = 2,
            [Rôle.Voyante] = 1,
            [Rôle.Voleur] = 1,
            [Rôle.Noiseuse] = 1,
            [Rôle.Tanneur] = 0,
            [Rôle.Soûlard] = 1,
            [Rôle.Chasseur] = 0,
            [Rôle.FrancMaçon] = 2,
            [Rôle.Insomniaque] = 1,
            [Rôle.Sbire] = 1,
            [Rôle.Doppelganger] = 0,
        });
        internal static readonly IReadOnlyDictionary<Rôle, int> composition8 = new ReadOnlyDictionary<Rôle, int>(new Dictionary<Rôle, int>()
        {
            [Rôle.Villageois] = 1,
            [Rôle.LoupGarou] = 2,
            [Rôle.Voyante] = 1,
            [Rôle.Voleur] = 1,
            [Rôle.Noiseuse] = 1,
            [Rôle.Tanneur] = 0,
            [Rôle.Soûlard] = 0,
            [Rôle.Chasseur] = 1,
            [Rôle.FrancMaçon] = 2,
            [Rôle.Insomniaque] = 0,
            [Rôle.Sbire] = 1,
            [Rôle.Doppelganger] = 1,
        });
        internal static readonly IReadOnlyDictionary<Rôle, int> composition9 = new ReadOnlyDictionary<Rôle, int>(new Dictionary<Rôle, int>()
        {
            [Rôle.Villageois] = 1,
            [Rôle.LoupGarou] = 2,
            [Rôle.Voyante] = 1,
            [Rôle.Voleur] = 1,
            [Rôle.Noiseuse] = 1,
            [Rôle.Tanneur] = 0,
            [Rôle.Soûlard] = 0,
            [Rôle.Chasseur] = 1,
            [Rôle.FrancMaçon] = 2,
            [Rôle.Insomniaque] = 1,
            [Rôle.Sbire] = 1,
            [Rôle.Doppelganger] = 1,
        });
        internal static readonly IReadOnlyDictionary<Rôle, int> composition10 = new ReadOnlyDictionary<Rôle, int>(new Dictionary<Rôle, int>()
        {
            [Rôle.Villageois] = 1,
            [Rôle.LoupGarou] = 2,
            [Rôle.Voyante] = 1,
            [Rôle.Voleur] = 1,
            [Rôle.Noiseuse] = 1,
            [Rôle.Tanneur] = 0,
            [Rôle.Soûlard] = 1,
            [Rôle.Chasseur] = 1,
            [Rôle.FrancMaçon] = 2,
            [Rôle.Insomniaque] = 1,
            [Rôle.Sbire] = 1,
            [Rôle.Doppelganger] = 1,
        });
        internal static readonly IReadOnlyDictionary<Rôle, int> composition11 = new ReadOnlyDictionary<Rôle, int>(new Dictionary<Rôle, int>()
        {
            [Rôle.Villageois] = 1,
            [Rôle.LoupGarou] = 2,
            [Rôle.Voyante] = 1,
            [Rôle.Voleur] = 1,
            [Rôle.Noiseuse] = 1,
            [Rôle.Tanneur] = 1,
            [Rôle.Soûlard] = 1,
            [Rôle.Chasseur] = 1,
            [Rôle.FrancMaçon] = 2,
            [Rôle.Insomniaque] = 1,
            [Rôle.Sbire] = 1,
            [Rôle.Doppelganger] = 1,
        });
        internal static readonly IReadOnlyDictionary<Rôle, int> composition12 = new ReadOnlyDictionary<Rôle, int>(new Dictionary<Rôle, int>()
        {
            [Rôle.Villageois] = 1,
            [Rôle.LoupGarou] = 2,
            [Rôle.Voyante] = 2,
            [Rôle.Voleur] = 1,
            [Rôle.Noiseuse] = 1,
            [Rôle.Tanneur] = 1,
            [Rôle.Soûlard] = 1,
            [Rôle.Chasseur] = 1,
            [Rôle.FrancMaçon] = 2,
            [Rôle.Insomniaque] = 1,
            [Rôle.Sbire] = 1,
            [Rôle.Doppelganger] = 1,
        });
        internal static readonly IReadOnlyDictionary<Rôle, int> composition13 = new ReadOnlyDictionary<Rôle, int>(new Dictionary<Rôle, int>()
        {
            [Rôle.Villageois] = 1,
            [Rôle.LoupGarou] = 2,
            [Rôle.Voyante] = 2,
            [Rôle.Voleur] = 1,
            [Rôle.Noiseuse] = 1,
            [Rôle.Tanneur] = 1,
            [Rôle.Soûlard] = 1,
            [Rôle.Chasseur] = 1,
            [Rôle.FrancMaçon] = 2,
            [Rôle.Insomniaque] = 2,
            [Rôle.Sbire] = 1,
            [Rôle.Doppelganger] = 1,
        });
        internal static readonly IReadOnlyDictionary<Rôle, int> composition14 = new ReadOnlyDictionary<Rôle, int>(new Dictionary<Rôle, int>()
        {
            [Rôle.Villageois] = 1,
            [Rôle.LoupGarou] = 3,
            [Rôle.Voyante] = 2,
            [Rôle.Voleur] = 1,
            [Rôle.Noiseuse] = 1,
            [Rôle.Tanneur] = 1,
            [Rôle.Soûlard] = 1,
            [Rôle.Chasseur] = 1,
            [Rôle.FrancMaçon] = 2,
            [Rôle.Insomniaque] = 2,
            [Rôle.Sbire] = 1,
            [Rôle.Doppelganger] = 1,
        });
        internal static readonly IReadOnlyDictionary<Rôle, int> composition15 = new ReadOnlyDictionary<Rôle, int>(new Dictionary<Rôle, int>()
        {
            [Rôle.Villageois] = 2,
            [Rôle.LoupGarou] = 3,
            [Rôle.Voyante] = 2,
            [Rôle.Voleur] = 1,
            [Rôle.Noiseuse] = 1,
            [Rôle.Tanneur] = 1,
            [Rôle.Soûlard] = 1,
            [Rôle.Chasseur] = 1,
            [Rôle.FrancMaçon] = 2,
            [Rôle.Insomniaque] = 2,
            [Rôle.Sbire] = 1,
            [Rôle.Doppelganger] = 1,
        });
    }
}
