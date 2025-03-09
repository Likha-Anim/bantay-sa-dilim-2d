using System.Collections.Generic;
using Data;
using Enum;
using Manager;
using TMPro;
using UnityEngine;
using Button = UnityEngine.UI.Button;

namespace Managers
{
    public class CombatManager : MonoBehaviour
    {
        public static CombatManager Instance { get; private set; }
        public GameObject combatBox;
        [SerializeField] private TextMeshProUGUI status;
        private string _prevHUD = "HUD/Stats";

        private Character _ibarra;
        private Character _maria;
        private Character _boss;
        private Queue<CombatActions> _actionsQueue = new Queue<CombatActions>();

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
            if (_actionsQueue.Count > 0 && !TypingManager.Instance.IsTyping())
            {
                CombatActions combatActions = _actionsQueue.Dequeue();
                Debug.Log("Current Action: " + combatActions);
                switch (combatActions)
                {
                    case CombatActions.Won:
                        // show game over screen
                        break;
                    case CombatActions.Lost:
                        // close combat box
                        break;
                    case CombatActions.BossAttack:
                        DisableButtons();
                        BossAttack();
                        break;
                    case CombatActions.IbarraAttack:
                        UpdateStatus($"Ibarra's turn to attack");
                        break;
                    case CombatActions.IbarraPray:
                        break;
                    case CombatActions.IbarraUseItem:
                        break;
                    case CombatActions.UpdateStats:
                        UpdateStats();
                        break;
                    case CombatActions.UpdateStatusMessage:
                        Debug.Log("Text in Queue: " + TypingManager.Instance.QueueCount);
                        break;
                    case CombatActions.EnableButtons:
                        EnableButtons();
                        break;
                }
            }

            if (TypingManager.Instance && TypingManager.Instance.IsTyping() &&
                (Input.anyKeyDown || Input.GetMouseButtonDown(0)))
            {
                TypingManager.Instance.SkipTyping();
            }
        }

        public void StartCombat()
        {
            combatBox.SetActive(true);

            InitializeButtons();
            InitializeCharacters();

            TypingManager.Instance.textDisplay =
                combatBox.transform.Find("Status/Text").GetComponent<TextMeshProUGUI>();

            _actionsQueue.Enqueue(CombatActions.BossAttack);
        }

        public void IbarraAttack()
        {
            DisableButtons();
            ShowUI("HUD/Stats");
            _boss.stats.TotalHealth -= _ibarra.stats.TotalDamage;

            UpdateStatus($"{_ibarra.name}'s dealt {_ibarra.stats.TotalDamage} damage to {_boss.name}");
            UpdateStatus($"{_boss.name}'s health is down to {_boss.stats.TotalHealth}");
            _actionsQueue.Enqueue(CombatActions.UpdateStats);
            HasBossDied();
        }

        private void BossAttack()
        {
            UpdateStatus($"{_boss.name}'s turn to attack");
            _ibarra.stats.TotalHealth -= _boss.stats.TotalDamage;

            UpdateStatus($"{_boss.name}'s dealt {_boss.stats.TotalDamage} damage to {_ibarra.name}");
            UpdateStatus($"{_ibarra.name}'s health is down to {_ibarra.stats.TotalHealth}");
            _actionsQueue.Enqueue(CombatActions.UpdateStats);
            HasIbarraDied();
        }

        public void Pray()
        {
            ShowUI("HUD/Pray");
            UpdateStatus($"Ibarra prayed for help!");
        }

        public void ShowItems()
        {
            ShowUI("HUD/Items");
            UpdateStatus($"Ibarra checked his items");
        }

        private void HasIbarraDied()
        {
            if (_ibarra.stats.TotalHealth <= 0)
            {
                _actionsQueue.Enqueue(CombatActions.Lost);
                UpdateStatus($"You have been defeated by {_boss.name}");
            }
            else
            {
                _actionsQueue.Enqueue(CombatActions.IbarraAttack);
                _actionsQueue.Enqueue(CombatActions.EnableButtons);
            }
        }

        private void HasBossDied()
        {
            if (_boss.stats.TotalHealth <= 0)
            {
                _actionsQueue.Enqueue(CombatActions.Won);
                UpdateStatus($"You have defeated {_boss.name}");
            }
            else
            {
                _actionsQueue.Enqueue(CombatActions.BossAttack);
            }
        }

        private void TryActivatePrayButton()
        {
        }

        private void ShowUI(string hierarchy)
        {
            if (_prevHUD != hierarchy)
                combatBox.transform.Find(_prevHUD).gameObject.SetActive(false);

            combatBox.transform.Find(hierarchy).gameObject.SetActive(true);
            _prevHUD = hierarchy;
        }

        private void EnableButtons()
        {
            Transform parent = combatBox.transform.Find("Buttons");
            foreach (Transform button in parent)
            {
                if (button.name == "Attack" || button.name == "Items")
                    button.GetComponent<Button>().interactable = true;
                else
                    button.GetComponent<Button>().interactable = false;
            }
        }

        private void DisableButtons()
        {
            Transform parent = combatBox.transform.Find("Buttons");
            foreach (Transform button in parent)
            {
                button.GetComponent<Button>().interactable = false;
            }
        }

        private void UpdateStatus(string text)
        {
            _actionsQueue.Enqueue(CombatActions.UpdateStatusMessage);
            TypingManager.Instance.EnqueueText(text);
            Debug.Log(text);
        }

        private void InitializeCharacters()
        {
            _ibarra = SceneManager.Instance.GetCharacter("Ibarra");
            _maria = SceneManager.Instance.GetCharacter("Maria");
            _boss = SceneManager.Instance.GetCharacter("Boss");

            UpdateStats();
        }

        private void UpdateStats()
        {
            combatBox.transform.Find("HUD/Stats/Ibarra/Health").GetComponent<TextMeshProUGUI>().text =
                $"Health: {_ibarra.stats.TotalHealth}";
            combatBox.transform.Find("HUD/Stats/Ibarra/Damage").GetComponent<TextMeshProUGUI>().text =
                $"Damage: {_ibarra.stats.TotalDamage}";
            combatBox.transform.Find("HUD/Stats/Maria/Health").GetComponent<TextMeshProUGUI>().text =
                $"Health: 60";
            combatBox.transform.Find("HUD/Stats/Maria/Damage").GetComponent<TextMeshProUGUI>().text =
                $"Damage: ---";
            combatBox.transform.Find("HUD/Stats/Boss/Name").GetComponent<TextMeshProUGUI>().text =
                $"{_boss.name}";
            combatBox.transform.Find("HUD/Stats/Boss/Health").GetComponent<TextMeshProUGUI>().text =
                $"Health: {_boss.stats.TotalHealth}";
            combatBox.transform.Find("HUD/Stats/Boss/Damage").GetComponent<TextMeshProUGUI>().text =
                $"Damage: {_boss.stats.TotalDamage}";

            Debug.Log("Stats updated");
        }

        private void InitializeButtons()
        {
            combatBox.transform.Find("Buttons/Attack").GetComponent<Button>().onClick.AddListener(IbarraAttack);
            combatBox.transform.Find("Buttons/Pray").GetComponent<Button>().onClick.AddListener(Pray);
            combatBox.transform.Find("Buttons/Items").GetComponent<Button>().onClick.AddListener(ShowItems);
        }
    }
}