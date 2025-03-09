using System.Collections.Generic;
using Ink.Parsed;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class DialogueManager : MonoBehaviour
    {
        public static DialogueManager Instance { get; private set; }

        [SerializeField] private List<TextAsset> _inkJsons;
        private List<Story> _stories;

        [SerializeField] private GameObject dialogueBox;
        [SerializeField] private GameObject choiceBox;
        [SerializeField] private GameObject combatBox;

        public float typingSpeed = 0.02f;
        private bool _isTyping;

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

        private void Start()
        {
        }

        public void DisplayNext()
        {
        }
    }
}