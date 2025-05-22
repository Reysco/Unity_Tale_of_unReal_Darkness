using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Bardent.Weapons.QuestSO;

namespace Bardent.Weapons
{
    [CreateAssetMenu]
    public class QuestSO : ScriptableObject
    {
        [field: SerializeField] public bool IsStackable { get; set; }
        public int ID => GetInstanceID();
        [field: SerializeField] public int MaxStackSize { get; set; } = 1;

        [field: SerializeField] public Sprite Icon { get; set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string Description { get; private set; }

    }
}