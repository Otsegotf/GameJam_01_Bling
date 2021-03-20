using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AisleConstructor))]
public class AisleConstructorInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Collect"))
        {
            var targetAisle = target as AisleConstructor;
            targetAisle.MarksRenderers = targetAisle.GetComponentsInChildren<MeshRenderer>(true);
        }
    }
}