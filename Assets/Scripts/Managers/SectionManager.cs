using System.Collections;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Manager
{
    public class SectionManager : Singleton<SectionManager>
    {
        public float fadeDuration = 1f;
        public float fadeDelay = 0.5f;

        private GameObject _currentSection;
        private Coroutine _fadeCoroutine;

        // Event handler for the "SwitchSection" event that changes the background
        // that fades out the old section and fades in the new section
        public IEnumerator SwitchSection(string newSection)
        {
            if (_currentSection)
            {
                if (_fadeCoroutine != null) StopCoroutine(_fadeCoroutine);
                _fadeCoroutine = StartCoroutine(FadeOutImage(_currentSection));
                yield return new WaitForSeconds(fadeDelay);
                _currentSection.SetActive(false);
            }

            Debug.Log("New section is " + newSection);
            _currentSection = Utilities.FindChild(newSection).gameObject;

            if (_currentSection)
            {
                if (_fadeCoroutine != null) StopCoroutine(_fadeCoroutine);
                _fadeCoroutine = StartCoroutine(FadeInImage(_currentSection));
            }
        }

        // Fades in the new section
        private IEnumerator FadeInImage(GameObject newSection)
        {
            newSection.SetActive(true);
            Image newImage = newSection.GetComponent<Image>();
            if (newImage)
            {
                yield return StartCoroutine(Utilities.FadeImage(newImage, 0, 1, fadeDuration));
            }
        }

        // Fades out the old section
        private IEnumerator FadeOutImage(GameObject oldSection)
        {
            Image oldImage = oldSection.GetComponent<Image>();
            if (oldImage)
            {
                yield return StartCoroutine(Utilities.FadeImage(oldImage, 1, 0, fadeDuration));
            }
        }
    }
}