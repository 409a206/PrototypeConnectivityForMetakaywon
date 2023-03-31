using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]
    private KeyCode dialogueTriggerKey;
    public Dialogue dialogue;

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
