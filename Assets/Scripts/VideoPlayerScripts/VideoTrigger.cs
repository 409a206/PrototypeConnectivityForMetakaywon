using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

 //temporary class
public class VideoTrigger : MonoBehaviour
{
   
    public GameObject VideoManager; 

    private GameObject VideoRendererCanvas;

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            VideoManager.GetComponentInChildren<Canvas>().enabled = true;
            VideoManager.GetComponentInChildren<VideoPlayer>().Play();
            
        } 
    }

    private void OnTriggerStay(Collider other) {
         if(other.tag == "Player") {
            if(VideoManager.GetComponentInChildren<VideoPlayer>().isPaused) {
                VideoManager.GetComponentInChildren<Canvas>().enabled = false;
                VideoManager.GetComponentInChildren<VideoPlayer>().Stop();
            }
         }
    }
}
