using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Linq;

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
        if (GUILayout.Button("create instance"))
        {
            Router prev = null;

            Undo.RegisterFullObjectHierarchyUndo(target, "create instances");
            var haschildren = (target as RouteEditor).GetComponentsInChildren<Transform>().Count() != 1;
            if (haschildren)
            {
                Debug.LogError("children already exist", target);
                return;
            }

            int index = -1;
            foreach (var p in (target as RouteEditor).positions)
            {
                ++index;
                var router = new GameObject();
                router.name = index.ToString();
                router.transform.parent = (target as RouteEditor).transform;
                router.transform.position = p;
                var component = router.AddComponent<Router>();
                component.Prev = prev;
                if (prev != null) { prev.Next = component; }
                prev = component;
            }
        }
        if (GUILayout.Button("clear instance"))
        {
            Undo.RegisterFullObjectHierarchyUndo(target, "destroy instances");
            var haschildren = (target as RouteEditor).GetComponentsInChildren<Transform>().Where(x => x.transform != (target as RouteEditor).transform).Select(x => x.gameObject).ToList();
            foreach (var c in haschildren)
            {
                GameObject.DestroyImmediate(c);
            }
        }
        if (GUILayout.Button("align view"))
        {
            //Undo.RegisterFullObjectHierarchyUndo(SceneView.lastActiveSceneView.camera, "align view"); //usually not undo-able in unity
            SceneView.lastActiveSceneView.camera.transform.rotation = Quaternion.FromToRotation(Vector3.forward, Vector3.down);
        }
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

            Handles.Label(t.positions[i], i.ToString());
            //Vector3 position = Handles.PositionHandle(t.positions[i], Quaternion.identity);
            Vector3 position = t.positions[i];
            Handles.color = Handles.xAxisColor;
            position = Handles.Slider(position, Vector3.right);
            Handles.color = Handles.zAxisColor;
            position = Handles.Slider(position, Vector3.forward);
            Handles.color = Handles.zAxisColor;
            position = Handles.FreeMoveHandle(position, Quaternion.identity, 0.5f, new Vector3(.5f, .5f, .5f), Handles.RectangleCap);
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
