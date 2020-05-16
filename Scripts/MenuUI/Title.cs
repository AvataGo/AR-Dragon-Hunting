using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public string sceneName = "ARDragonShoot";

    public static Title instance;
    private SaveAndLoad theSaveAndLoad;

    private void Awake() {

        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(this.gameObject);
    }

    public void ClickStart()
    {
        Debug.Log("Start...");
        SceneManager.LoadScene(sceneName);
    }
    public void ClickLoad()
    {
        Debug.Log("Load...");

        StartCoroutine(LoadCoroutine());
    }
    IEnumerator LoadCoroutine()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while(!operation.isDone)
        {
            yield return null;
        }

        theSaveAndLoad = FindObjectOfType<SaveAndLoad>();
        theSaveAndLoad.LoadData();

        gameObject.SetActive(false);
    }
    public void ClicExit()
    {
        Debug.Log("Exit...");
        Application.Quit();
    }
}
