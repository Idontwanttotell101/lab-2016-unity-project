using UnityEngine;
using System.Collections;

//TODO : split to Attack Receiver
public class HP : MonoBehaviour
{
    [System.Serializable]
    public class HPChangeEvent : UnityEngine.Events.UnityEvent<HP>
    {
        /*parameter : this*/
    };

    //[SerializeField]
    [System.NonSerialized]
    private float Max = float.PositiveInfinity;
    [SerializeField]
    private float Initial=1;
    public HPChangeEvent BeforeHpChange;
    public HPChangeEvent AfterHpChange;

    private float _value;
    public float Value
    {
        get
        {
            return _value;
        }
        set
        {
            BeforeHpChange.Invoke(this);
            _value = value;
            CheckedHP();
            AfterHpChange.Invoke(this);
        }
    }

    private void CheckedHP()
    {
        if (Value <= 0)
            Destroy(this.gameObject);
        if (Value > Max)
            Value = Max;
    }

    void Start()
    {
        // do not trigger event because *Initial* is supposed to be set to certain value (not a change)
        _value = Initial;
    }
}
