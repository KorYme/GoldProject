using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KorYmeLibrary.SaveSystem 
{
   public class DSM_GameData : DataSaveManager<GameData>
   {
       // Modify if you're willing to add some behaviour to the component
   }

   [System.Serializable]
   public class GameData : GameDataTemplate
   {
        // Create the values you want to save here
        public SerializableDictionnary<int, int> LevelDictionnary;
        public SerializableDictionnary<Utilities.GAMECOLORS, SKINPACK> SkinEquippedDictionnary;
        public SerializableDictionnary<SKINPACK, bool> SkinAcquiredDictionnary;
        public float Volume;
        public string Version;
   }

    public enum SKINSTATE
    {
        NOT_ACQUIRED,
        ACQUIRED,
        EQUIPPED,
    }

    public enum SKINPACK
    {
        BASIC,
        CHIC,
        CRISTAL,
    }
}