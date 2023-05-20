using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]
    private KeyCode dialogueTriggerKey;
    public Dialogue dialogue;

    private void Start() {
        InitiateQuestData();    
    }

    private void InitiateQuestData()
    {
        //Debug.Log("Initiate QuestData Called");

        //Saved Data가 존재한다면 불러오기
        if(SaveLoad.LoadData() != null) {
//            Debug.Log("Quest Data Loaded");
            PlayerData.QuestData.QuestsActive = SaveLoad.LoadData().QuestsActive;
            PlayerData.QuestData.QuestsInactive = SaveLoad.LoadData().QuestsInactive;
            PlayerData.QuestData.QuestsComplete = SaveLoad.LoadData().QuestsComplete;
        }


        //만약 플레이어 데이터에 해당 퀘스트 정보가 이미 있다면
        if(PlayerData.QuestData.QuestsActive.Contains(dialogue.questCode)) {
            //느낌표 활성 처리 로직 작성
            dialogue.questIcon.SetActive(true);
            //Debug.Log(dialogue.questCode.ToString() + "Quest is in Active mode");
            dialogue.isMedalTaken = false;
            return;
        } else if (PlayerData.QuestData.QuestsInactive.Contains(dialogue.questCode)) {
            //느낌표 비활성 처리 로직 작성
            dialogue.questIcon.SetActive(false);
            //Debug.Log(dialogue.questCode.ToString() + "Quest is in Inactive mode");
            dialogue.isMedalTaken = false;
            return;
        } else if (PlayerData.QuestData.QuestsComplete.Contains(dialogue.questCode)) {
            //느낌표 비활성 처리 로직 작성
            dialogue.questIcon.SetActive(false);
           // Debug.Log(dialogue.questCode.ToString() + "Quest is in Complete mode");
            //해당 다이얼로그가 완료된 것이라면 isMedalTaken을 true로 변경
            dialogue.isMedalTaken = true;
            return;
        } 

        //플레이어 데이터에 해당 퀘스트 정보가 없을 경우 로직
        
        //선행퀘스트 관련 로직
        for(int i = 0; i < dialogue.requirements.Length; i++) {
            //완료한 퀘스트 목록에 선행퀘스트가 하나라도 없다면
            if(!PlayerData.QuestData.QuestsComplete.Contains(dialogue.requirements[i])){
                //해당 퀘스트를 inactive처리하기
                PlayerData.QuestData.QuestsInactive.Add(dialogue.questCode);
                Debug.Log(dialogue.questCode.ToString() + "Quest is set Inactive");
                //느낌표 비활성 처리 로직 작성
                dialogue.questIcon.SetActive(false);
                return;
            }
        }
        //선행퀘스트가 다 완료되어있다면 active처리하기
        PlayerData.QuestData.QuestsActive.Add(dialogue.questCode); 
        Debug.Log(dialogue.questCode.ToString() + "Quest is set Active");
        dialogue.questIcon.SetActive(true);
    }

    public void TriggerDialogue() {
        FindObjectOfType<DialogueManager>().StartDialogue(ref dialogue);
    }
    
    private void OnTriggerExit(Collider other) {
        if(other.tag == "Player") HideDialogueStartMessage();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player" && 
        (PlayerData.QuestData.QuestsActive.Contains(dialogue.questCode) || PlayerData.QuestData.QuestsComplete.Contains(dialogue.questCode))) {
        ShowDialogueStartMessage();
        }
    }

    private void OnTriggerStay(Collider other) {
        
        if(other.tag == "Player") {
            if(Input.GetKeyDown(dialogueTriggerKey) && FindObjectOfType<DialogueManager>().isDialogueRunning == false
            && (PlayerData.QuestData.QuestsActive.Contains(dialogue.questCode) || PlayerData.QuestData.QuestsComplete.Contains(dialogue.questCode))
            && !SaveAndLoadController.isMenuOpen) {
                Debug.Log("DialogueTriggerKey Pressed");
                HideDialogueStartMessage();
                TriggerDialogue();
            }
        }
    }

    
    private void ShowDialogueStartMessage()
    {
        gameObject.GetComponent<UIMenssengerTrigger>().ShowMenssage();
    }
    private void HideDialogueStartMessage()
    {
        gameObject.GetComponent<UIMenssengerTrigger>().HideMenssage();
    }
}
