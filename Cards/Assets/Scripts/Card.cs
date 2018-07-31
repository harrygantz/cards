using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DEAL.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Base Card", menuName = "Cards/Base")]
    public class Card : ScriptableObject, IPooledObject
    {
        // Basic Info
        public new string name; // name of the card
        public string description; // used to describe the card
        public int pointValue; // the value the card is worth if swipped successfully
        public Material matertial; //
        public Color32 color;

        //Use this for initialization instead of Unity's Start() method
        public void OnObjectSpawn()
        {
            
        }
    }
}

