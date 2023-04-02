using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{   
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public GameObject ContinueButton;
    public GameObject YesAndNoButton;
    public GameObject ConfirmButton;

    public float letterSpeed = 0.02f;

    public Animator animator;

    private Queue<string> sentences;
    //private string clipURL;
    private Dialogue dialogue;

    [SerializeField]
    private CamPivotController camPivotController;
    [SerializeField]
    private ThirdPersonController thirdPersonController;
   // private Dialogue dialogue;

    [HideInInspector]
    public bool isDialogueRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        
    }
    private void Update() {
        if(isDialogueRunning && Input.GetKeyDown(KeyCode.Space) && ContinueButton.activeSelf) {
            DisplayNextSentence();
        }
    }

    public void StartDialogue(ref Dialogue dialogue)
    {
        this.dialogue = dialogue;
        Debug.Log("Starting conversation with " + dialogue.name);
        isDialogueRunning = !isDialogueRunning;
        ContinueButton.SetActive(true);

        camPivotController.enabled = false;
        thirdPersonController.enabled = false;

        StartCoroutine(ShowCursorAfterOneFrame());


        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.name;
        //clipURL = dialogue.clipURL;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        
        DisplayNextSentence();
      
    }

    public void DisplayNextSentence()
    {
        
        // if(sentences.Count == 0) {
        //     EndDialogue();
        //     return;
        // }

        if(sentences.Count == 1) {
            //상황에 따라 다른 버튼 보이기 로직 작성
            ContinueButton.SetActive(false);
            if(dialogue.clipURL != "") {
                YesAndNoButton.SetActive(true);
            } else {
                ConfirmButton.SetActive(true);
            }
        }

        string sentence = sentences.Dequeue();
        //Debug.Log(sentence);
        //dialogueText.text = sentence;

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence) 
    {
        dialogueText.text = "";
        
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(letterSpeed);
        }
    }

    public void EndDialogue()
    {   
        //모든 버튼 비활성화
        YesAndNoButton.SetActive(false);
        ConfirmButton.SetActive(false);
        ContinueButton.SetActive(false);

        Debug.Log("End of Conversation");
        animator.SetBool("IsOpen", false);
        HideCursor();
        isDialogueRunning = !isDialogueRunning;
        camPivotController.enabled = true;
        thirdPersonController.enabled = true;
        
    }
    
    public void CompleteQuest() {
        //메달을 아직 획득하지 않았다면 획득
        if(!dialogue.isMedalTaken) {
            dialogue.isMedalTaken = true;
            PlayerData.QuestData.QuestsComplete.Add(dialogue.questCode);
            //AugmentMedal 함수 호출하기
            Debug.Log("AugmentMedal Called");
        }
    }

    public void ShowURL() {
        if(dialogue.clipURL != "") {
            Debug.Log("ShowURL");
            Application.OpenURL(dialogue.clipURL);
        }
    }

    //커서 보이기 코루틴
    protected IEnumerator ShowCursorAfterOneFrame()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Debug.Log("ShowCursorAfterOneFrame Called");
            yield return null;
        }

    //커서 숨기기
    private void HideCursor()
    {
        StopCoroutine(ShowCursorAfterOneFrame());
        Cursor.visible = false;
    }
}
