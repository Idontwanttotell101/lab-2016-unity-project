using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(RouteEditor))]
public class RouteEditorScript : Editor
{
    private ReorderableList list;

    private void OnEnable()
    {
        list = new ReorderableList(serializedObject,
                serializedObject.FindProperty("Transforms"),
                true, true, true, true);

        list.drawElementCallback =
    (Rect rect, int index, bool isActive, bool isFocused) =>
    {
        var element = list.serializedProperty.GetArrayElementAtIndex(index);
        rect.y += 2;

        EditorGUI.PropertyField(
            new Rect(rect.x, rect.y, rect.width-4, EditorGUIUtility.singleLineHeight),
            element, GUIContent.none,false);
    };
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        list.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }

    void OnSceneGUI()
    {
        // get the chosen game object
        RouteEditor t = target as RouteEditor;

        if (t == null)
            return;

        // iterate over game objects added to the array...
        //for (int i = 0; i < t.Routers.Count; i++)
        // {
        //    // ... and draw a line between them
        //     if (t.[i] != null)
        //        Handles.DrawLine(center, t.GameObjects[i].transform.position);
        // }
    }
}
