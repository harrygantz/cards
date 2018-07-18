using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelManagement
{
    public class LevelLoader : MonoBehaviour
    {
        // If we need to change the mainMenuIndex we will need to change it here from this script because
        // it won't show up in the Unity inspector because it is a static variable.
        private static int mainMenuIndex = 1;
        
        public static void LoadLevel(string levelName)
        {
            if (!Application.CanStreamedLevelBeLoaded(levelName))
            {
                Debug.LogWarning("LEVELLOADER LoadLevel Error: Invalad scene name specified!");
            }
            else
            {
                SceneManager.LoadScene(levelName);
            }
        }

        public static void LoadLevel(int levelIndex)
        {
            if (levelIndex >= 0 && levelIndex < SceneManager.sceneCountInBuildSettings)
            {
                if (levelIndex == mainMenuIndex)
                {
                    MainMenu.Open();
                }
                
                SceneManager.LoadScene(levelIndex);
            }
            else
            {
                Debug.LogWarning("LEVELLOADER LoadLevel Error: Invalad scene index specified!");
            }
        }

        public static void ReloadLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public static void LoadNextLevel()
        {
            //If the current scene is not last scene in the build index go to the next scene
            if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else //Otherwise go to the main menu when you are on the last scene
            {
                SceneManager.LoadScene(mainMenuIndex);
            }
        }

        public static void LoadMainMenuLevel()
        {
            LoadLevel(mainMenuIndex);
        }
    }

}

