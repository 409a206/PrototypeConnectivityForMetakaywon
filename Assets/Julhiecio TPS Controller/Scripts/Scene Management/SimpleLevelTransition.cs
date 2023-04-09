using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System;

[AddComponentMenu("JU TPS/Scene Management/Trigger Load Level")]
public class SimpleLevelTransition : MonoBehaviour
{

    
    [SerializeField]
    string DesiredLevelName = "Hub";
    [SerializeField]
    private Vector3 nextScenePlayerSpawnPosition;
    [SerializeField]
    private Vector3 nextScenePlayerSpawnRotation;
    [SerializeField]
    private LevelTransitionManager levelTransitionManager;

    /*public static class SceneTransitionData
    {
        //현재 씬 이름
        public static string activeSceneName;
        //불러올 씬 이름
        public static string nextSceneName;
        //현재 씬 아이디
        public static int activeSceneId;
        //불러올 씬 아이디
        public static int nextSceneId;
        public static bool isPlayerPosConfigured;
        public static Vector3 playerSpawnPosition;
        public static Vector3 playerSpawnRotation;


    }

    public void LoadScene(int sceneID) {

        SceneTransitionData.activeSceneId = SceneManager.GetActiveScene().GetHashCode();

        StartCoroutine(LoadSceneAsync(sceneID));
    }
    public void LoadScene(string sceneName) {

       LoadingBarFill.fillAmount = 0;
        LoadingPanel.SetActive(true);
        SceneTransitionData.activeSceneName = SceneManager.GetActiveScene().name;
        SceneTransitionData.nextSceneName = DesiredLevelName;
        SceneTransitionData.isPlayerPosConfigured = false;
        
        //Debug.Log(SceneTransitionData.prevSceneName);
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    //Scene Id로 씬전환
    IEnumerator LoadSceneAsync(int sceneID) {

        LoadingPanel.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);

        while(!operation.isDone) {

            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);

            LoadingBarFill.fillAmount = progressValue;

            yield return null;
        }
    }

    //Scene Name으로 씬전환
    IEnumerator LoadSceneAsync(string sceneName) {

        //LoadingScreen.SetActive(true);
        

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while(!operation.isDone) {

            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);

            LoadingBarFill.fillAmount = progressValue;

            yield return null;
        }
    }*/

    
    /* private void setPlayerPos()
    {
        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
        string nextSceneName = DesiredLevelName;
        Debug.Log("nextSceneName : " + nextSceneName);
        string activeSceneName = SceneTransitionData.activeSceneName;
        Debug.Log("activeSceneName : " + activeSceneName);

        if(nextSceneName == "Main") {
            switch(activeSceneName) {
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
                다시 세팅하기
                    playerGO.transform.position = new Vector3(258, 28, 527);
                    playerGO.transform.rotation = Quaternion.Euler(0, 90, 0); 
                    break;
            }
        }

    }

    Set Fake loading time

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
 */

    //trigger로 Load Scene하기
    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            //SceneManager.LoadScene(DesiredLevelName);
            //StartCoroutine(LoadSceneAsync(DesiredLevelName));
            //StartCoroutine(WaitForLoadingBar(10));
            levelTransitionManager.LoadScene(DesiredLevelName, nextScenePlayerSpawnPosition, nextScenePlayerSpawnRotation);
             
        }
    }

    
}
