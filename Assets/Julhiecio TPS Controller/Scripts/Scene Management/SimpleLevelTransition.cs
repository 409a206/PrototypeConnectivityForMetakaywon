using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System;

[AddComponentMenu("JU TPS/Scene Management/Trigger Load Level")]
public class SimpleLevelTransition : MonoBehaviour
{

    public GameObject LoadingScreen;
    public Image LoadingBarFill;
    [SerializeField]
    string DesiredLevelName = "Hub";

    public void LoadScene(int sceneID) {

        Indestructable.instance.prevSceneID = SceneManager.GetActiveScene().GetHashCode();

        StartCoroutine(LoadSceneAsync(sceneID));
    }
    public void LoadScene(string sceneName) {

       LoadingBarFill.fillAmount = 0;
        LoadingScreen.SetActive(true);
        Indestructable.instance.prevSceneName = SceneManager.GetActiveScene().name;
        Indestructable.instance.currentSceneName = DesiredLevelName;
        Indestructable.instance.isPlayerPosConfigured = false;
        
        //Debug.Log(Indestructable.instance.prevSceneName);
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    IEnumerator LoadSceneAsync(int sceneID) {

        LoadingScreen.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);

        while(!operation.isDone) {

            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);

            LoadingBarFill.fillAmount = progressValue;

            yield return null;
        }
    }

    IEnumerator LoadSceneAsync(string sceneName) {

        //LoadingScreen.SetActive(true);
        

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while(!operation.isDone) {

            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);

            LoadingBarFill.fillAmount = progressValue;

            yield return null;
        }
    }

    IEnumerator LoadSceneWithFakeLoadingTime(string sceneName) {
        LoadingBarFill.fillAmount = 0;
        LoadingScreen.SetActive(true);
        Indestructable.instance.prevSceneName = SceneManager.GetActiveScene().name;
        Indestructable.instance.currentSceneName = DesiredLevelName;
        Indestructable.instance.isPlayerPosConfigured = false;

        //Debug.Log(Indestructable.instance.prevSceneName);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;
        float progress = 0;
        while(!operation.isDone) {
            progress = Mathf.MoveTowards(progress, operation.progress, Time.deltaTime);
            LoadingBarFill.fillAmount = progress;
            if(progress>=0.9f) {
                LoadingBarFill.fillAmount = 1;
                yield return new WaitForEndOfFrame();
                operation.allowSceneActivation = true;
                LoadingScreen.SetActive(false);
            }
            yield return null;
        }
    }

    private void setPlayerPos()
    {
        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
        string currentSceneName = DesiredLevelName;
        Debug.Log("currentSceneName : " + currentSceneName);
        string prevSceneName = Indestructable.instance.prevSceneName;
        Debug.Log("prevSceneName : " + prevSceneName);

        if(currentSceneName == "Main") {
            switch(prevSceneName) {
                case "KUMAGallery" : 
                    playerGO.transform.position = new Vector3(258, 28, 527);
                    playerGO.transform.rotation = Quaternion.Euler(0, 90, 0); 
                    break;

                case "LakePark" : 
                    playerGO.transform.position = new Vector3(255, 25, 624);
                    playerGO.transform.rotation = Quaternion.Euler(0, 142, 0); 
                    break;

                case "RacingKart" :
                    playerGO.transform.position = new Vector3(299, 38, 553);
                    playerGO.transform.rotation = Quaternion.Euler(0, 180, 0);
                    break;

                default : 
                //다시 세팅하기
                    playerGO.transform.position = new Vector3(258, 28, 527);
                    playerGO.transform.rotation = Quaternion.Euler(0, 90, 0); 
                    break;
            }
        }

    }

    //Set Fake loading time

    //     IEnumerator WaitForLoadingBar(float seconds)
    // {
    //      float currentTime = seconds;

    //      while (currentTime > 0)
    //      {
    //           currentTime -= Time.deltaTime;
    //           yield return null;
    //      }

    //      Debug.Log(seconds + " seconds have passed! And I'm done waiting!");
    // }


    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            //SceneManager.LoadScene(DesiredLevelName);
            //StartCoroutine(LoadSceneAsync(DesiredLevelName));
            //StartCoroutine(WaitForLoadingBar(10));
            StartCoroutine(LoadSceneWithFakeLoadingTime(DesiredLevelName));
             
        }
    }
}
