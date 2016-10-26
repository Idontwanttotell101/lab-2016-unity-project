using UnityEngine;
using System.Collections;

public class RouterTrackerExample : MonoBehaviour
{

    private RouterTracker tracker;

    void Start()
    {
        tracker = GetComponent<RouterTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            tracker.MoveForword();
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            tracker.MoveBackword();
        }
    }
}
