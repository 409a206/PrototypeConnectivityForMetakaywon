using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Indestructable : MonoBehaviour {

    public static Indestructable instance = null;

    // For sake of example, assume -1 indicates first scene
    public int prevSceneID = -1;
    public string prevSceneName = "";
    public string currentSceneName = "";
    public bool isPlayerPosConfigured;

    void Awake() {
        // If we don't have an instance set - set it now
        if(!instance )
            instance = this;
        // Otherwise, its a double, we dont need it - destroy
        else {
            Destroy(this.gameObject) ;
            return;
        }

        DontDestroyOnLoad(this.gameObject) ;
    }

    private void Update() {
        if(SceneManager.GetActiveScene().isLoaded && SceneManager.GetActiveScene().name == currentSceneName && !isPlayerPosConfigured) {
            isPlayerPosConfigured = true;
            setPlayerPos();
        }
    }

     private void setPlayerPos()
    {
        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
        // string currentSceneName = DesiredLevelName;
        Debug.Log("currentSceneName : " + currentSceneName);
        string prevSceneName = Indestructable.instance.prevSceneName;
        Debug.Log("prevSceneName : " + prevSceneName);

        if(currentSceneName == "Main" || currentSceneName == "DummySceneForLoading") {
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

                //dummy case
               case "DialogueExampleScene" :
                    Debug.Log("Hiroo");
                    break;

                default : 
                //다시 세팅하기
                    playerGO.transform.position = new Vector3(258, 28, 527);
                    playerGO.transform.rotation = Quaternion.Euler(0, 90, 0); 
                    break;
            }
        }

    }


}