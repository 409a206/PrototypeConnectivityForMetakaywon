using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndLoadTrigger : MonoBehaviour
{
   private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            SaveLoad.SaveData();
            Debug.Log("Save Data Trigger Entered");
        }
   }
}
