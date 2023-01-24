using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("JU TPS/Scene Management/Trigger Load Level")]
public class SimpleLevelTransition : MonoBehaviour
{

    public GameObject LoadingScreen;
    public Image LoadingBarFill;
    [SerializeField]string DesiredLevelName = "Hub";

    public void LoadScene(int sceneID) {
        StartCoroutine(LoadSceneAsync(sceneID));
    }
    public void LoadScene(string sceneName) {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    IEnumerator LoadSceneAsync(int sceneID) {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);

        LoadingScreen.SetActive(true);

        while(!operation.isDone) {

            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);

            LoadingBarFill.fillAmount = progressValue;

            yield return null;
        }
    }

    IEnumerator LoadSceneAsync(string sceneName) {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        LoadingScreen.SetActive(true);

        while(!operation.isDone) {

            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);

            LoadingBarFill.fillAmount = progressValue;

            yield return null;
        }
    }

    // IEnumerator LoadSceneWithFakeLoadingTime(string sceneName) {
    //     LoadingBarFill.fillAmount = 0;
    //     LoadingScreen.SetActive(true);

    //     AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
    //     operation.allowSceneActivation = false;
    //     float progress = 0;
    //     while(!operation.isDone) {
    //         progress = Mathf.MoveTowards(progress, operation.progress, Time.deltaTime);
    //         LoadingBarFill.fillAmount = progress;

    //     }
    // }

    //Set Fake loading time

    IEnumerator WaitForLoadingBar(float seconds)
{
     float currentTime = seconds;
 
     while (currentTime > 0)
     {
          currentTime -= Time.deltaTime;
          yield return null;
     }
 
     Debug.Log(seconds + " seconds have passed! And I'm done waiting!");
}
   
  
    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            //SceneManager.LoadScene(DesiredLevelName);
            StartCoroutine(LoadSceneAsync(DesiredLevelName));
            //StartCoroutine(WaitForLoadingBar(10));
        }
    }
}
