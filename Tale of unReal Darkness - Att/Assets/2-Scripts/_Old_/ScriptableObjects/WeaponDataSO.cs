using System;
using System.Collections.Generic;
using System.Linq;
using Bardent.Weapons.Components;
using UnityEngine;
using static Bardent.Weapons.WeaponDataSO;

namespace Bardent.Weapons
{
    [CreateAssetMenu(fileName = "newWeaponData", menuName = "Data/Weapon Data/Basic Weapon Data", order = 0)]
    public class WeaponDataSO : ScriptableObject, IItemAction
    {
        [field: SerializeField] public Sprite Icon { get; set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public RuntimeAnimatorController AnimatorController { get; private set; }
        [field: SerializeField] public int NumberOfAttacks { get; private set; }

        [field: SerializeField] public bool IsStackable { get; set; }
        public int ID => GetInstanceID();
        [field: SerializeField] public int MaxStackSize { get; set; } = 1;

        [field: SerializeReference] public List<ComponentData> ComponentData { get; private set; }

        public T GetData<T>()
        {
            return ComponentData.OfType<T>().FirstOrDefault();
        }

        public List<Type> GetAllDependencies()
        {
            return ComponentData.Select(component => component.ComponentDependency).ToList();
        }

        public void AddData(ComponentData data)
        {
            if(ComponentData.FirstOrDefault(t => t.GetType() == data.GetType()) != null) 
                return;
            
            ComponentData.Add(data);
        }

        public string ActionName => "Equip";

        public interface IItemAction
        {
            public string ActionName { get; }
        }

        public static implicit operator WeaponDataSO(ItemSO v)
        {
            throw new NotImplementedException();
        }
    }
}