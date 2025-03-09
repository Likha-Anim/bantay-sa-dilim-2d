using System;
using System.Collections.Generic;
using Data;
using Enum;
using Managers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Manager
{
    [Serializable]
    public class Scene
    {
        public string sceneName;
        public SectionType currentSectionType = SectionType.Main;
        public List<Section> sections;
        public List<Character> characters;
        [HideInInspector] public List<Character> charactersCopy;
    }

    [Serializable]
    public class Section
    {
        public SectionType sectionType;
        public GameObject sceneObject;
        public Canvas Canvas => sceneObject.GetComponent<Canvas>();
    }

    public class SceneManager : MonoBehaviour
    {
        public static SceneManager Instance { get; private set; }

        [SerializeField] private List<Scene> scenes;
        private Scene _currentScene;

        public Character GetCharacter(string name)
        {
            return _currentScene.characters.Find(x =>
                (name != "Boss") ? x.name == name : x.name != "Ibarra" && x.name != "Maria");
        }

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
            SwitchScene("FirstScene");
            CreateCopy();
            CombatManager.Instance.StartCombat();
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.W)) SwitchSection(SectionType.Forward);
            else if (Input.GetKey(KeyCode.A)) SwitchSection(SectionType.Left);
            else if (Input.GetKey(KeyCode.D)) SwitchSection(SectionType.Right);
            else if (Input.GetKey(KeyCode.S)) SwitchSection(SectionType.Main);
            else if (Input.GetKey(KeyCode.F)) SwitchSection(SectionType.Secret);
        }

        private void OnDestroy()
        {
            Debug.Log("Recreating characters data");
            for (int i = 0; i < _currentScene.characters.Count; i++)
            {
                _currentScene.characters[i].SetRuntimeCopy(_currentScene.charactersCopy[i]);
            }
        }

        private void CreateCopy()
        {
            Debug.Log("Creating copy for characters data");
            foreach (Character character in _currentScene.characters)
            {
                _currentScene.charactersCopy.Add(character.GetRuntimeCopy());
            }
        }

        private void SwitchScene(string sceneName)
        {
            Debug.Log("Switching to " + sceneName);
            _currentScene = scenes.Find(x => x.sceneName == sceneName);
            SwitchSection(_currentScene.currentSectionType);
        }

        private void SwitchSection(SectionType newSection)
        {
            HideNonCurrentSection(newSection);
            HideCanvas(_currentScene.currentSectionType);
            ShowCanvas(newSection);
            _currentScene.currentSectionType = newSection;
        }

        private void HideNonCurrentSection(SectionType newSection)
        {
            foreach (var section in _currentScene.sections)
            {
                section.sceneObject.SetActive(section.sectionType == newSection ? true : false);
            }
        }

        private void HideCanvas(SectionType prevSection)
        {
            var canvas = _currentScene.sections.Find(x => x.sectionType == prevSection).Canvas;
            canvas.sortingOrder = 0;
        }

        private void ShowCanvas(SectionType newSection)
        {
            var canvas = _currentScene.sections.Find(x => x.sectionType == newSection).Canvas;
            canvas.sortingOrder = 1;
        }
    }
}