using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Managers
{
    // A struct that represents a character in the story with color for dialogue
    [Serializable]
    public struct Character
    {
        public string name;
        public Color color;
    }

    public class UIManager : Singleton<UIManager>
    {
        public List<Character> characters;
        public Button choiceButtonPrefab;

        private GameObject _previousUI;

        // Initializes the UI for the current scene
        public void InitializeUI()
        {
            InitializeDialogueButton();
            // InitializeScreen();
        }

        // Changes the UI to the new UI
        public void ChangeUI(string newUI)
        {
            if (!newUI.Contains("Screen")) ClearText();
            if (_previousUI) _previousUI.SetActive(false);
            if (newUI == "") return;
            _previousUI = Utilities.FindChild(newUI).gameObject;
            _previousUI.SetActive(true);
        }

        // Changes the speaker for the dialogue
        public void ChangeSpeaker(string characterName)
        {
            Character character = characters.Find(c => c.name == characterName);
            TextMeshProUGUI speakerDisplay =
                Utilities.FindChild("Overlay/Dialogue/Speaker").GetComponent<TextMeshProUGUI>();
            speakerDisplay.color = character.color;
            speakerDisplay.text = character.name;
        }

        // Shows the dialogue button, used after the typing animation
        public void ShowDialogueButton()
        {
            var dialogueButton = Utilities.FindChild("Overlay/Dialogue/Button").GetComponent<Button>();
            dialogueButton.interactable = true;
            dialogueButton.gameObject.SetActive(true);
        }

        // Shows the choices for the current section
        public void ShowChoices(Story story)
        {
            var choiceGroup = Utilities.FindChild("Overlay/Choices/ChoiceGroup")
                .GetComponent<VerticalLayoutGroup>();
            if (choiceGroup.GetComponentsInChildren<Button>().Length > 0) return;

            for (int i = 0; i < story.currentChoices.Count; i++)
            {
                var choice = story.currentChoices[i];
                Button button = Instantiate(choiceButtonPrefab, choiceGroup.transform);
                button.GetComponentInChildren<TextMeshProUGUI>().text = choice.text;
                button.onClick.AddListener(() =>
                {
                    story.ChooseChoiceIndex(choice.index);
                    story.Continue();
                    RemoveChoices();
                    choiceGroup.padding = new RectOffset(0, 0, 0, 0);
                    GameEvents.Instance.TriggerGameEvent("NextDialogue", null);
                    GameEvents.Instance.TriggerGameEvent("ChangeUI", "");
                });
            }

            choiceGroup.padding = new RectOffset(0, 0, 10, 10);
        }

        // Removes the choices from the UI
        private void RemoveChoices()
        {
            var choiceGroup = Utilities.FindChild("Overlay/Choices/ChoiceGroup");
            foreach (Transform child in choiceGroup) Destroy(child.gameObject);
        }

        // Clears the text for the dialogue and choices
        private void ClearText()
        {
            Utilities.FindChild("Overlay/Dialogue/Speaker").GetComponent<TextMeshProUGUI>().text = "";
            Utilities.FindChild("Overlay/Dialogue/Sentence").GetComponent<TextMeshProUGUI>().text = "";
            Utilities.FindChild("Overlay/Choices/Sentence").GetComponent<TextMeshProUGUI>().text = "";
        }

        // Initializes the dialogue button for the current scene
        private void InitializeDialogueButton()
        {
            var dialogueButton = Utilities.FindChild("Overlay/Dialogue/Button").GetComponent<Button>();
            dialogueButton.onClick.AddListener(() =>
            {
                dialogueButton.interactable = false;
                EventSystem.current.SetSelectedGameObject(null);
                GameEvents.Instance.TriggerGameEvent("NextDialogue", null);
                dialogueButton.gameObject.SetActive(false);
                ClearText();
            });
        }

        // Initializes the screens button for the current scene
        private void InitializeScreen()
        {
            var startButton = Utilities.FindChild("Overlay/Screen/Start/Choice").GetComponent<Button>();
            startButton.onClick.AddListener(() =>
            {
                ClearPreviousUI();
                GameEvents.Instance.ShouldPause(false);
                EventSystem.current.SetSelectedGameObject(null);
            });

            var deathButton = Utilities.FindChild("Overlay/Screen/Death/Choice").GetComponent<Button>();
            deathButton.onClick.AddListener(() =>
            {
                ClearPreviousUI();
                GameEvents.Instance.ShouldPause(false);
                EventSystem.current.SetSelectedGameObject(null);
            });
        }

        // Clears the previous UI
        private void ClearPreviousUI()
        {
            if (_previousUI) _previousUI.SetActive(false);
        }
    }
}