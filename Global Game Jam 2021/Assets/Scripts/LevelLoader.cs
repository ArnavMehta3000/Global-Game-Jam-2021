using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;

    private int currentScene;


    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }


    public void SetLevel(int mapNumber)
    {
        GameManager.mapNumber = mapNumber;
    }


    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            //text.text = progress.ToString("f1");
            yield return null;
        }

        loadingScreen.SetActive(false);
    }


    public void QuitLevel()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }
}
