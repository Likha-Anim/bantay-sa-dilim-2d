using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class TypingManager : Singleton<TypingManager>
    {
        public float typingSpeed = 0.03f;
        public float delayBetweenMessages = 0.5f;

        private Queue<string> _textQueue = new Queue<string>();
        private TextMeshProUGUI _textDisplay;
        private string _currentText;
        private bool _isTyping;

        private void Update()
        {
            if (HasNext() && Input.anyKeyDown)
            {
                SkipTyping();
            }

            if (_textDisplay && HasNext() && !IsTyping())
            {
                StartCoroutine(TypeText(_textQueue.Dequeue()));
            }
        }

        public void EnqueueText(string text)
        {
            _textQueue.Enqueue(text);
        }

        // Event handler for the "TypeText" event that triggers the typing animation
        public IEnumerator TypeText(string text)
        {
            _isTyping = true;
            _textDisplay.text = "";
            _currentText = text;

            foreach (char letter in text)
            {
                _textDisplay.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }

            yield return new WaitForSeconds(delayBetweenMessages);
            _isTyping = false;
        }

        // Initializes the text display for the current UI for the typing animation
        public void InitializeTextDisplay(string newUI)
        {
            if (newUI == "ToBeContinued" || newUI == "Death")
            {
                newUI = $"Screen/{newUI}";
                typingSpeed = 0.1f;
            }
            else typingSpeed = 0.03f;

            _textDisplay = Utilities.FindChild($"Overlay/{newUI}/Sentence")
                .GetComponent<TextMeshProUGUI>();
        }

        private void SkipTyping()
        {
            if (_isTyping)
            {
                StopAllCoroutines();
                _textDisplay.text = _currentText;
                _isTyping = false;
            }
        }

        public bool HasNext()
        {
            return _textQueue.Count > 0;
        }

        // Checks if the typing animation is currently running
        public bool IsTyping()
        {
            return _isTyping;
        }
    }
}