using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void ToggleMenu(ref bool isMenuOpen)
    {
        if(Input.GetKeyDown(OpenMenuKey)){
            if(dialogueManager != null) {
                if(!dialogueManager.isDialogueRunning) {
                    isMenuOpen = !isMenuOpen;
                    if(isMenuOpen) {
                        Debug.Log("메뉴창 열기");
                        camPivotController.enabled = false;
                        thirdPersonController.enabled = false;
                        MenuCanvas.enabled = true;
                        StartCoroutine(gameManager.ShowCursorAfterOneFrame());
                    } else {
                        Debug.Log("메뉴창 닫기");
                        camPivotController.enabled = true;
                        thirdPersonController.enabled = true;
                        MenuCanvas.enabled = false;
                        gameManager.HideCursor();
                    } 
            }
            } else {
                 isMenuOpen = !isMenuOpen;
                    if(isMenuOpen) {
                        Debug.Log("메뉴창 열기");
                        camPivotController.enabled = false;
                        thirdPersonController.enabled = false;
                        MenuCanvas.enabled = true;
                        StartCoroutine(gameManager.ShowCursorAfterOneFrame());
                    } else {
                        Debug.Log("메뉴창 닫기");
                        camPivotController.enabled = true;
                        thirdPersonController.enabled = true;
                        MenuCanvas.enabled = false;
                        gameManager.HideCursor();
                    } 
            }
        }
    }

    private void SaveData() {
        SaveLoad.SaveData();
    }

}
