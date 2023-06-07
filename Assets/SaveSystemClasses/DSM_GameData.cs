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
        public SerializableDictionnary<int, SKINSTATE> SkinDictionnary;
        public float Volume;
   }

    public enum SKINSTATE
    {
        NOT_ACQUIRED,
        ACQUIRED,
        EQUIPPED,
    }
}