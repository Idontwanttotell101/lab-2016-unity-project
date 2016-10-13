using UnityEngine;
using System.Collections;

public class ShootBeam : MonoBehaviour
{
    public GameObject beam;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var v3 = Input.mousePosition;
            v3.z = 10;
            v3 = Camera.main.ScreenToWorldPoint(v3);
            v3.z = 0;
            v3.Normalize();
            Instantiate(beam, transform.position, Quaternion.FromToRotation(Vector3.right, v3));
        }
    }
}
