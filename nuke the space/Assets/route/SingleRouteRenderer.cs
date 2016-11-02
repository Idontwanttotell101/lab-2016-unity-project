using UnityEngine;
using System.Collections;

public class SingleRouteRenderer : MonoBehaviour
{
    public Router Origin;
    public Router Target;
    public bool IsPortal = false;

    private static Material mat;
    private static Material hmat;
    private static bool MaterialLoaded;
    public static Material GetRouteMaterial (bool isPortal){
        if (!MaterialLoaded)
        {
            mat = Resources.Load<Material>("Route Material");
            hmat = Resources.Load<Material>("Hint Route Material");
        }
        return isPortal ? hmat : mat;
    }

    // Update is called once per frame
    void Update()
    {
        var diff = Target.transform.position - Origin.transform.position;
        var distance = diff.magnitude;
        this.transform.position = Origin.transform.position + diff / 2;
        this.transform.rotation = Quaternion.FromToRotation(Vector3.right, diff);
        this.transform.localScale = new Vector3(distance / 2, this.transform.localScale.y, this.transform.localScale.z);
        this.GetComponentInChildren<Renderer>().material = GetRouteMaterial(IsPortal);
    }
}
