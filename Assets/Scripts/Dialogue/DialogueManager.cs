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

    public float letterSpeed = 0.02f;

    public Animator animator;

    private Queue<string> sentences;
    private string clipURL;

    [SerializeField]
    private CamPivotController camPivotController;
    [SerializeField]
    private ThirdPersonController thirdPersonController;

    [HideInInspector]
    public bool isDialogueRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        
    }
    private void Update() {
        if(isDialogueRunning && Input.GetKeyDown(KeyCode.Space)) {
            DisplayNextSentence();
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
       
        Debug.Log("Starting conversation with " + dialogue.name);
        isDialogueRunning = !isDialogueRunning;

        camPivotController.enabled = false;
        thirdPersonController.enabled = false;

        StartCoroutine(ShowCursorAfterOneFrame());


        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.name;
        clipURL = dialogue.clipURL;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        
        DisplayNextSentence();
      
    }

    public void DisplayNextSentence()
    {
        
        if(sentences.Count == 0) {
            EndDialogue();
            return;
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
        Debug.Log("End of Conversation");
        animator.SetBool("IsOpen", false);
        HideCursor();
        isDialogueRunning = !isDialogueRunning;
        camPivotController.enabled = true;
        thirdPersonController.enabled = true;
        
        if(clipURL != "") Application.OpenURL(clipURL);
    }

    //커서 보이기 코루틴
    protected IEnumerator ShowCursorAfterOneFrame()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Debug.Log("ShowCursorAfterOneFrame Called");
            yield return null;
        }

    private void HideCursor()
    {
        StopCoroutine(ShowCursorAfterOneFrame());
        Cursor.visible = false;
    }
}
