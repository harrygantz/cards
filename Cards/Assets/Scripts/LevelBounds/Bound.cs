using System.Collections;
using System.Collections.Generic;
using DEAL;
using DEAL.Cards;
using DEAL.Data;
using UnityEngine;
using DEAL.Tools;

namespace DEAL.Tools
{
    public class Bound : MonoBehaviour
    {
        public string acceptedCardType;
        private CardSpawner _cardSpawner;

        private void Awake()
        {
            _cardSpawner = FindObjectOfType<CardSpawner>();
        }

        /// <summary>
        /// Checks to see if our card is one of the accepted card types if so and it has left the level bound and then sets
        /// it to inactive using our Destroy() method not Unity's Destroy() method which would destroy it.
        /// </summary>
        /// <param name="other"></param> other is referring to the item with a collider that is being interacted with in this
        /// case it is the card. 
        void OnTriggerExit(Collider other) 
        {
            if (other.gameObject.CompareTag(acceptedCardType) || acceptedCardType == "Clear")
            {
                if (acceptedCardType == "Clear")
                {
                    acceptedCardType = other.gameObject.tag;
                }
                other.gameObject.GetComponent<PoolableObject>().Destroy();
                GameManager.Instance.points += other.gameObject.GetComponent<CardDisplay>().points;
                _cardSpawner.SpawnCard(_cardSpawner.transform.position, _cardSpawner.transform.rotation, 4);
            }
        }
    }

}

