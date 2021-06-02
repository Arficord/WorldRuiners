using System;
using System.Collections;
using System.Collections.Generic;
using My.Base.Units;
using My.UI.Windows;
using UnityEngine;

namespace My.Base.Battle
{
    //TODO: WIP. Currently this is a test code
    public class BattleManager : MonoBehaviour
    {
        public UnitInfoWindow unitInfoWindow;
        
        public BattleUnit battleUnit1;
        public BattleUnit battleUnit2;
        public BattleUnit battleUnit3;
        private void Start()
        {
            battleUnit1.UnitModel = UnitFactory.GetNewTestWarrior(82);
            battleUnit2.UnitModel = UnitFactory.GetNewTestMage(12);
            battleUnit3.UnitModel = UnitFactory.GetNewTestTank(3);
        }
    }
}