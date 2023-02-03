using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmailCollider : MonoBehaviour
{
    public GameObject EmailManager;
    // Start is called before the first frame update
    void Start()
    {
        EmailManager = GameObject.Find("EmailManager");
    }

    
    void OnTriggerEnter(Collider other) {

        if(other.tag == "Player"){
            SMTPEmail email = EmailManager.GetComponent<SMTPEmail>();
            email.SendMail(0);
            Debug.Log("Trigger Entered");
        }
    }
    
    
}
