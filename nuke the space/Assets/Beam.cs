using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Beam : MonoBehaviour
{
    public float BeginRadius = 1;
    public float MaxRadius = 1;
    public float EndRadius = 1;
    public float Distance = 1000;
    public AnimationClip ssss;
    private Vector3 direction = new Vector3(1, 0, 0);
    public GameObject model;

    // Use this for initialization
    void Start()
    {
        model.transform.localScale = new Vector3(Distance, 1, 1);
        StartCoroutine(GrowHitAndFade(MaxRadius, 1, 0, 5));
    }
    private IEnumerator GrowHitAndFade(float radius, float fadeInTime, float keepTime, float fadeOutTime)
    {
        var beginRad = 0f;
        var endRad = 0f;
        var fadeIn_EndTime = Time.time + fadeInTime;
        var max_EndTime = fadeIn_EndTime + keepTime;
        var fadeOut_EndTime = max_EndTime + fadeOutTime;
        var fadeinCurve = AnimationCurve.EaseInOut(Time.time, beginRad, fadeIn_EndTime, radius);
        var fadeoutCurve = AnimationCurve.EaseInOut(max_EndTime, radius, fadeOut_EndTime, endRad);
        yield return null;

        while (Time.time < fadeIn_EndTime)
        {
            var r = fadeinCurve.Evaluate(Time.time);
            model.transform.localScale = new Vector3(Distance, r, r);
            yield return null;
        }

        model.transform.localScale = new Vector3(Distance, radius, radius);
        while (Time.time < max_EndTime)
        {
            yield return null;
        }

        while (Time.time < fadeOut_EndTime)
        {
            var r = fadeoutCurve.Evaluate(Time.time);
            model.transform.localScale = new Vector3(Distance, r, r);
            yield return null;
        }

        Destroy(model);
    }
}
