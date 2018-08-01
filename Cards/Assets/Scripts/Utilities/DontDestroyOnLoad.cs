using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DEAL.Utilities
{
    public class DontDestroyOnLoad : MonoBehaviour 
    {
        private void Awake()
        {
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }
    }
}

