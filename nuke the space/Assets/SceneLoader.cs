using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    public string nextStage;

    public void Retry()
    {
        var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void NextStage()
    {
        SceneManager.LoadScene(nextStage);
    }
}
