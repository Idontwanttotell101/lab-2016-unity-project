using UnityEngine;
using System.Collections;
using System;

public class RouterTracker : MonoBehaviour
{
    public float Speed;
    public Router PrevRouter;
    public Router NextRouter;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            MoveForword();
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            MoveBackword();
        }
    }

    private void MoveToward(Router router, float speed, bool forword = true)
    {

        if (router == null)
        {
            Debug.Log("Invalid Move Direction, target is nothing");
            return;
        }

        var diff =  router.transform.position - transform.position;

        // close to router
        if (diff.magnitude < speed)
        {
            //stop at router
            this.transform.position = router.transform.position;

            //update route table
            if (forword)
            {
                if (router.Next == null) return;
                PrevRouter = NextRouter;
                NextRouter = NextRouter.Next;
            }
            else
            {
                if (router.Prev == null) return;
                NextRouter = PrevRouter;
                PrevRouter = PrevRouter.Prev;
            }
        }
        else
        {
            //normal move
            this.transform.position += diff.normalized * speed;
        }
    }

    public void MoveForword()
    {
        MoveToward(NextRouter, Speed);
    }

    public void MoveBackword()
    {
        MoveToward(PrevRouter, Speed);
    }
}
