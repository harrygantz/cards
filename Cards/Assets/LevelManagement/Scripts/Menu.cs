using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DEAL;

namespace LevelManagement
{
    //Here we are using the curiously recurring template pattern: https://en.wikipedia.org/wiki/Curiously_recurring_template_pattern
    public abstract class Menu<T> : Menu where T : Menu<T>
    {
        private static T _instance;

        public static T Instance { get { return _instance; } }

        protected virtual void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = (T)this;
            }
        }

        public static void Open()
        {
            if (MenuManager.Instance != null && Instance != null)
                MenuManager.Instance.OpenMenu(Instance);
        }
        protected virtual void OnDestroy()
        {
                _instance = null;
        }
    }
    
    [RequireComponent(typeof(Canvas))]
    public abstract class Menu : MonoBehaviour 
    { 
        public virtual void OnBackPressed()
        { 
            if (MenuManager.Instance != null)
            {
                MenuManager.Instance.CloseMenu();
            }
        }
    }    

}

