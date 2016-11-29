using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

[ExecuteInEditMode]
public class RouteEditor : MonoBehaviour
{
    //public List<Router> Routers;
    //public List<Transform> Transforms;
    public List<Vector3> positions;
    [HideInInspector]
    public List<float> enemies;
    public float position;

    public Vector3 GetPos(float pos_persent)
    {
        var pos = position;
        List<float> accu = new List<float>();
        accu.Add(0);
        for (int i = 1; i < positions.Count; ++i)
        {
            accu.Add(accu[i - 1] + Vector3.Distance(positions[i], positions[i - 1]));
        }
        pos *= accu.Last();

        var last = accu.Last(x => x <= pos);
        var index = accu.LastIndexOf(last);
        var delta = pos - last;
        var curr = positions[index];
        var next = positions[index + 1];
        var diff = next - curr;

        if (index == positions.Count) return positions.Last();
        return curr + diff * delta / diff.magnitude;
    }

    //void Update() {
    //    Routers =
    //        GetComponentsInChildren<Router>()
    //       .ToList();
    //   Transforms =
    //        Routers.Select(x => x.transform).ToList();
    //}
}
