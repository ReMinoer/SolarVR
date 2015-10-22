using UnityEngine;
using UnityEditor;
using System.Collections;

public class LoadScene : MonoBehaviour {

    public string sceneName = "-1";

    void Start()
    {
        Debug.Log(sceneName);
        if (sceneName == "-1")
        {
            Debug.Log("Ouech'");
            sceneName = Application.loadedLevelName;
        }
    }
    public void Load()
    {
        Application.LoadLevel(sceneName);
    }
}
