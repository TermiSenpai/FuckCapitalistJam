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
            stressBar.increaseStress(stressBar.debugValue);
        }
        if (GUILayout.Button("Decrease"))
        {
            stressBar.decreaseStress(stressBar.debugValue);
        }
    }
}
