using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
   public static class QuestData
   {
        static bool isTalkWithLibrarianComplete;
        static bool isTalkWithPresidentComplete;
        static bool isTalkWithProfSoekComplete;
        public static bool IsTalkWithPresidentComplete
        {
            get{return isTalkWithPresidentComplete;}
            set{isTalkWithPresidentComplete = value;}
        }
        public static bool IsTalkWithProfSoekComplete
        {
            get{return isTalkWithProfSoekComplete;}
            set{isTalkWithProfSoekComplete = value;}
        }
        public static bool IsTalkWithLibrarianComplete
        {
            get{return isTalkWithLibrarianComplete;}
            set{isTalkWithLibrarianComplete = value;}
        }
   }


}
