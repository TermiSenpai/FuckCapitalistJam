using System;

public static class PlayerStress
{
    private static float stress = 0;
    public static event EventHandler ValueChanged;

    public static bool canModify = true;

    public static float Stress
    {
        get { return stress; }
        set
        {
            if (stress != value && canModify)
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
