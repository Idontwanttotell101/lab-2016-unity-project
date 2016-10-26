using UnityEngine;
using System.Collections;
using System;

public class Router : MonoBehaviour
{
    public float speed;
    public route PrevRoute;
    public route NextRoute;
    public Vector3 currentSpeed;

    // Update is called once per frame
    void Update()
    {


        if ((transform.position - NextRoute.transform.position).magnitude < speed)
        {

        }
    }

    public void MoveForword()
    {
        if (NextRoute == null)
        {
            Debug.Log("Invalid Move Direction, target is nothing");
            return;
        }
        if ((transform.position - NextRoute.transform.position).magnitude < speed)
        {
            this.transform.position = NextRoute.transform.position;
            UpdateRouteTable(NextRoute);
        }
        else
        {
            this.transform.position += currentSpeed;
        }
    }

    public void MoveBackword()
    {
        if (PrevRoute == null) {
            Debug.Log("Invalid Move Direction, target is nothing");
            return;
        }
        if ((transform.position - PrevRoute.transform.position).magnitude < speed)
        {
            this.transform.position = PrevRoute.transform.position;
            UpdateRouteTable(PrevRoute);
        }
        else
        {
            this.transform.position -= currentSpeed;
        }
    }

    private void UpdateRouteTable(route routePoint)
    {
        PrevRoute = routePoint.Prev;
        NextRoute = routePoint.Next;
    }
}
