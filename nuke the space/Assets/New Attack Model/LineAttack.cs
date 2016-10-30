using UnityEngine;
using System.Collections;
using System.Linq;


//TODO : Layered Attack

public class LineAttack : MonoBehaviour
{

    public Vector3 Direction = new Vector3(1, 0, 0);
    public float Distance = 10;
    public float DamagePerPeriod = 1;
    public float DamagePeriod = 0.05f;
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
        foreach (var hp in inrange.Select(x => x.collider.GetComponent<HP>()))
        {
            Debug.Log("Hit : " + hp.gameObject.name, hp.gameObject);
            hp.Value -= DamagePerPeriod;
        }
    }
}
