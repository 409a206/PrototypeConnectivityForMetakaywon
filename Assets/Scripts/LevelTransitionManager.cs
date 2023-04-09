using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelTransitionManager : MonoBehaviour
{
    public GameObject LoadingPanel;
    public Image LoadingBarFill;
    
    private IEnumerator LoadSceneWithFakeLoadingTime(string sceneName, Vector3 nextScenePlayerSpawnPosition, Vector3 nextScenePlayerSpawnRotation) {
        LoadingBarFill.fillAmount = 0;
        LoadingPanel.SetActive(true);

        // SceneTransitionData.activeSceneName = SceneManager.GetActiveScene().name;
        // SceneTransitionData.nextSceneName = DesiredLevelName;
        //SceneTransitionData.isPlayerPosConfigured = false;

        //Debug.Log(SceneTransitionData.prevSceneName);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        ThirdPersonController.SceneTransitionData.PlayerSpawnPosition = nextScenePlayerSpawnPosition;
        ThirdPersonController.SceneTransitionData.PlayerSpawnRotation = nextScenePlayerSpawnRotation;

        operation.allowSceneActivation = false;
        float progress = 0;

        while(!operation.isDone) {
            progress = Mathf.MoveTowards(progress, operation.progress, Time.deltaTime);
            LoadingBarFill.fillAmount = progress;
            if(progress>=0.9f) {
                LoadingBarFill.fillAmount = 1;
                yield return new WaitForEndOfFrame();
                operation.allowSceneActivation = true;
                LoadingPanel.SetActive(false);
            }
            yield return null;
        }
    }

    public void LoadScene(string desiredLevelName, Vector3 nextScenePlayerSpawnPosition, Vector3 nextScenePlayerSpawnRotation) {
        StartCoroutine(LoadSceneWithFakeLoadingTime(desiredLevelName, nextScenePlayerSpawnPosition, nextScenePlayerSpawnRotation));
    }


}
