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
                serializedObject.FindProperty("positions"),
                true, true, true, true);

        list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            var element = list.serializedProperty.GetArrayElementAtIndex(index);
            rect.y += 2;

            EditorGUI.PropertyField(
                new Rect(rect.x, rect.y, rect.width - 4, EditorGUIUtility.singleLineHeight),
                element, GUIContent.none, false);
        };
        list.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(rect, "Routers");
        };

    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        //EditorGUILayout.ObjectField("RouteEditorScript.cs",this,typeof(RouteEditorScript),false);
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

        for (int i = 0; i < t.positions.Count; ++i)
        {
            EditorGUI.BeginChangeCheck();

            Handles.Label(t.positions[i], "LABLE");
            Vector3 position = Handles.PositionHandle(t.positions[i], Quaternion.identity);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Changed Router Position");
                t.positions[i] = position;
            }
        }

        for (int i = 1; i < t.positions.Count; ++i)
        {
            Handles.color = Color.cyan;
            Handles.DrawLine(t.positions[i], t.positions[i - 1]);
        }
    }
}
