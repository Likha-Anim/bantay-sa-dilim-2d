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

    [Serializable]
    public abstract class Item : ScriptableObject, IItem
    {
        public string ItemName { get; set; }
        public Color ItemColor { get; set; }
        public Sprite ItemSprite { get; set; }
        public StatusEffect StatusEffect { get; set; }
    }
}