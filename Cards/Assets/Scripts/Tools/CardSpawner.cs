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
        
        protected Transform _lastSpawnedTransform;

        protected virtual void Start () 
        {
            /// we get the object pooler component
            _objectPoolerBase = GetComponent<ObjectPoolerBase> ();	
        }

        private void Update()
        {
            CheckSpawn();
        }

        /// <summary>
        /// Checks if the conditions for a new spawn are met, and if so, triggers the spawn of a new object
        /// </summary>
        private void CheckSpawn()
        {
            
            // if we haven't spawned anything yet, or if the last spawned transform is inactive, we reset to first spawn.
            if ((_lastSpawnedTransform== null) || (!_lastSpawnedTransform.gameObject.activeInHierarchy))
            {
                SpawnCard(transform.position, Quaternion.identity);	
                return;
            }
        }

        /// <summary>
        /// Spawns an object at the specified position and determines the next spawn position
        /// </summary>
        /// <param name="spawnPosition">Spawn position.</param>
        protected virtual void SpawnCard(Vector3 spawnPosition, Quaternion spawnRotation)
        {
            // we spawn a gameobject at the location we've determined previously
            GameObject spawnedObject = Spawn(spawnPosition,spawnRotation,false);
            
            // if the spawned object is null, we're gonna start again with a fresh spawn next time we get fresh objects.
            if (spawnedObject==null)
            {
                _lastSpawnedTransform=null;
                return;
            }
            
            // we need to have a poolableObject component for the distance spawner to work.
            if (spawnedObject.GetComponent<PoolableObject>()==null)
            {
                throw new Exception(gameObject.name+" is trying to spawn objects that don't have a PoolableObject component.");					
            }
            
            // if we've already spawned at least one object, we'll reposition our new object according to that previous one
            if (_lastSpawnedTransform != null)
            {
                // we center our object on the spawner's position
                spawnedObject.transform.position = transform.position;
            }
            
            //we tell our object it's now completely spawned
            spawnedObject.GetComponent<PoolableObject>().TriggerOnSpawnComplete();
            foreach (Transform child in spawnedObject.transform)
            {
                if (child.gameObject.GetComponent<ReactivateOnSpawn>()!=null)
                {
                    child.gameObject.GetComponent<ReactivateOnSpawn>().Reactivate();
                }
            }
            
            // we store our new object, which will now be the previously spawned object for our next spawn
            _lastSpawnedTransform = spawnedObject.transform;

        }
    }//ENDCARDSPAWNER
}//END NAMESPACE
