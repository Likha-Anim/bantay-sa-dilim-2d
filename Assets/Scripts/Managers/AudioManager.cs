using UnityEngine;

namespace Managers
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }
        private AudioSource _audioSource;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            _audioSource = gameObject.AddComponent<AudioSource>();
        }

        public void PlaySound(AudioClip clip, float volume)
        {
            if (clip != null)
            {
                _audioSource.PlayOneShot(clip, volume);
            }
        }
    }
}