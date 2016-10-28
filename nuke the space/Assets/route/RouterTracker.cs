using UnityEngine;
using System.Collections;
using System;

public class RouterTracker : MonoBehaviour
{
    public float Speed;
    public Router PrevRouter;
    public Router NextRouter;

    void Start()
    {
        if (PrevRouter == null && NextRouter == null)
        {
            Debug.LogError("No Route Set", this.gameObject);
            this.enabled = false;
            return;
        }

        if (PrevRouter == null)
        {
            PrevRouter = NextRouter.Prev;
        }
        else if (NextRouter == null)
        {
            NextRouter = PrevRouter.Next;
        }

        if (PrevRouter != NextRouter.Prev || NextRouter != PrevRouter.Next)
        {
            Debug.LogError("Prev and Next router not match", this.gameObject);
        }
    }

    private void MoveToward(Router router, float speed, bool forword = true)
    {

        if (router == null)
        {
            Debug.Log("Invalid Move Direction, target is nothing");
            return;
        }

        var diff = router.transform.position - transform.position;

        // close to router
        if (diff.magnitude < speed)
        {
            //stop at router
            this.transform.position = router.transform.position;

            //update route table
            if (forword)
            {
                if (router.IsForwardPortal)
                {
                    var relatedPortal = NextRouter.Next;
                    Debug.Assert(relatedPortal != null, "Forward Portal Not Connected", NextRouter);
                    this.transform.position = relatedPortal.transform.position;
                    PrevRouter = relatedPortal;
                    NextRouter = relatedPortal.Next;
                }
                else
                {
                    PrevRouter = NextRouter;
                    NextRouter = NextRouter.Next;
                }

            }
            else
            {
                if (router.IsBackPortal)
                {
                    var relatedPortal = PrevRouter.Prev;
                    Debug.Assert(relatedPortal != null, "Back Portal Not Connected", PrevRouter);
                    this.transform.position = relatedPortal.transform.position;
                    PrevRouter = relatedPortal;
                    NextRouter = relatedPortal.Next;
                }
                else
                {
                    NextRouter = PrevRouter;
                    PrevRouter = PrevRouter.Prev;
                }

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
        MoveToward(PrevRouter, Speed, false);
    }
}
