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

    public string clipURL;
    [TextArea(3, 10)]
    public string[] sentences;

}
