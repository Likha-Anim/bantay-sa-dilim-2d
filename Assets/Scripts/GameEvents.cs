using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using Manager;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Scene = UnityEngine.SceneManagement.Scene;

public class GameEvents : Singleton<GameEvents>
{
    public float delay = 1;
    private Queue<(string, object[])> _eventQueue = new Queue<(string, object[])>();
    private bool _isProcessing;
    private bool _isPaused;

    // This shows the thumbnail and starts the game when the button is clicked
    private void Start()
    {
        Utilities.FindChild("Thumbnail").gameObject.SetActive(true);
        var button = Utilities.FindChild("Thumbnail/Button").GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            Utilities.FindChild("Thumbnail").gameObject.SetActive(false);
            StartScene("Scene1");
        });
    }

    // This is the main game loop
    private void Update()
    {
        if (_eventQueue.Count == 0) return;
        // Check if the game is paused or processing an event, if not, start processing the other events
        if (!_isPaused && !_isProcessing) StartCoroutine(ProcessEventQueue());
    }

    public void StartScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Loads the scene and intializes the UI
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        UIManager.Instance.InitializeUI();
        StoryManager.Instance.StartStory(scene.name);
    }

    // Pauses the game
    public void ShouldPause(bool shouldPause)
    {
        Debug.Log("Pausing Game");
        _isPaused = shouldPause;
    }

    // Queues the game event
    public void TriggerGameEvent(string eventName, params object[] args)
    {
        Debug.Log($"Queued Game Event: {eventName} with args: {(args != null ? string.Join(", ", args) : "None")}");
        _eventQueue.Enqueue((eventName, args));
    }

    // Processes the game event
    private IEnumerator ProcessEventQueue()
    {
        _isProcessing = true;
        var (eventName, args) = _eventQueue.Dequeue();
        yield return StartCoroutine(InvokeEvent(eventName, args));
        _isProcessing = false;
    }

    // Invokes the associated event based on the event queue
    private IEnumerator InvokeEvent(string eventName, object[] args)
    {
        Debug.Log($"Processing Event: {eventName}");

        switch (eventName)
        {
            case "ChangeScene":
                StartScene((string)args[0]);
                break;
            case "NextDialogue":
                StoryManager.Instance.NextDialogue();
                break;
            case "AddDelay":
                yield return new WaitForSeconds(float.Parse((string)args[0]));
                break;
            case "ChangeSpeaker":
                UIManager.Instance.ChangeSpeaker((string)args[0]);
                break;
            case "ShowChoices":
                UIManager.Instance.ShowChoices(args[0] as Story);
                break;
            case "PlaySound":
                SoundManager.Instance.PlaySound((string)args[0]);
                break;
            case "PlaySoundLoop":
                SoundManager.Instance.PlaySoundLoop((string)args[0]);
                break;
            case "TypeText":
                string text = args[0] as string;
                yield return StartCoroutine(TypingManager.Instance.TypeText(text));
                if (text != "Ano ang dapat gawin?") UIManager.Instance.ShowDialogueButton();
                break;
            case "ChangeSection":
                yield return StartCoroutine(SectionManager.Instance.SwitchSection((string)args[0]));
                yield return new WaitForSeconds(delay / 2);
                break;
            case "ChangeUI":
                string type = (string)args[0];
                UIManager.Instance.ChangeUI(type);
                if (type != "" && !type.Contains("Screen")) TypingManager.Instance.InitializeTextDisplay(type);
                if (type.Contains("Combat")) ShouldPause(true);
                break;
            default:
                Debug.LogWarning($"Unknown event: {eventName}");
                break;
        }
    }
}