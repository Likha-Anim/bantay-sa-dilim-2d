using UnityEngine;

namespace Miscellaneous
{
    public class CreditsScroll : MonoBehaviour
    {
        public float scrollSpeed = 120f;
        private RectTransform _rectTransform;

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            float bottomY = _rectTransform.anchoredPosition.y - (_rectTransform.rect.height / 2);
            if (bottomY >= 1000) enabled = false; // Stop scrolling when condition is met
            transform.Translate(Vector3.up * scrollSpeed * Time.deltaTime);
        }
    }
}