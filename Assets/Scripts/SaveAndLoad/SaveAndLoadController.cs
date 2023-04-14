using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveAndLoadController : MonoBehaviour
{   
    [SerializeField]
    private KeyCode OpenMenuKey = KeyCode.Escape;
    [HideInInspector]
    public static bool isMenuOpen = false;
    [SerializeField]
    private Canvas MenuCanvas;
    [SerializeField]
    private CamPivotController camPivotController;
    [SerializeField]
    private ThirdPersonController thirdPersonController;
    [SerializeField]
    private DialogueManager dialogueManager;
    [SerializeField]
    private GameManagerAndUI gameManager;
    [SerializeField]
    private GameObject alertBox;
    [SerializeField]
    private LevelTransitionManager levelTransitionManager;

    // Start is called before the first frame update
    void Start()
    {
        MenuCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        ToggleMenu(ref isMenuOpen);
    }

    public void ToggleMenu(ref bool isMenuOpen)
    {
        if(Input.GetKeyDown(OpenMenuKey)){
            if(dialogueManager != null) {
                if(!dialogueManager.isDialogueRunning) {
                    if(isMenuOpen) {
                        CloseMenu();
                    } else {
                        OpenMenu();
                    } 
            }
            } else {
                    if(isMenuOpen) {
                       CloseMenu();
                    } else {
                       OpenMenu();
                    } 
            }
        }
    }

    public void OpenMenu() {
        Debug.Log("메뉴창 열기");
        isMenuOpen = !isMenuOpen;
        camPivotController.enabled = false;
        thirdPersonController.enabled = false;
        MenuCanvas.enabled = true;
        StartCoroutine(gameManager.ShowCursorAfterOneFrame());
    }
    public void CloseMenu() {
        Debug.Log("메뉴창 닫기");
        isMenuOpen = !isMenuOpen;
        camPivotController.enabled = true;
        thirdPersonController.enabled = true;
        MenuCanvas.enabled = false;
        gameManager.HideCursor();
    }

    public void ShowAlert() {
        alertBox.SetActive(true);
    }

    public void CloseAlert() {
        alertBox.SetActive(false);
    }

    public void SaveData() {
        SaveLoad.SaveData();   
    }

    public void LoadData() {
        SaveLoad.LoadData();
    }

    public void ExitToLobby() {
        SaveLoad.SaveData(); 
        //로비로 가는 로직 작성
        levelTransitionManager.LoadScene("Lobby", new Vector3(), new Vector3());
    }
}
