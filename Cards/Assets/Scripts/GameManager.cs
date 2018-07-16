using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DEAL
{
    public class GameManager : MonoBehaviour
    { 
        private bool _isGameOver;
        public bool IsGameOver { get { return _isGameOver; } }
        
        // reference to objective
        private Objective _objective;
        
        [SerializeField]
        private string _nextLevelName;
        
        [SerializeField]
        private int _nextLevelIndex;

        private static GameManager _instance;

        public static GameManager Instance { get { return _instance; } }

        // initialize references
        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
            }
            _objective = Object.FindObjectOfType<Objective>();
        }

        private void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }

        // end the level
        public void EndLevel()
        {
            // When the endgame condtion has been met we will give the player a few options:
            //    - We can restart the level right away. 
            //    - If the player has been unsucessful x amount of times give them an "out".
            //    - 
            // check if we have set IsGameOver to true, only run this logic once
           
               //Level Loading
               //LoadLevel(_nextLevelIndex);
               //LoadNextLevel();
            
        }

        // check for the end game condition on each frame
        private void Update()
        {
            if (_objective != null && _objective.IsComplete)
            {
                EndLevel();
            }
        }

        private void LoadLevel(string levelName)
        {
            if (!Application.CanStreamedLevelBeLoaded(levelName))
            {
                Debug.LogWarning("GAMEMANAGER LoadLevel Error: Invalad scene name specified!");
            }
            else
            {
                SceneManager.LoadScene(levelName);
            }
        }

        private void LoadLevel(int levelIndex)
        {
            if (levelIndex >= 0 && levelIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(levelIndex);
            }
            else
            {
                Debug.LogWarning("GAMEMANAGER LoadLevel Error: Invalad scene index specified!");
            }
        }

        public void ReloadLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void LoadNextLevel()
        {
            //If the current scene is not last scene in the build index
            if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                SceneManager.LoadScene(0);
            }
        }
    }

}
    