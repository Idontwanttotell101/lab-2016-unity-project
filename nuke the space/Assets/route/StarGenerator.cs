using UnityEngine;
using System.Collections;

public class StarGenerator : MonoBehaviour
{

    public GameObject[] star;
    public float maxdistance;

    // Use this for initialization
    void Start()
    {
        if (star.Length == 0)
        {
            Debug.Log("no star");
            return;
        }
        for (int i = 0; i < 1000; ++i)
        {
            var randomRelatedPos = (Vector3)Random.insideUnitCircle * maxdistance;
            var realpos = this.transform.TransformVector(transform.position + randomRelatedPos);
            var starIns = Instantiate(star[Random.Range(0, star.Length - 1)], realpos, Random.rotationUniform) as GameObject;
            starIns.transform.parent = this.transform;
    }
}
}
