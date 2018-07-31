using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DEAL.Tools;

namespace DEAL
{
    public class CardSpawner : MonoBehaviour
    {
        // Tells the game if it can spawn another card or not.
        [SerializeField]
        private bool _canSpawn = true;
        
        // Records the starting game time
        /// #### May want to place this field in another class like the gameManager.
        [SerializeField]
        private float _startTime;
        
        // Keeps count of the number of cards the player has discarded.
        /// #### May want to place this field in another class like the gameManager.
        [SerializeField]
        private int _cardCount;

        private ObjectPoolerBase _objectPoolerBase;

        private void Awake()
        {
            _objectPoolerBase = GetComponent<ObjectPoolerBase>();
            _startTime = Time.time;
        }

        public GameObject Spawn(Vector3 spawnPosition, Quaternion spawnRotation, bool triggerObjectActivation=true )
        {
            GameObject nextGameObject = _objectPoolerBase.GetPooledGameObject();
            if (nextGameObject == null)
            {
                Debug.LogWarning("CARDSPAWNER Spawn: The nextGameObject has returned null");
                return null;
            }
            
            if (nextGameObject.GetComponent<PoolableObject>()==null)
            {
                throw new Exception(gameObject.name+" is trying to spawn objects that don't have a PoolableObject component.");					
            }
        }
    }
}
