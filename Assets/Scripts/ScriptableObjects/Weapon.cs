using System;
using System.Collections.Generic;
using Enum;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "NewWeapon", menuName = "Data/New Weapon")]
    public class Weapon : Item
    {
        public float damage;
        public float dropRate;
    }
}