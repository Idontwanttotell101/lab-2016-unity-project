using UnityEngine;
using System.Linq;
using System;
using System.Collections.Generic;

public class RouteRenderer : MonoBehaviour
{
    class RouterPair
    {
        public Router lhs;
        public Router rhs;
        public bool hint;
    }

    class RouterPairComparer : IEqualityComparer<RouterPair>
    {
        bool IEqualityComparer<RouterPair>.Equals(RouterPair x, RouterPair y)
        {
            return
                x.hint == y.hint
                &&
                (
                (x.lhs == y.lhs && x.rhs == y.rhs)
                ||
                (x.lhs == y.rhs && x.rhs == y.lhs)
                );
        }

        int IEqualityComparer<RouterPair>.GetHashCode(RouterPair obj)
        {
            return (obj as object).GetHashCode();
        }
    }

    // Use this for initialization
    void Start()
    {
        var routers = FindObjectsOfType<Router>();
        var uniqueRoute = routers.SelectMany(x =>
        new RouterPair[]{
            new RouterPair() { lhs = x,rhs = x.Next, hint = x.IsForwardPortal },
            new RouterPair() { lhs = x,rhs = x.Prev, hint = x.IsBackPortal }
        })
        .Where(x => x.lhs != null && x.rhs != null)
        .GroupBy(x => x, new RouterPairComparer())
        .Select(x => x.First());

        SingleRouteRenderer rrenderer = Resources.Load<SingleRouteRenderer>("Route Renderer");

        foreach (var pair in uniqueRoute)
        {
            SingleRouteRenderer instance = Instantiate(rrenderer);
            instance.Origin = pair.lhs;
            instance.Target = pair.rhs;
            instance.IsPortal = pair.hint;
            instance.transform.parent = transform;
        }
    }
}
