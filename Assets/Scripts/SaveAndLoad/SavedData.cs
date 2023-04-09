using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//(메달 코인 갯수 추가하기)
[System.Serializable]
public class SavedData
{
    //저장 및 불러오기 할 정보들 
    public List<PlayerData.QuestData.QuestCode> QuestsInactive;
    public List<PlayerData.QuestData.QuestCode> QuestsActive;
    public List<PlayerData.QuestData.QuestCode> QuestsComplete;
    
    public SavedData() {
        QuestsInactive = PlayerData.QuestData.QuestsInactive;
        QuestsActive = PlayerData.QuestData.QuestsActive;
        QuestsComplete = PlayerData.QuestData.QuestsComplete;
    }
}
