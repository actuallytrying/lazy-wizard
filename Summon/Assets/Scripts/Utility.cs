using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility : MonoBehaviour
{
    public static string FormatNumber(float number)
    {
        if (number < 1000)
        {
            // Anything less than a thousand
            return number.ToString("0");
        }
        else if (number >= 1000 && number < 1000000)
        {
            // Anything less than a million
            return (number / 1000).ToString("0.00K");
        }
        else if (number >= 1000000 && number < 1000000000)
        {
            // Anything less than a billion
            return (number / 1000000).ToString("0.00M");
        }
        else
        {
            // A billion or more
            return (number / 1000000000).ToString("0.00B");
        }
    }
}

