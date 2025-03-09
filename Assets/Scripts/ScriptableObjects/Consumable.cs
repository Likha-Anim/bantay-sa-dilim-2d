using System;
using System.Collections.Generic;
using Enum;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "NewConsumable", menuName = "Data/New Consumable")]
    public class Consumable : Item
    {
        public float healthGiven;
    }
}