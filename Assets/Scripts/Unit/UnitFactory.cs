﻿using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using My.Base.Unit;
using UnityEngine;

namespace My.Base.Unit
{
    public static class UnitFactory
    {

        public static Unit GetNewUnit(UnitTypes presetName, int level)
        {
            switch (presetName)
            {
                case UnitTypes.TestWarrior:
                    return GetNewTestWarrior(level);
                case UnitTypes.TestMage:
                    return GetNewTestMage(level);
                case UnitTypes.TestTank:
                    return GetNewTestTank(level);
                default:
                    throw new InvalidEnumArgumentException(presetName.ToString());
            }
        }

        #region Units Creation Methods
        public static Unit GetNewTestWarrior(int level)
        {
            UnitAttributes attributes = new UnitAttributes()
            {
                Health = 10 + level*2,
                Stamina = 20 + level/2,
                StaminaRegeneration = 2 + level/4,
                PhysicalDefence = 5 + level/3,
                MagicalDefence = 2 + level/4,
                PhysicalPower = 5 + level,
                Speed = 4 + level/4,
                Dodge = 5 + level,
                Accuracy = 25 + level,
                CriticalHit = 0.1f
            };
            //TODO: add translation support
            return new Unit(attributes, "Bandit Swordsman", level);
        }
        
        public static Unit GetNewTestMage(int level)
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
                Speed = 3 + level/5,
                Dodge = 2 + level/2,
                Accuracy = 10 + level*2,
                CriticalHit = 0.15f,
                CriticalHitMultiplier = 2,
            };
            //TODO: add translation support
            return new Unit(attributes, "Bandit Mage", level);
        }
        
        public static Unit GetNewTestTank(int level)
        {
            UnitAttributes attributes = new UnitAttributes()
            {
                Health = 15 + level*2,
                Stamina = 24 + level/4,
                StaminaRegeneration = 2 + level/8,
                PhysicalDefence = 8 + level/2,
                MagicalDefence = 6 + level/3,
                PhysicalPower = 1 + level/5,
                Speed = 2 + level/6,
                Dodge = 0 + level/2,
                Accuracy = 10 + level*2,
                CriticalHit = 0.15f,
                CriticalHitMultiplier = 2,
            };
            //TODO: add translation support
            return new Unit(attributes, "Bandit Bastion", level);
        }
        #endregion
    }
}