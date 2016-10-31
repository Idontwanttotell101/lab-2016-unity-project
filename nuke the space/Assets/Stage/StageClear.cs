using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class StageClear : MonoBehaviour {
    public GameObject ui;
    
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            ui.SetActive(true);
        }
    }
}
