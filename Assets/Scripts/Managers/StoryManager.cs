using System;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;

namespace Managers
{
    [Serializable]
    public struct Scene
    {
        public string sceneName;
        public TextAsset inkJson;
    }

    public class StoryManager : Singleton<StoryManager>
    {
        public List<Scene> scenes;
        private Story _story;

        // Starts the story with the given scene name
        public void StartStory(string sceneName)
        {
            TextAsset inkJson = scenes.Find(s => s.sceneName == sceneName).inkJson;
            _story = new Story(inkJson.text);
            GameEvents.Instance.TriggerGameEvent("NextDialogue", null);
        }

        // Continues the story in the current scene
        public void NextDialogue()
        {
            // If there is text to display
            if (_story.canContinue)
            {
                string text = _story.Continue();
                text = text?.Trim();

                if (_story.currentTags.Count > 0) CheckForTags();
                if (!string.IsNullOrEmpty(text)) GameEvents.Instance.TriggerGameEvent("TypeText", text);
            }
            // If there are no more text, check for choices
            else if (_story.currentChoices.Count > 0)
            {
                GameEvents.Instance.TriggerGameEvent("ChangeUI", "Choices");
                GameEvents.Instance.TriggerGameEvent("TypeText", "Ano ang dapat gawin?");
                GameEvents.Instance.TriggerGameEvent("ShowChoices", _story);
            }
        }

        // Checks for tags (which are events) in the current section
        private void CheckForTags()
        {
            foreach (string currentTag in _story.currentTags)
            {
                string[] tags = currentTag.Split(':');
                Debug.Log($"Current tag: {tags[0]} {tags[1]}");
                GameEvents.Instance.TriggerGameEvent(tags[0], tags[1]);
            }
        }
    }
}