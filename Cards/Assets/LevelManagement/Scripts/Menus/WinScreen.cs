using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelManagement
{
    public class WinScreen : Menu<WinScreen> 
    {
        public void OnNextLevelPressed()
        {
            // Since we are using a stack and the GameMenu is below this (i.e. the icon on the screen) we want to pop
            // this menu and that is what OnBackPressed() does from the base Menu class. Also note the order is
            // importatnt otherwise the correct menu may not show up.
            base.OnBackPressed();
            
            LevelLoader.LoadNextLevel();
        }

        public void OnRestartPressed()
        {
            base.OnBackPressed();
            
            LevelLoader.ReloadLevel();
        }

        public void OnMainMenuPressed()
        {
            LevelLoader.LoadMainMenuLevel();
            MainMenu.Open();
            
        }
    }
}

