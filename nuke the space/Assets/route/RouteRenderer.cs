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
    }

    class RouterPairComparer : IEqualityComparer<RouterPair>
    {
        bool IEqualityComparer<RouterPair>.Equals(RouterPair x, RouterPair y)
        {
            return
                (x.lhs == y.lhs && x.rhs == y.rhs)
                || (x.lhs == y.rhs && x.rhs == y.lhs);
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
            new RouterPair() { lhs = x,rhs = x.Next },
            new RouterPair() { lhs = x,rhs = x.Prev }
        })
        .Where(x => x.lhs != null && x.rhs != null)
        .GroupBy(x => x, new RouterPairComparer());

        foreach (var pair in uniqueRoute) {
            throw new NotImplementedException("Go To Class");
        }
    }
}
