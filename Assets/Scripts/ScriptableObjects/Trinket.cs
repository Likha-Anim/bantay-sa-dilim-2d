using System;
using System.Collections.Generic;
using Enum;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "NewTrinket", menuName = "Data/New Trinket")]
    public class Trinket : Item
    {
        public float healthBoost;
        public float damageBoost;
    }
}