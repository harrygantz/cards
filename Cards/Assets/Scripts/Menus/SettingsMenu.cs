using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DEAL.Data;

namespace DEAL.LevelManagement
{
  
    public class SettingsMenu : Menu<SettingsMenu>
    {
        [SerializeField]
        private Slider _masterVolumeSlider;
        
        [SerializeField]
        private Slider _musicVolumeSlider;
        
        [SerializeField]
        private Slider _sfxVolumeSlider;

        private DataManager _dataManager;
        
        protected override void Awake()
        {
            base.Awake();
            _dataManager = FindObjectOfType<DataManager>();
            LoadPreferences();
        }
        
        public void OnMasterVolumeChanged(float volume)
        {
            if (_dataManager != null)
            {
                _dataManager.MasterVolume = volume;
            }
        }

        public void OnSFXVolumeChanged(float volume)
        {
            if (_dataManager != null)
            {
                _dataManager.SfxVolume = volume;
            }
        }

        public void OnMusicVolumeChanged(float volume)
        {
            if (_dataManager != null)
            {
                _dataManager.MusicVolume = volume;
            }
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
            // It's good practice to save to a disk on transitions so that the players don't notice minor hiccups that
            // may occur during saving.
            if (_dataManager != null)
            {
                _dataManager.Save();
            }
        }

        private void LoadPreferences()
        {
            if (_dataManager != null)
            {
                _dataManager.Load();
                
                _masterVolumeSlider.value = _dataManager.MasterVolume;
                _musicVolumeSlider.value = _dataManager.MusicVolume;
                _sfxVolumeSlider.value = _dataManager.SfxVolume;
            } 
        }
    } 
}

