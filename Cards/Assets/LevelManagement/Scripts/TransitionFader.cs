﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelManagement
{
    public class TransitionFader : ScreenFader 
    {
        [SerializeField]
        private float _lifetime = 1f;
        
        [SerializeField]
        private float _delay = 0f;
        
        [SerializeField]
        private float _maxLifetime = 10f;

        private void Awake()
        {
            // Want to make sure we don't enter an invalid lifetime so we use Mathf.Clap(Value, Min, Max) to make sure
            // the lifetime at minimum accounts for the FadeOnDuration, FadeOffDuration, and the _delay.
            _lifetime = Mathf.Clamp(_lifetime, FadeOnDuration + FadeOffDuration + _delay, _maxLifetime);
        }

        private IEnumerator PlayRoutine()
        {
            SetAlpha(_clearAlpha);            
            yield return new WaitForSeconds(_delay);
            
            FadeOn();
            
            float onTime = _lifetime - (FadeOffDuration + _delay);
            yield return new WaitForSeconds(onTime);
            
            FadeOff();
            Destroy(gameObject, FadeOffDuration);
        }

        public void Play()
        {
            StartCoroutine(PlayRoutine());
        }
        
        // We want a transition screen to be able to be invoked anywhere at any time by any object so it makes sense to
        // make a public static method to deploy it.
        public static void PlayTransition(TransitionFader transitionPrefab)
        {
            if (transitionPrefab != null)
            {
                TransitionFader instance = Instantiate(transitionPrefab, Vector3.zero, Quaternion.identity);
                instance.Play();
            }
        }
    }
}

