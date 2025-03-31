using System;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    [Serializable]
    public class SoundManager : Singleton<SoundManager>
    {
        public List<AudioClip> audioClips;
        private AudioSource _audioSource;

        // Event handler for the "PlaySound" event that plays the sound effect
        // and stops the current sound effect if it is playing
        public void PlaySound(string soundName, float volume = 1f)
        {
            _audioSource = GetComponent<AudioSource>();
            AudioClip clip = audioClips.Find(sound => sound.name == soundName);
            if (clip)
            {
                _audioSource.Stop();
                _audioSource.PlayOneShot(clip, volume);
            }
        }

        // Event handler for the "PlaySoundLoop" event that plays the sound effect
        // in a loop and stops the current sound effect if it is playing
        public void PlaySoundLoop(string soundName, float volume = 1f)
        {
            _audioSource = GetComponent<AudioSource>();
            AudioClip clip = audioClips.Find(sound => sound.name == soundName);
            if (clip)
            {
                _audioSource.Stop();
                _audioSource.clip = clip;
                _audioSource.volume = volume;
                _audioSource.loop = true;
                _audioSource.Play();
            }
        }
    }
}