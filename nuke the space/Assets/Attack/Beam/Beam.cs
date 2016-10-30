using UnityEngine;
using System.Collections;

public class Beam : MonoBehaviour
{
    public Vector3 Direction = Vector3.right;
    public float Distance = 15;
    public float Duration = 4;
    public float DPS = 20;
    private float DamagePeriod = 0.05f;

    private BeamModel render_model;
    private LineAttack attack_model;

    void Start()
    {
        render_model = GetComponentInChildren<BeamModel>(true);
        attack_model = GetComponentInChildren<LineAttack>(true);
        render_model.Direction = Direction;
        render_model.Distance = Distance;
        render_model.FadeInTime = Duration / 2;
        render_model.FadeOutTime = Duration / 2;
        render_model.KeepTime = 0;

        attack_model.Direction = Direction;
        attack_model.Distance = Distance;
        attack_model.DamagePerPeriod = DPS * DamagePeriod;
        attack_model.DamagePeriod = DamagePeriod;

        render_model.gameObject.SetActive(true);
        attack_model.gameObject.SetActive(true);

        Destroy(this.gameObject, Duration);
    }
}
