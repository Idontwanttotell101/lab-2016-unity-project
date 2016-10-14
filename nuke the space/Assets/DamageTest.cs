using UnityEngine;
using System.Collections;

public class DamageTest : MonoBehaviour
{
    public int damage = 0;
    public float damageDelay = 0.1f;
    private int damageCurrent = 0;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("TryDamage", 0, damageDelay);
    }

    void OnTriggerStay(Collider c)
    {
        var attack = c.gameObject.transform.root.GetComponent<Attack>();
        if (attack != null)
        {
            damageCurrent = attack.damage;
        }
    }

    void TryDamage()
    {
        damage += damageCurrent;
        damageCurrent = 0;
    }
}
