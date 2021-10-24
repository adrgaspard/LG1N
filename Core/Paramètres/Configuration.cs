using Discord;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    /// <summary>
    /// Classe qui contrôle les constantes globales du bot.
    /// </summary>
    static partial class Configuration
    {
        // Partie technique ne pas toucher.
        internal static readonly string coreAssembly = "Core";

        // Randomizer
        internal static readonly Random Random = new Random();

        // Constantes globales
        internal static readonly string Version = "Version BETA-4";
        internal static readonly Tuple<string, string, ActivityType> Activité = Tuple.Create(Version, "", ActivityType.Playing);
        internal static readonly IEnumerable<string> Préfix = new List<string>() { "/" };
        internal static string Nom(Langue langue)
        {
            switch (langue)
            {
                case Langue.EN:
                    return "Werewolf for one night";
                case Langue.FR:
                    return "Loup-Garou pour une nuit";
                default:
                    throw new NotImplementedException();
            }
        }
        internal static readonly Tuple<int, int, int> CouleurInfo = Tuple.Create(4, 56, 140);
        internal static readonly Tuple<int, int, int> CouleurImportant = Tuple.Create(185, 16, 204);
        internal static readonly Tuple<int, int, int> CouleurAttention = Tuple.Create(255, 252, 79);
        internal static readonly Tuple<int, int, int> CouleurErreur = Tuple.Create(26, 26, 26);
    }
}
