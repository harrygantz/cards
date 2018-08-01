using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DEAL.Tools;

namespace DEAL
{
    [RequireComponent (typeof (ObjectPoolerBase))]
    public class Spawner : MonoBehaviour 
    {
        /// Tells the game if it can spawn another card or not.
        [SerializeField]
        public bool spawning = true;
        
        /// Initial delay before the first spawn, in seconds.
        public float initialDelay = 0f;
        
        protected ObjectPoolerBase _objectPoolerBase;
        protected float _startTime;
        
        /// <summary>
        /// On awake, we get the objectPooler component
        /// </summary>
        protected virtual void Awake()
        {
            _objectPoolerBase = GetComponent<ObjectPoolerBase>();
            _startTime = Time.time;
        }
        
        protected GameObject Spawn(Vector3 spawnPosition, Quaternion spawnRotation, bool triggerObjectActivation=true )
        {
            
            if ((Time.time - _startTime < initialDelay) || (!spawning))
            {
                return null;
            }
            
            GameObject nextGameObject = _objectPoolerBase.GetPooledGameObject();
            if (nextGameObject == null)
            {
                Debug.LogWarning("CARDSPAWNER Spawn: The nextGameObject has returned null");
                return null;
            }
            
            // Check to see if the gameObject is a poolable object.
            if (nextGameObject.GetComponent<PoolableObject>()==null)
            {
                throw new Exception(gameObject.name+" is trying to spawn objects that don't have a PoolableObject component.");					
            }
            
            // we position the object
            nextGameObject.transform.position = spawnPosition;
            
            // we set the object's rotation
            nextGameObject.transform.rotation = spawnRotation;
            
            // we activate the object
            nextGameObject.gameObject.SetActive(true);
            
            if (triggerObjectActivation)
            {
                if (nextGameObject.GetComponent<PoolableObject>()!=null)
                {
                    nextGameObject.GetComponent<PoolableObject>().TriggerOnSpawnComplete();
                }
                foreach (Transform child in nextGameObject.transform)
                {
                    if (child.gameObject.GetComponent<ReactivateOnSpawn>()!=null)
                    {
                        child.gameObject.GetComponent<ReactivateOnSpawn>().Reactivate();
                    }
                }
            }

            return nextGameObject;
        }
    }
}

