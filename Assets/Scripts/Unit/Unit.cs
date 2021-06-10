using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My.Base.Units
{
    public class Unit
    {
        public string Name { get; }
        public int Level { get; }
        public UnitTypes UnitType { get; }
        public UnitAttributes BaseAttributes { get; }
        public UnitAttributes CurrentAttributes { get; }

        public Unit(UnitAttributes attributes, string unitName, UnitTypes unitType, int level)
        {
            BaseAttributes = attributes;
            CurrentAttributes = new UnitAttributes(BaseAttributes);
            Name = unitName;
            UnitType = unitType;
            Level = level;
        }
        
        public void ReceiveDamage(Damage damage)
        {
            Debug.Log($"{Name} received {damage.Value} {damage.Type} damage!");
            CurrentAttributes.Health -= damage.Value;
            if (CurrentAttributes.Health <= 0)
            {
                Die();
            }
        }

        public void ReceiveHealing(float healAmount)
        {
            if (healAmount + CurrentAttributes.Health > BaseAttributes.Health)
            {
                CurrentAttributes.Health = BaseAttributes.Health;
            }

            CurrentAttributes.Health += healAmount;
        }

        public void Attack(Unit target)
        {
            Damage damage = new Damage(CurrentAttributes.PhysicalPower, DamageType.Strike);
            target.ReceiveDamage(damage);
        }
        

        public void Die()
        {
            Debug.Log($"Killed {Name}");
        }
    }
}