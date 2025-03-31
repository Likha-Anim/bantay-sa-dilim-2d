using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class Stats
    {
        public List<Item> items;
        public float baseHealth;
        public float baseDamage;
        public float criticalChance;
        public float criticalDamage;
        private float _totalHealth;
        private float _totalDamage;
        private bool _hasCalculated;

        public float TotalHealth
        {
            get
            {
                if (!_hasCalculated) CalculateTotalHealthAndDamage();
                return _totalHealth;
            }
            set { _totalHealth = value; }
        }

        public float TotalDamage
        {
            get
            {
                if (!_hasCalculated) CalculateTotalHealthAndDamage();
                return _totalDamage;
            }
            set { _totalDamage = value; }
        }

        public void CalculateTotalHealthAndDamage()
        {
            _totalHealth = baseHealth;
            _totalDamage = baseDamage;
            foreach (Item item in items)
            {
                if (item is Weapon weapon)
                {
                    _totalDamage += weapon.damage;
                }
                else if (item is Trinket trinket)
                {
                    _totalHealth += trinket.healthBoost;
                    _totalDamage += trinket.damageBoost;
                }
            }

            _hasCalculated = true;
        }
    }

    [CreateAssetMenu(fileName = "NewCharacter", menuName = "Data/New Character")]
    public class Character : ScriptableObject
    {
        public string characterName;
        public Color dialogueColor;
        public Sprite dialogueSprite;
        public Sprite combatSprite;
        public List<Sprite> emoteSprite;
        public Stats stats;

        public Character GetRuntimeCopy()
        {
            Character character = Instantiate(this);
            return character;
        }

        public void SetRuntimeCopy(Character character)
        {
            dialogueSprite = character.dialogueSprite;
            emoteSprite = character.emoteSprite;
            combatSprite = character.combatSprite;
            stats = character.stats;
        }
    }
}