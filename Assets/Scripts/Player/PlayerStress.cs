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
                value = checkValue(value);                

                stress = value;
                OnValueChanged(); // Llamamos al evento cuando el valor cambia
            }
        }
    }

    private static float checkValue(float setValue)
    {
        if (setValue > 100f)
            return 100f;
        else if (setValue < 0f)
             return 0f;

        return setValue;
    }

    private static void OnValueChanged()
    {
        ValueChanged?.Invoke(null, EventArgs.Empty);
    }
}
