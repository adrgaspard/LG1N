using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    [Flags]
    public enum Rôle : long
    {
        Villageois = 1,
        LoupGarou = 2,
        Voyante = 4,
        Voleur = 8,
        Noiseuse = 16,
        Tanneur = 32,
        Soûlard = 64,
        Chasseur = 128,
        FrancMaçon = 256,
        Insomniaque = 512,
        Sbire = 1024,
        Doppelganger = 2048,
    }
}
