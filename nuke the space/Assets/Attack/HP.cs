using UnityEngine;
using System.Collections;

public class HP : MonoBehaviour
{
    [System.Serializable]
    public class HPChangeEvent : UnityEngine.Events.UnityEvent<HP>
    {
        /*parameter : hp after change*/
    };

    [SerializeField]
    private float Max;
    [SerializeField]
    private float Initial;
    public HPChangeEvent BeforeHpChange;
    public HPChangeEvent AfterHpChange;

    private float _value;
    public float Value
    {
        get
        {
            return _value;
        }
        private set
        {
            BeforeHpChange.Invoke();
            _value = value;
            AfterHpChange.Invoke();
        }
    }

    void Start()
    {
        Value = Initial;
    }

    public void Update()
    {
        if (Value < 0)
            Destroy(this.gameObject);
    }
}
