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
    [SerializeField]
    private GameObject[] QuestNPCs;
    [SerializeField]
    private GameManagerAndUI gameManager;
   

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
        //Debug.Log("Starting conversation with " + dialogue.name);
        isDialogueRunning = !isDialogueRunning;
        ContinueButton.SetActive(true);

        camPivotController.enabled = false;
        thirdPersonController.enabled = false;

        StartCoroutine(gameManager.ShowCursorAfterOneFrame());


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

        //Debug.Log("End of Conversation");
        animator.SetBool("IsOpen", false);
        gameManager.HideCursor();
        isDialogueRunning = !isDialogueRunning;
        camPivotController.enabled = true;
        thirdPersonController.enabled = true;
        
    }
    
    public void CompleteQuest() {
        //메달을 아직 획득하지 않았다면 획득
        if(!dialogue.isMedalTaken) {
            dialogue.isMedalTaken = true;
            PlayerData.QuestData.QuestsComplete.Add(dialogue.questCode);
//            Debug.Log(PlayerData.QuestData.QuestsComplete.Count);
            PlayerData.QuestData.QuestsActive.Remove(dialogue.questCode);
            dialogue.questIcon.SetActive(false);
            //AugmentMedal 함수 호출하기
            //Debug.Log("AugmentMedal Called");
            //FindObjectOfType<AugmentMedalCount>().AugmentMedal(1);
            UpdateOtherQuestsData();
            //퀘스트 데이터 세이브하기
            SaveLoad.SaveData();
        }
    }

    private void UpdateOtherQuestsData()
    {
        Debug.Log("UpdateQuestsData Called");

        //선행퀘스트 관련 로직 업데이트
        for(int i = 0; i < QuestNPCs.Length; i++) {
            Debug.Log(QuestNPCs[i].GetComponent<DialogueTrigger>().dialogue.questCode);
            //비활성 상태의 퀘스트가 아니라면, 즉 완료한 퀘스트거나 이미 활성화된 퀘스트라면 넘어가기
            if(!PlayerData.QuestData.QuestsInactive.Contains(QuestNPCs[i].GetComponent<DialogueTrigger>().dialogue.questCode)) {
                Debug.Log(QuestNPCs[i].GetComponent<DialogueTrigger>().dialogue.questCode + "is either already active or complete. continue"); 
                continue;
            }

            for(int j = 0; j < QuestNPCs[i].GetComponent<DialogueTrigger>().dialogue.requirements.Length; j ++) {
                //완료한 퀘스트 목록에 선행퀘스트가 하나라도 없다면
                if(!PlayerData.QuestData.QuestsComplete.Contains(QuestNPCs[i].GetComponent<DialogueTrigger>().dialogue.requirements[j])) {
                    //그대로 반복문 나가기
                    Debug.Log(QuestNPCs[i].GetComponent<DialogueTrigger>().dialogue.questCode.ToString() + "Quest remains Inactive");
                    break;
                }
                //선행퀘스트가 다 완료되었다면
                if(j == QuestNPCs[i].GetComponent<DialogueTrigger>().dialogue.requirements.Length - 1) {
                    //inactive한 상태라면
                    if(PlayerData.QuestData.QuestsInactive.Contains(QuestNPCs[i].GetComponent<DialogueTrigger>().dialogue.questCode)) {
                        //Active퀘스트 리스트에 추가하고 Inactive퀘스트 리스트에서 제거하기
                        Debug.Log(QuestNPCs[i].GetComponent<DialogueTrigger>().dialogue.questCode.ToString() + "Quest is set active");
                        PlayerData.QuestData.QuestsInactive.Remove(QuestNPCs[i].GetComponent<DialogueTrigger>().dialogue.questCode);
                        PlayerData.QuestData.QuestsActive.Add(QuestNPCs[i].GetComponent<DialogueTrigger>().dialogue.questCode);
                        //느낌표 보이기
                        QuestNPCs[i].GetComponent<DialogueTrigger>().dialogue.questIcon.SetActive(true);
                    }   
                }
            }
        }
    }

    public void ShowURL() {
        if(dialogue.clipURL != "") {
            //Debug.Log("ShowURL");
            Application.OpenURL(dialogue.clipURL);
        }
    }

    // //커서 보이기 코루틴
    // public IEnumerator ShowCursorAfterOneFrame()
    //     {
    //         Cursor.visible = true;
    //         Cursor.lockState = CursorLockMode.None;
    //         //Debug.Log("ShowCursorAfterOneFrame Called");
    //         yield return null;
    //     }

    // //커서 숨기기
    // public void HideCursor()
    // {
    //     StopCoroutine(ShowCursorAfterOneFrame());
    //     Cursor.visible = false;
    // }
}
