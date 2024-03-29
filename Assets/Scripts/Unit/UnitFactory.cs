﻿using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using My.Base.Skills;
using My.Base.Units;
using UnityEngine;

namespace My.Base.Units
{
    public static class UnitFactory
    {

        public static Unit GetNewUnit(UnitTypes presetName, int level, Team team)
        {
            switch (presetName)
            {
                case UnitTypes.TestWarrior:
                    return GetNewTestWarrior(level, team);
                case UnitTypes.TestMage:
                    return GetNewTestMage(level, team);
                case UnitTypes.TestTank:
                    return GetNewTestTank(level, team);
                default:
                    throw new InvalidEnumArgumentException(presetName.ToString());
            }
        }

        #region Units Creation Methods
        public static Unit GetNewTestWarrior(int level, Team team)
        {
            UnitAttributes attributes = new UnitAttributes()
            {
                Health = 10 + level*2,
                Mana = 5 + level/2,
                ManaRegeneration = 0+level/3,
                Stamina = 20 + level/2,
                StaminaRegeneration = 2 + level/4,
                PhysicalDefence = 5 + level/3,
                MagicalDefence = 2 + level/4,
                PhysicalPower = 5 + level,
                Speed = 4 + level*0.3f,
                Dodge = 5 + level,
                Accuracy = 25 + level,
                CriticalHitChance = 0.1f
            };
            string name = "Bandit Swordsman";
            UnitTypes type = UnitTypes.TestWarrior;
            Unit unit = new Unit(name, level, attributes, type, team);
            unit.Skills.Add(new PhysicalAttackSkill());
            return unit;
        }
        
        public static Unit GetNewTestMage(int level, Team team)
        {
            UnitAttributes attributes = new UnitAttributes()
            {
                Health = 10 + level*2/3,
                Mana = 20 + level*3/2,
                ManaRegeneration = 2+level/3,
                Stamina = 10 + level/10,
                StaminaRegeneration = 1 + level/10,
                PhysicalDefence = 2 + level/6,
                MagicalDefence = 5 + level/5,
                PhysicalPower = 1 + level/5,
                MagicalPower = 8 + level*3/2,
                Speed = 3 + level*0.2f,
                Dodge = 2 + level/2,
                Accuracy = 10 + level*2,
                CriticalHitChance = 0.15f,
                CriticalHitMultiplier = 2,
            };
            string name = "Bandit Mage";
            UnitTypes type = UnitTypes.TestMage;
            Unit unit = new Unit(name, level, attributes, type, team);
            unit.Skills.Add(new PhysicalAttackSkill());
            unit.Skills.Add(new HealSkill());
            return unit;
        }
        
        public static Unit GetNewTestTank(int level, Team team)
        {
            UnitAttributes attributes = new UnitAttributes()
            {
                Health = 15 + level*2,
                HealthRegeneration = 0.1f*(1+level/10),
                Mana = 5 + level/2,
                ManaRegeneration = 0+level/3,
                Stamina = 24 + level/4,
                StaminaRegeneration = 2 + level/8,
                PhysicalDefence = 8 + level/2,
                MagicalDefence = 6 + level/3,
                PhysicalPower = 1 + level/5,
                Speed = 2 + level*0.1f,
                Dodge = 0 + level/2,
                Accuracy = 10 + level*2,
                CriticalHitChance = 0.15f,
                CriticalHitMultiplier = 2,
            };
            string name = "Bandit Bastion";
            UnitTypes type = UnitTypes.TestTank;
            Unit unit = new Unit(name, level, attributes, type, team);
            unit.Skills.Add(new PhysicalAttackSkill());
            return unit;
        }
        #endregion
    }
}