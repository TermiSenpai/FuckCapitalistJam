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
            PlayerStress.Stress += stressBar.debugValue;
            stressBar.debug = PlayerStress.Stress;
        }
        if (GUILayout.Button("Decrease"))
        {
            PlayerStress.Stress -= stressBar.debugValue;
        }
    }
}
