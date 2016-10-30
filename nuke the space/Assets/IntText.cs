using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IntText : MonoBehaviour
{
    public Text textComponent;
    public int text
    {
        set
        {
            textComponent.text = value.ToString();
        }
    }

    public HP HPToText
    {
        set
        {
            textComponent.text = value.Value.ToString();
        }
    }

    void Start()
    {
        textComponent = GetComponent<Text>();
        Debug.Assert(textComponent != null, "should have text attached");
    }
}
