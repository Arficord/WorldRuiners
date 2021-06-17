using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My.Base.Units
{
    public class UnitAttributes
    {
        public float Health
        {
            get=>health;
            set
            {
                health = value;
                OnHealthChanged?.Invoke(health);
            }
        }
        public float HealthRegeneration { get; set; }
        public float Mana 
        { 
            get=>mana;
            set
            {
                mana = value;
                OnManaChanged?.Invoke(mana);
            } 
        }
        public float ManaRegeneration { get; set; }
        public float Stamina
        {
            get=>stamina;
            set
            {
                stamina = value;
                OnStaminaChanged?.Invoke(stamina);
            }
        }
        public float StaminaRegeneration { get; set; }
        public float PhysicalDefence { get; set; }
        public float MagicalDefence { get; set; }
        public float PhysicalPower { get; set; }
        public float MagicalPower { get; set; }
        public float Speed { get; set; }
        public float Dodge { get; set; }
        public float Accuracy { get; set; }
        public float CriticalHitChance { get; set; }
        public float CriticalHitMultiplier { get; set; } = 1.5f;
        public event Action<float> OnHealthChanged;
        public event Action<float> OnManaChanged;
        public event Action<float> OnStaminaChanged;

        private float health;
        private float mana;
        private float stamina;
        
        public UnitAttributes(){}
        
        public UnitAttributes(UnitAttributes copiedOriginal)
        {
            Health = copiedOriginal.Health;
            HealthRegeneration = copiedOriginal.HealthRegeneration;
            Mana = copiedOriginal.Mana;
            ManaRegeneration = copiedOriginal.ManaRegeneration;
            Stamina = copiedOriginal.Stamina;
            StaminaRegeneration = copiedOriginal.StaminaRegeneration;
            PhysicalDefence = copiedOriginal.PhysicalDefence;
            MagicalDefence = copiedOriginal.MagicalDefence;
            PhysicalPower = copiedOriginal.PhysicalPower;
            MagicalPower = copiedOriginal.MagicalPower;
            Speed = copiedOriginal.Speed;
            Dodge = copiedOriginal.Dodge;
            Accuracy = copiedOriginal.Accuracy;
            CriticalHitChance = copiedOriginal.CriticalHitChance;
            CriticalHitMultiplier = copiedOriginal.CriticalHitMultiplier;
        }
    }
}