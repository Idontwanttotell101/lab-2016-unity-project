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
            Vector3 pos = Random.insideUnitCircle * maxdistance;
            (Instantiate(star[Random.Range(0, star.Length - 1)], transform.position + pos, Random.rotationUniform)
                as GameObject)
                .transform.parent = this.transform;

        }
    }
}
