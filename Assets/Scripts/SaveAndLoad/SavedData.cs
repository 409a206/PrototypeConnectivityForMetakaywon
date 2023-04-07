using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SavedData
{
    public PlayerData playerData;
    
    public SavedData() {
        playerData = new PlayerData();
    }
}
