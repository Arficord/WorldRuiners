﻿using System;
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
        public Team RealTeam { get; set; }
        public Team MindedTeam { get; set; }
        public UnitAttributes BaseAttributes { get; }
        public UnitAttributes CurrentAttributes { get; }
        public event Action OnDie;
        
        public List<Skill> Skills { get; } = new List<Skill>();

        public Unit(string unitName, int level, UnitAttributes attributes,  UnitTypes unitType, Team team)
        {
            Name = unitName;
            Level = level;
            BaseAttributes = attributes;
            CurrentAttributes = new UnitAttributes(BaseAttributes);
            UnitType = unitType;
            RealTeam = team;
            MindedTeam = team;
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
            Debug.Log($"{Name} received {healAmount} health!");
            if (healAmount + CurrentAttributes.Health > BaseAttributes.Health)
            {
                CurrentAttributes.Health = BaseAttributes.Health;
            }

            CurrentAttributes.Health += healAmount;
        }

        public void Die()
        {
            OnDie?.Invoke();
            Debug.Log($"Killed {Name}");
        }
    }
}