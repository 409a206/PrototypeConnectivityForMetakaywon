using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using UnityEngine.Video;

public class PlayVideo : MonoBehaviour
{
    public GameObject VideoManager; 

    private GameObject VideoRendererCanvas;

    public VideoClip[] VideoClips;
    
    [Tooltip("Typically leave unticked so temporary Dialogue Managers don't unregister your functions.")]
    public bool unregisterOnDisable = false;

    private void Start() {
        VideoManager = this.gameObject;
    }
    void PlayVideoClip(double videoClipIndex) {
         VideoManager.GetComponentInChildren<Canvas>().enabled = true;
         VideoManager.GetComponentInChildren<VideoPlayer>().clip = VideoClips[(int)videoClipIndex];
         VideoManager.GetComponentInChildren<VideoPlayer>().Play();
    }

    #region Register with Lua
    void OnEnable()
    {
        // Make the functions available to Lua: (Replace these lines with your own.)
        // Lua.RegisterFunction("DebugLog", this, SymbolExtensions.GetMethodInfo(() => DebugLog(string.Empty)));
        // Lua.RegisterFunction("AddOne", this, SymbolExtensions.GetMethodInfo(() => AddOne((double)0)));

        Lua.RegisterFunction("PlayVideoClip", this, SymbolExtensions.GetMethodInfo(() => PlayVideoClip((double)0)));
    }

    void OnDisable()
    {
        if (unregisterOnDisable)
        {
            // Remove the functions from Lua: (Replace these lines with your own.)
            // Lua.UnregisterFunction("DebugLog");
            // Lua.UnregisterFunction("AddOne");
            Lua.UnregisterFunction("PlayVideoClip");

        }
    }

   #endregion



}
