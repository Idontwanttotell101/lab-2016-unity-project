using UnityEngine;
using System.Collections;
using System;

public class Router : MonoBehaviour
{
    public float speed;
    public route PrevRoute;
    public route NextRoute;
    public Vector3 currentSpeed;

    void Start() {
        currentSpeed = (NextRoute.transform.position - PrevRoute.transform.position).normalized * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            MoveForword();
        }
        else if (Input.GetKey(KeyCode.LeftArrow)) {
            MoveBackword();
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
            if (NextRoute.Next == null) return;
            PrevRoute = NextRoute;
            NextRoute = NextRoute.Next ;
            currentSpeed = (NextRoute.transform.position - PrevRoute.transform.position).normalized * speed;
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
            if (PrevRoute.Prev == null) return;
            NextRoute = PrevRoute;
            PrevRoute = PrevRoute.Prev;
            currentSpeed = (NextRoute.transform.position - PrevRoute.transform.position).normalized * speed;
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
        currentSpeed = (NextRoute.transform.position - PrevRoute.transform.position).normalized * speed;
    }
}
