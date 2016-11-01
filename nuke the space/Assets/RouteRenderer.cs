using UnityEngine;
using System.Collections;

public class RouteRenderer : MonoBehaviour
{
    Router router;
    LineRenderer prevRoute;
    LineRenderer nextRoute;
    Material mat;
    Material hmat;

    // Use this for initialization
    void Start()
    {
        router = GetComponent<Router>();
        if (router == null) Destroy(this);

        mat = Resources.Load<Material>("Route Material");
        hmat = Resources.Load<Material>("Hint Route Material");

        if (router.Prev != null)
        {
            prevRoute = new GameObject("Prev Route", typeof(LineRenderer)).GetComponent<LineRenderer>();
            prevRoute.SetPositions(new Vector3[] { transform.position, transform.position });
            prevRoute.transform.parent = this.transform;
            prevRoute.material = router.IsBackPortal ? hmat : mat;
        }
        if (router.Next != null)
        {
            nextRoute = new GameObject("Next Route", typeof(LineRenderer)).GetComponent<LineRenderer>();
            nextRoute.SetPositions(new Vector3[] { transform.position, transform.position });
            nextRoute.transform.parent = this.transform;
            nextRoute.material = router.IsForwardPortal ? hmat : mat;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (router.Prev != null)
        {
            prevRoute.SetPosition(0, this.transform.position);
            prevRoute.SetPosition(1, router.Prev.transform.position);
        }
        if (router.Next != null)
        {
            nextRoute.SetPosition(0, this.transform.position);
            nextRoute.SetPosition(1, router.Next.transform.position);
        }

    }
}
