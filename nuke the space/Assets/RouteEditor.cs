﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

[ExecuteInEditMode]
public class RouteEditor : MonoBehaviour
{
    public List<Router> Routers;
    public List<Transform> Transforms;

    void Update() {
        Routers =
            GetComponentsInChildren<Router>()
            .ToList();
        Transforms =
            Routers.Select(x => x.transform).ToList();
    }
}
