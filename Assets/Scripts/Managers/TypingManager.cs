using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class TypingManager : MonoBehaviour
    {
        public static TypingManager Instance { get; private set; }
        [HideInInspector] public TextMeshProUGUI textDisplay;
        public float typingSpeed = 0.05f;
        public float delayBetweenMessages = 1f;

        public int QueueCount => textQueue.Count;
        private Queue<string> textQueue = new Queue<string>();

        private string currentText;
        private bool isTyping = false;

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
        }

        private void Update()
        {
            if (textQueue.Count > 0 && textDisplay && !isTyping)
            {
                StartCoroutine(TypeText(textQueue.Dequeue()));
            }
        }

        public void EnqueueText(string text)
        {
            textQueue.Enqueue(text);
        }

        private IEnumerator TypeText(string text)
        {
            isTyping = true;
            textDisplay.text = "";
            currentText = text;

            foreach (char letter in text.ToCharArray())
            {
                textDisplay.text += letter;

                yield return new WaitForSeconds(typingSpeed);
            }

            yield return new WaitForSeconds(delayBetweenMessages);
            isTyping = false;
        }

        public void SkipTyping()
        {
            if (isTyping)
            {
                StopAllCoroutines();
                textDisplay.text = currentText;
                isTyping = false;
            }
        }

        public bool IsTyping()
        {
            return isTyping;
        }
    }
}