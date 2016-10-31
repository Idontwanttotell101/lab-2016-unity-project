using UnityEngine;
using System.Collections;

public class BeamLauncher
{
    public static Beam CastBeam(Vector3 origin, Vector3 direction, float distance = 10, float DPS = 20, float duration = 4)
    {
        var resource = Resources.Load<Beam>("Attack/Beam");
        resource.Direction = direction;
        resource.Distance = distance;
        resource.DPS = DPS;
        resource.Duration = duration;
        var beam = GameObject.Instantiate(resource);
        beam.transform.position = origin;
        return beam;
    }
}
