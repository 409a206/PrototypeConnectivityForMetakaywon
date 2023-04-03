using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string name;
    public bool isMedalTaken = false;
    public PlayerData.QuestData.QuestCode questCode;
    public GameObject questIcon;
    
    //선행퀘스트 목록
    public PlayerData.QuestData.QuestCode[] requirements;

    public string clipURL;
    [TextArea(3, 10)]
    public string[] sentences;

}
