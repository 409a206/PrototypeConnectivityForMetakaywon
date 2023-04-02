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

    //미완
    private void InitiateQuestData()
    {
        
            //해당 다이얼로그가 완료된 것이라면 isMedalTaken을 true로 변경
            if(PlayerData.QuestData.QuestsComplete.Contains(dialogue.questCode)) {
                dialogue.isMedalTaken = true;
            }

            //선행퀘스트 관련 로직
            for(int i = 0; i < dialogue.requirements.Length; i++) {
                //완료한 퀘스트 목록에 선행퀘스트가 하나라도 없다면
                if(!PlayerData.QuestData.QuestsComplete.Contains(dialogue.requirements[i])){
                    //해당 퀘스트를 inactive처리하기
                    PlayerData.QuestData.QuestsInactive.Add(dialogue.questCode);
                    break;
                }
                //선행퀘스트가 다 완료되어있다면 active처리하기
                if(i == dialogue.requirements.Length - 1) PlayerData.QuestData.QuestsActive.Add(dialogue.questCode); 
            }

            //questsComplete, questsActive, questsInactive 리스트 중 어디에 속하는지 체크하는 로직 작성하기

    }

    public void TriggerDialogue() {
        FindObjectOfType<DialogueManager>().StartDialogue(ref dialogue);
    }
    
    private void OnTriggerExit(Collider other) {
        if(other.tag == "Player") HideDialogueStartMessage();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") ShowDialogueStartMessage();
    }

    private void OnTriggerStay(Collider other) {
        
        if(other.tag == "Player") {
            if(Input.GetKeyDown(dialogueTriggerKey) && FindObjectOfType<DialogueManager>().isDialogueRunning == false) {
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
