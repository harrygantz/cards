using System.Collections;
using System.Collections.Generic;
using System;

namespace LevelManagement.Data
{
    // This is going to be just a container for information regarding save data. Nothing else. 
    [Serializable]
    public class SaveData
    {
        // As our game expands and we need to save more data we will just define some extra fields in here with whatever
        // type of data we want to save.
        public string playerName;
        private readonly string defaultPlayerName = "Player";

        public float masterVolume;
        public float sfxVolume;
        public float musicVolume;

        public string hashValue;

        // Since we don't have an automatic Awake() method to initialize our values instead we are going to use a public
        // constructor for our SaveData class.
        public SaveData()
        {
            playerName = defaultPlayerName;
            masterVolume = 1;
            sfxVolume = 0.5f;
            musicVolume = 0.5f;
            hashValue = String.Empty;
        }
    }
}

