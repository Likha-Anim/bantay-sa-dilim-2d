using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Miscellaneous
{
    public class SimpleGif : MonoBehaviour
    {
        public Sprite[] frames;
        public float frameRate = 0.1f;

        private Image _imageComponent;

        private void Start()
        {
            _imageComponent = GetComponent<Image>();
            if (frames.Length > 0) StartCoroutine(PlayGif());
        }

        private IEnumerator PlayGif()
        {
            int index = 0;
            while (true)
            {
                if (_imageComponent)
                    _imageComponent.sprite = frames[index];

                index = (index + 1) % frames.Length;
                yield return new WaitForSeconds(frameRate);
            }
        }
    }
}