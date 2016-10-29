using UnityEngine;
using System.Collections;
using System.Linq;

public class LineAttack : MonoBehaviour
{

    public Vector3 Direction;
    public float Distance;
    public float DamagePerPeriod = 1;
    public float DamagePeriod = 1;
    public float FirstDamageDelay = 0;

    void Start()
    {
        InvokeRepeating("DoAttack", FirstDamageDelay, DamagePeriod);
    }

    void OnDisable()
    {
        CancelInvoke("DoAttack");
    }

    //this function do a real attack detection and effect the HP component
    //this function is automatically called base on the configuration
    //there is no need to call this manually
    public void DoAttack()
    {
        var inrange = Physics.RaycastAll(this.transform.position, Direction, Distance);
        Debug.DrawLine(transform.position, transform.position + Direction.normalized * Distance);
        foreach (var hp in inrange.Select(x => x.collider.GetComponent<HP>()))
        {
            hp.Value -= DamagePerPeriod;
        }
    }
}
