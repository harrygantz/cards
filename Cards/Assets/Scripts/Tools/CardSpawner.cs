using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DEAL.Tools;

namespace DEAL.Tools
{
    public class CardSpawner : Spawner
    {   
        // Keeps count of the number of cards the player has discarded.
        /// #### May want to place this field in another class like the gameManager.
        [SerializeField]
        private int _cardCount;

        [SerializeField] 
        public int _initalizedCardCount = 5;

        /// <summary>
        /// This is used to keep track of the postitions of the first few cards in the deck
        /// </summary>
        private GameObject firstSpawnedObject;
        private GameObject secondSpawnedObject;
        private GameObject thirdSpawnedObject;
        private GameObject fourthSpawnedObject;
        private GameObject lastSpawnedObject;
        
        private Transform _firstSpawnedTransform = null;
        private Transform _secondSpawnedTransform = null;
        private Transform _thirdSpawnedTransform = null;
        private Transform _fourthSpawnedTransform = null;
        private Transform _lastSpawnedTransform = null; 
        
        /// Check to see if the game is just beginning.
        private bool _isStartGame = true;

        private void Start()
        {
            /// we get the object pooler component
            _objectPoolerBase = GetComponent<ObjectPoolerBase>();            
            InitalizeDeck(_initalizedCardCount);
        }

        private void Update()
        {
            CheckSpawn();
        }

        /// <summary>
        /// Initalizes the deck by spawning a predetermined number of cards in a stack.
        /// </summary>
        private void InitalizeDeck(int numOfCards)
        {
            for (int i = 0; i < numOfCards; i++)
            {
                if (i == 0)
                {
                    SpawnCard( new Vector3(0f,0f,0.0f), Quaternion.identity, i+1);
                }
                if (i == 1)
                {
                    SpawnCard( new Vector3(0f,0f,0.02f), Quaternion.identity, i+1);
                }
                if (i == 2)
                {
                    SpawnCard( new Vector3(0f,0f,0.04f), Quaternion.identity, i+1);
                }
                if (i == 3)
                {
                    SpawnCard( new Vector3(0f,0f,0.06f), Quaternion.identity, i+1);
                    break; // dont need to finish any more iterations  
                }
            }

            // Change the start game to false to stop the initializeation process.
            _isStartGame = false;
        }
        
        /// <summary>
        /// Checks if the conditions for a new spawn are met, and if so, triggers the spawn of a new object
        /// </summary>
        private void CheckSpawn()
        {
            
            // if we haven't spawned anything yet, or if the last spawned transform is inactive, we reset to first spawn.
            if (!_firstSpawnedTransform.gameObject.activeInHierarchy)
            {
                SpawnCard(new Vector3(0f, 0f, 0.1f), Quaternion.identity, _cardCount);
                _cardCount++;
                return;
            }
        }

        /// <summary>
        /// Spawns an object at the specified position and determines the next spawn position
        /// </summary>
        /// <param name="spawnPosition"> The position of the spawned objet.</param>
        /// <param name="spawnRotation"> the rotation of the spawned object.</param>
        public void SpawnCard(Vector3 spawnPosition, Quaternion spawnRotation, int count)
        {
            // need to save a reference to each game object so we can move them around
            if (count < 5)
            {
                if (count == 1)
                {
                    // we spawn a gameobject at the location we've determined previously
                     firstSpawnedObject = Spawn(spawnPosition,spawnRotation,false);
                    _firstSpawnedTransform = firstSpawnedObject.transform;
                    _cardCount++;
                }
                
                if (count == 2)
                {
                    // we spawn a gameobject at the location we've determined previously
                     secondSpawnedObject = Spawn(spawnPosition,spawnRotation,false);
                    _secondSpawnedTransform = secondSpawnedObject.transform;
                    _cardCount++;
                }
                
                if (count == 3)
                {
                    // we spawn a gameobject at the location we've determined previously
                     thirdSpawnedObject = Spawn(spawnPosition,spawnRotation,false);
                    _thirdSpawnedTransform = thirdSpawnedObject.transform;
                    _cardCount++;
                }
                
                if (count == 4)
                {
                    // we spawn a gameobject at the location we've determined previously
                    fourthSpawnedObject = Spawn(spawnPosition,spawnRotation,false);
                    _fourthSpawnedTransform = fourthSpawnedObject.transform;
                    _cardCount++;
                }
                
                return;
            }

            // we spawn a gameobject at the location we've determined previously
            lastSpawnedObject = Spawn(new Vector3(0f,0f,0.8f),spawnRotation,false);
            
            // we need to have a poolableObject component for the distance spawner to work.
            if (lastSpawnedObject.GetComponent<PoolableObject>()==null)
            {
                throw new Exception(gameObject.name+" is trying to spawn objects that don't have a PoolableObject component.");					
            }
            
            // if we've already spawned at least one object, we'll reposition our new object according to that previous one
            if (_firstSpawnedTransform != null)
            {   
                // we store our new object, which will now be the previously spawned object for our next spawn
                //swap position of the second card with the first card since the first card has been discarded.
                firstSpawnedObject = secondSpawnedObject;
                firstSpawnedObject.transform.position = _firstSpawnedTransform.position;
                
                // swap the position of the third card with the second card to bump it up a position.
                secondSpawnedObject = thirdSpawnedObject;
                secondSpawnedObject.transform.position = _secondSpawnedTransform.position; 
                
                // swap the position of the fourth card with the third cards position moving it up a spot.
                thirdSpawnedObject = fourthSpawnedObject;
                thirdSpawnedObject.transform.position = _thirdSpawnedTransform.position;
            
                // swap the position of the last card with the fourth card position moving it to the last position in 
                // the deck
                fourthSpawnedObject = lastSpawnedObject;
                fourthSpawnedObject.transform.position = _lastSpawnedTransform.position;
                
                lastSpawnedObject = Spawn(new Vector3(0f,0f,0.8f),spawnRotation,false);
                _cardCount++;
                // we center our object on the spawner's position
                lastSpawnedObject.transform.position = new Vector3(0f,0f, .02f * _initalizedCardCount);
                
            }
            
            // we tell our object it's now completely spawned
            // we should use events to do this later on. 
            lastSpawnedObject.GetComponent<PoolableObject>().TriggerOnSpawnComplete();
            // we increment the card count.
            _cardCount++;
            foreach (Transform child in lastSpawnedObject.transform)
            {
                if (child.gameObject.GetComponent<ReactivateOnSpawn>()!=null)
                {
                    child.gameObject.GetComponent<ReactivateOnSpawn>().Reactivate();
                }
            }
            
            //*** ORIGINAL ***
            _lastSpawnedTransform = lastSpawnedObject.transform;
            Debug.Log("Card count: " +_cardCount);
        }
    }//ENDCARDSPAWNER
}//END NAMESPACE
