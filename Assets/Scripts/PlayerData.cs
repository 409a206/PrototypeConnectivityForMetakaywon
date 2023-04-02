using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
   public static class QuestData
   {    
        public static List<QuestCode> QuestsInactive = new List<QuestCode>(); //세이브 파일에서 받아오는 로직 작성하기
        public static List<QuestCode> QuestsActive = new List<QuestCode>(); //세이브 파일에서 받아오는 로직 작성하기
        public static List<QuestCode> QuestsComplete = new List<QuestCode>(); //세이브 파일에서 받아오는 로직 작성하기
        public enum QuestCode
        {
            Q1, Q2, Q3, Q4, Q5, Q6, Q7
        }
   }


}
