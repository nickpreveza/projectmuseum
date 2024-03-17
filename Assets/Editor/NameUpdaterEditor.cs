using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NameUpdater))]
[CanEditMultipleObjects]
public class NameUpdaterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        NameUpdater updater = (NameUpdater)target;
        if (GUILayout.Button("Update Name"))
        {
            updater.UpdateName();
        }

        DrawDefaultInspector();
    }
}
