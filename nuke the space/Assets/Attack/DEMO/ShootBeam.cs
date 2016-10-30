using UnityEngine;
using System.Collections;

public class ShootBeam : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var p = Input.mousePosition;
            p.z = 10;
            p = Camera.main.ScreenToWorldPoint(p);

            Vector3 begin = Camera.main.transform.position;
            Vector3 direction = p - begin;

            var scale = -begin.y / direction.y;

            //hit on x-z plane
            var hit = begin + direction * scale;

            Debug.Log(hit);
            Debug.DrawRay(transform.position, direction);

            BeamLauncher.CastBeam(transform.position, hit - transform.position, 10);
        }
    }
}
