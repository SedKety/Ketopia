using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AirshipMovement))]
public class AirshipMovementEnableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        AirshipMovement movement = (AirshipMovement)target;

        if (GUILayout.Button("EnableMovement"))
        {
            movement.EnableMovement();
        }
        if (GUILayout.Button("DisableMovement"))
        {
            movement.DisableMovement();
        }
    }
}
