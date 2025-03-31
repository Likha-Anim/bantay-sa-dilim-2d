using System;
using Enum;
using UnityEngine;

namespace Data
{
    public interface IItem
    {
        string ItemName { get; set; }
        Color ItemColor { get; set; }
        Sprite ItemSprite { get; set; }
        StatusEffect StatusEffect { get; set; }
    }

    public abstract class Item : ScriptableObject, IItem
    {
        [SerializeField] private string itemName;
        [SerializeField] private Color itemColor;
        [SerializeField] private Sprite itemSprite;
        [SerializeField] private StatusEffect statusEffect;

        public string ItemName
        {
            get => itemName;
            set => itemName = value;
        }

        public Color ItemColor
        {
            get => itemColor;
            set => itemColor = value;
        }

        public Sprite ItemSprite
        {
            get => itemSprite;
            set => itemSprite = value;
        }

        public StatusEffect StatusEffect
        {
            get => statusEffect;
            set => statusEffect = value;
        }
    }
}