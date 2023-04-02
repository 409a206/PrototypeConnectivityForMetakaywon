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
        //if세이브파일이 없다면
        // PlayerData.QuestData.QuestsActive.Add(PlayerData.QuestData.QuestCode.Q1);
        // PlayerData.QuestData.QuestsInactive.Add(PlayerData.QuestData.QuestCode.Q2);
        // PlayerData.QuestData.QuestsInactive.Add(PlayerData.QuestData.QuestCode.Q3);
        // PlayerData.QuestData.QuestsInactive.Add(PlayerData.QuestData.QuestCode.Q4);
        // PlayerData.QuestData.QuestsInactive.Add(PlayerData.QuestData.QuestCode.Q5);
        // PlayerData.QuestData.QuestsInactive.Add(PlayerData.QuestData.QuestCode.Q6);
        // PlayerData.QuestData.QuestsInactive.Add(PlayerData.QuestData.QuestCode.Q7);
        //end if

        switch(dialogue.questCode) {
            case PlayerData.QuestData.QuestCode.Q1 : 
                if(PlayerData.QuestData.QuestsComplete.Contains(PlayerData.QuestData.QuestCode.Q1))    
                    dialogue.isMedalTaken = true;
                break;
            case PlayerData.QuestData.QuestCode.Q2 :
                if(PlayerData.QuestData.QuestsComplete.Contains(PlayerData.QuestData.QuestCode.Q2))    
                    dialogue.isMedalTaken = true;
                break;
            case PlayerData.QuestData.QuestCode.Q3 :
                if(PlayerData.QuestData.QuestsComplete.Contains(PlayerData.QuestData.QuestCode.Q3))    
                    dialogue.isMedalTaken = true;
                break;
            case PlayerData.QuestData.QuestCode.Q4 :
                if(PlayerData.QuestData.QuestsComplete.Contains(PlayerData.QuestData.QuestCode.Q4))    
                    dialogue.isMedalTaken = true;
                break;
            case PlayerData.QuestData.QuestCode.Q5 :
                if(PlayerData.QuestData.QuestsComplete.Contains(PlayerData.QuestData.QuestCode.Q5))    
                    dialogue.isMedalTaken = true;
                break;
            case PlayerData.QuestData.QuestCode.Q6 :
                if(PlayerData.QuestData.QuestsComplete.Contains(PlayerData.QuestData.QuestCode.Q6))    
                    dialogue.isMedalTaken = true;
                break;
            case PlayerData.QuestData.QuestCode.Q7 :
                if(PlayerData.QuestData.QuestsComplete.Contains(PlayerData.QuestData.QuestCode.Q7))    
                    dialogue.isMedalTaken = true;
                break;
            
            default: 
                Debug.LogError("QuestCode Set to Null");
            break;
        }
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
