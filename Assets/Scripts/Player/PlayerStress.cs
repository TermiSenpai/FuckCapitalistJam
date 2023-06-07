using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStress
{
    private static float stress = 0;
    public static event EventHandler ValueChanged;

    public static float Stress
    {
        get { return stress; }
        set
        {
            if (stress != value)
            {
                stress = value;
                OnValueChanged(); // Llamamos al evento cuando el valor cambia
            }
        }
    }
    private static void OnValueChanged()
    {
        ValueChanged?.Invoke(null, EventArgs.Empty);
    }
}
