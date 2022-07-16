using System;
using UnityEngine;

namespace Snake.Audio
{
    public class AudioReproducer : MonoBehaviour
    {
        [Header("Audio Source")]
        [SerializeField] private AudioSource _sourceMusic;
        [SerializeField] private AudioSource _sourceEffect;
        
        [Header("Effect")]
        [SerializeField] private AudioClip _apple;
        [SerializeField] private AudioClip _turned;
        [SerializeField] private AudioClip _bitted;

        private void Awake()
        {
            if (Instance is null)
                Instance = this;
            else
                Destroy(gameObject);
            
            DontDestroyOnLoad(gameObject);
        }

        public static AudioReproducer Instance { get; private set; }

        public void PlayAppleEffect()
        {
            _sourceEffect.clip = _apple;
            _sourceEffect.Play();
        }
        
        public void PlayTurnedEffect()
        {
            _sourceEffect.clip = _turned;
            _sourceEffect.Play();
        }
        
        public void PlayBittedEffect()
        {
            _sourceEffect.clip = _bitted;
            _sourceEffect.Play();
        }
        
        public void TurnOnMusic()
        {
            _sourceMusic.volume = 1f;
            Debug.Log("TurnOnMusic");
        }

        public void TurnOffMusic()
        {
            _sourceMusic.volume = 0f;
            Debug.Log("TurnOffMusic");
        }

        public void TurnOnEffect()
        {
            _sourceEffect.volume = 1f;
            Debug.Log("TurnOnSound");
        }

        public void TurnOffEffect()
        {
            _sourceEffect.volume = 0f;
            Debug.Log("TurnOffSound");
        }
    }
}