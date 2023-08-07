using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class CombatHelper
{
    // Class multiplier
    public static float GetClassMultiplier(Class attackerClass, Class defenderClass)
    {
        if ((attackerClass == Class.Magic && defenderClass == Class.Melee) ||
            (attackerClass == Class.Melee && defenderClass == Class.Ranged) ||
            (attackerClass == Class.Ranged && defenderClass == Class.Magic))
            return 1.25f;
        else if ((attackerClass == Class.Melee && defenderClass == Class.Magic) ||
                 (attackerClass == Class.Ranged && defenderClass == Class.Melee) ||
                 (attackerClass == Class.Magic && defenderClass == Class.Ranged))
            return 0.75f;
        else
            return 1f;
    }

    // Element multiplier
    public static float GetElementMultiplier(Element attackerElement, Element defenderElement)
    {
        // Check if the defender element is None
        if (defenderElement == Element.None)
            return 1f;

        if ((attackerElement == Element.Fire && defenderElement == Element.Wind) ||
            (attackerElement == Element.Wind && defenderElement == Element.Earth) ||
            (attackerElement == Element.Earth && defenderElement == Element.Water) ||
            (attackerElement == Element.Water && defenderElement == Element.Fire))
            return 1.25f;
        else if ((attackerElement == Element.Wind && defenderElement == Element.Fire) ||
                 (attackerElement == Element.Earth && defenderElement == Element.Wind) ||
                 (attackerElement == Element.Water && defenderElement == Element.Earth) ||
                 (attackerElement == Element.Fire && defenderElement == Element.Water))
            return 0.75f;
        else
            return 1f;
    }
}

