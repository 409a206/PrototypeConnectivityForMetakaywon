using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class QuestManagerScript : MonoBehaviour
{
    public GameObject[] QuestNPCs;

    private SpriteRenderer[] MinimapQuestIcons;

    
    [Tooltip("Typically leave unticked so temporary Dialogue Managers don't unregister your functions.")]
    public bool unregisterOnDisable = false;

    private void Start() {
        MinimapQuestIcons = new SpriteRenderer[QuestNPCs.Length];
        for(int i = 0; i < QuestNPCs.Length; i++) {
            MinimapQuestIcons[i] = QuestNPCs[i].GetComponentInChildren<SpriteRenderer>();
            MinimapQuestIcons[i].enabled = false;
        }
    }

    private void QuestSignAppear(double index) {
        
        
        MinimapQuestIcons[(int) index].enabled = true;

    }

    private void QuestSignDisappear(double index) {
         MinimapQuestIcons[(int) index].enabled = false;
    }

    
    #region Register with Lua
    void OnEnable()
    {
        // Make the functions available to Lua: (Replace these lines with your own.)
        // Lua.RegisterFunction("DebugLog", this, SymbolExtensions.GetMethodInfo(() => DebugLog(string.Empty)));
        // Lua.RegisterFunction("AddOne", this, SymbolExtensions.GetMethodInfo(() => AddOne((double)0)));
        Lua.RegisterFunction("QuestSignAppear", this, SymbolExtensions.GetMethodInfo(() => QuestSignAppear((double) 0)));
        Lua.RegisterFunction("QuestSignDisappear", this, SymbolExtensions.GetMethodInfo(() => QuestSignDisappear((double) 0)));

    }

    void OnDisable()
    {
        if (unregisterOnDisable)
        {
            // Remove the functions from Lua: (Replace these lines with your own.)
            // Lua.UnregisterFunction("DebugLog");
            // Lua.UnregisterFunction("AddOne");
            Lua.UnregisterFunction("QuestSignAppear");  
            Lua.UnregisterFunction("QuestSignDisappear");

        }
    }

   #endregion
}
