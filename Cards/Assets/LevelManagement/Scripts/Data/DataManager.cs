using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelManagement.Data
{
    // Since we are using MVC and SaveData is our container we will need a public corresponding property for each field
    // stored in that class placed in this class.
    public class DataManager : MonoBehaviour
    {
        private SaveData _saveData;
        private JSONSaver _jsonSaver;

        public string PlayerName
        {
            get { return _saveData.playerName; }
            set { _saveData.playerName = value; }
        }
        
        public float MasterVolume
        {
            get {return _saveData.masterVolume;}
            set { _saveData.masterVolume = value; }
        }

        public float SfxVolume
        {
            get { return _saveData.sfxVolume; }
            set { _saveData.sfxVolume = value; }
        }

        public float MusicVolume
        {
            get { return _saveData.musicVolume; }
            set { _saveData.musicVolume = value; }
        }

        private void Awake()
        {
            // Want to invoke SaveData's constructor since it is not a monobehavior. So every time we start the game we
            // are going to create a new object to store our save data.
            _saveData = new SaveData();
            // Similar to above, we want to create a new instance of JSONSaver with the new keyword and we can do that
            // because we aren't inheriting from a monobehavior. 
            _jsonSaver = new JSONSaver();
        }

        public void Save()
        {
            _jsonSaver.Save(_saveData);
        }

        public void Load()
        {
            _jsonSaver.Load(_saveData);
        }
    }

}

