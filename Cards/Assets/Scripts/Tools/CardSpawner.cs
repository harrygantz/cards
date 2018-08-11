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
        private int _initalizedCardCount = 5;

        /// Initialize the stack that will hold all the cards in the deck
        public static Stack<GameObject> deck = new Stack<GameObject>();
        
        /// <summary>
        /// This is used to keep track of the postitions of the first few cards in the deck
        /// </summary>
        private GameObject firstSpawnedObject;
        private GameObject secondSpawnedObject;
        private GameObject thirdSpawnedObject;
        private GameObject fourthSpawnedObject;
        private GameObject lastSpawnedObject;
        
        private Transform _firstSpawnedTransform = null;

        
        /// Check to see if the game is just beginning.
        private bool _isStartGame = true;

        public bool runOnce = true;

        private void Start()
        {
            /// we get the object pooler component
            _objectPoolerBase = GetComponent<ObjectPoolerBase>();            
            InitalizeDeck(_initalizedCardCount);
        }

        private void FixedUpdate()
        {
            CheckSpawn();
        }

        /// <summary>
        /// Initalizes the deck by spawning a predetermined number of cards in a stack.
        /// </summary>
        public void InitalizeDeck(int numOfCards)
        {
            for (int i = 0; i < numOfCards; i++)
            {
                    // The cards are being spawned and placed under each other, Roateded in the same direction, and 
                    // increasing the card count by one each time.
                    SpawnCard( new Vector3(0f,0f,(0.02f * i)), Quaternion.identity, (i + 1) );
            }
            
            _isStartGame = false;
        }
        
        /// <summary>
        /// Checks if the conditions for a new spawn are met, and if so, triggers the spawn of a new object
        /// </summary>
        private void CheckSpawn()
        {
            // if we've already spawned at least one object, we'll reposition our new object according to that previous one
            if (!_firstSpawnedTransform.gameObject.activeInHierarchy && runOnce)
            {   
                firstSpawnedObject = secondSpawnedObject;
                firstSpawnedObject.transform.position = new Vector3(0f, 0f, 0.00f);
                secondSpawnedObject = thirdSpawnedObject;
                secondSpawnedObject.transform.position = new Vector3(0f, 0f, 0.02f);
                thirdSpawnedObject = fourthSpawnedObject;
                thirdSpawnedObject.transform.position = new Vector3(0f, 0f, 0.04f);
                fourthSpawnedObject = lastSpawnedObject;
                fourthSpawnedObject.transform.position = new Vector3(0f, 0f, 0.06f);
                SpawnCard(new Vector3(0f, 0f, 0.08f), Quaternion.identity, _initalizedCardCount); 
               
                runOnce = false;
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
            if (count < _initalizedCardCount)
            {
                if (count == 1)
                {
                    // we spawn a gameobject at the location we've determined previously
                     firstSpawnedObject = Spawn(spawnPosition,spawnRotation,false);
                    _firstSpawnedTransform = firstSpawnedObject.transform;
                    firstSpawnedObject.GetComponent<PoolableObject>().TriggerOnSpawnComplete();
                    foreach (Transform child in firstSpawnedObject.transform)
                    {
                        if (child.gameObject.GetComponent<ReactivateOnSpawn>()!=null)
                        {
                            child.gameObject.GetComponent<ReactivateOnSpawn>().Reactivate();
                        }
                    }
                    _cardCount++;
                }
                
                if (count == 2)
                {
                    // we spawn a gameobject at the location we've determined previously
                     secondSpawnedObject = Spawn(spawnPosition,spawnRotation,false);
                    
                    secondSpawnedObject.GetComponent<PoolableObject>().TriggerOnSpawnComplete();
                    foreach (Transform child in secondSpawnedObject.transform)
                    {
                        if (child.gameObject.GetComponent<ReactivateOnSpawn>()!=null)
                        {
                            child.gameObject.GetComponent<ReactivateOnSpawn>().Reactivate();
                        }
                    }
                    _cardCount++;
                }
                
                if (count == 3)
                {
                    // we spawn a gameobject at the location we've determined previously
                     thirdSpawnedObject = Spawn(spawnPosition,spawnRotation,false);
                    
                    thirdSpawnedObject.GetComponent<PoolableObject>().TriggerOnSpawnComplete();
                    foreach (Transform child in thirdSpawnedObject.transform)
                    {
                        if (child.gameObject.GetComponent<ReactivateOnSpawn>()!=null)
                        {
                            child.gameObject.GetComponent<ReactivateOnSpawn>().Reactivate();
                        }
                    }
                    _cardCount++;
                }
                
                if (count == 4)
                {
                    // we spawn a gameobject at the location we've determined previously
                    fourthSpawnedObject = Spawn(spawnPosition,spawnRotation,false);
                    
                    fourthSpawnedObject.GetComponent<PoolableObject>().TriggerOnSpawnComplete();
                    foreach (Transform child in fourthSpawnedObject.transform)
                    {
                        if (child.gameObject.GetComponent<ReactivateOnSpawn>()!=null)
                        {
                            child.gameObject.GetComponent<ReactivateOnSpawn>().Reactivate();
                        }
                    }
                    _cardCount++;
                }
                
                return;
            }

            // we spawn a gameobject at the location we've determined previously
            lastSpawnedObject = Spawn(spawnPosition,spawnRotation,false);
            
            // we need to have a poolableObject component for the distance spawner to work.
            if (lastSpawnedObject.GetComponent<PoolableObject>()==null)
            {
                throw new Exception(gameObject.name+" is trying to spawn objects that don't have a PoolableObject component.");					
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
            
        }
    }//ENDCARDSPAWNER
}//END NAMESPACE
