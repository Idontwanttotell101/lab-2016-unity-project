using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class BeamModel : MonoBehaviour
{
    public float BeginRadius = 0;
    public float MaxRadius = 1;
    public float EndRadius = 0;
    public float Distance = 1000;
    public Vector3 Direction = Vector3.right;
    public float FadeInTime = 1;
    public float KeepTime = 0;
    public float FadeOutTime = 2;

    void Start()
    {
        this.transform.localRotation = Quaternion.FromToRotation(Vector3.right, Direction);
        StartCoroutine(GrowAndFade());
    }

    void OnDisable()
    {
        //will crash in unity 5.4.2f2 if not added
        StopAllCoroutines();
    }

    private IEnumerator GrowAndFade()
    {
        var fadeIn_EndTime = Time.time + FadeInTime;
        var max_EndTime = fadeIn_EndTime + KeepTime;
        var fadeOut_EndTime = max_EndTime + FadeOutTime;
        var fadeinCurve = AnimationCurve.EaseInOut(Time.time, BeginRadius, fadeIn_EndTime, MaxRadius);
        var fadeoutCurve = AnimationCurve.EaseInOut(max_EndTime, MaxRadius, fadeOut_EndTime, EndRadius);
        yield return null;

        transform.localScale = new Vector3(Distance, BeginRadius, BeginRadius);
        gameObject.SetActive(true);

        while (Time.time < fadeIn_EndTime)
        {
            var r = fadeinCurve.Evaluate(Time.time);
            transform.localScale = new Vector3(Distance, r, r);
            yield return null;
        }

        transform.localScale = new Vector3(Distance, MaxRadius, MaxRadius);
        while (Time.time < max_EndTime)
        {
            yield return null;
        }

        while (Time.time < fadeOut_EndTime)
        {
            var r = fadeoutCurve.Evaluate(Time.time);
            transform.localScale = new Vector3(Distance, r, r);
            yield return null;
        }
    }
}
