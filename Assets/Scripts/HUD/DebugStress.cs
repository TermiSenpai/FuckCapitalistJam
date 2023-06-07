using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StressBar))]
public class DebugStress : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        StressBar stressBar = (StressBar)target;

        if (GUILayout.Button("Increase"))
        {
            PlayerStress.stress += stressBar.debugValue;

            if(PlayerStress.stress > 100)
                PlayerStress.stress = 100;

            stressBar.increaseStress(PlayerStress.stress);
        }
        if (GUILayout.Button("Decrease"))
        {
            PlayerStress.stress -= stressBar.debugValue;

            if (PlayerStress.stress <= 0 )
                PlayerStress.stress = 0;

            stressBar.decreaseStress(PlayerStress.stress);
        }
    }
}
