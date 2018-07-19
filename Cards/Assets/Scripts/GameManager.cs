using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using LevelManagement;

namespace DEAL
{
    public class GameManager : MonoBehaviour
    { 
        private bool _isGameOver;
        public bool IsGameOver { get { return _isGameOver; } }
        
        // reference to objective
        private Objective _objective;
        
        private static GameManager _instance;
        public static GameManager Instance { get { return _instance; } }
        
        [SerializeField]
        private TransitionFader _endTransitionPrefab;
        
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

            // check if we have set IsGameOver to true, only run this logic once
            if (!_isGameOver)
            {
                _isGameOver = true;
                
                // Play some effect to congradulate the player
                
                // Because we have a screen transition this means we need to set up a transtion manually in each level
                // so if we have a bunch of short levels this may be obnoxious. If that ends up being the case we can
                // refactor the way we handle transitions here.
                StartCoroutine(WinRoutine());
            }   
        }

        private IEnumerator WinRoutine()
        {
            TransitionFader.PlayTransition(_endTransitionPrefab);

            // If we do not account for the time the transition fader is fading then we will get a visual glitch from
            // the next screen popping into view without a smooth fade transition.
            float  fadeDelay = (_endTransitionPrefab != null) ? 
                _endTransitionPrefab.Delay + _endTransitionPrefab.FadeOnDuration : 0f;
            
            yield return new WaitForSeconds(fadeDelay);
            WinScreen.Open();
        }

        // check for the end game condition on each frame
        private void Update()
        {
            if (_objective != null && _objective.IsComplete)
            {
                EndLevel();
            }
        }
    }

}
    