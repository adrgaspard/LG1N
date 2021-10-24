using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    [Flags]
    public enum GameState : byte
    {
        AttenteDeJoueur = 1,
        Préparation = 2,
        Nuit = 4,
        Jour = 8,
        Finalisation = 16,
        Jeu = Nuit | Jour,
        Calcul = Préparation | Finalisation,
    }
}
