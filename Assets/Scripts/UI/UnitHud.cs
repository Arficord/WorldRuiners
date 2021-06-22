using System;
using System.Collections;
using System.Collections.Generic;
using My.Base.Battle;
using My.Base.Units;
using My.UI.Utils;
using UnityEngine;

namespace My.UI.InfoBlocks
{
    public class UnitHud : MonoBehaviour
    {
        [SerializeField] private ProgressBar healthBar;
        [SerializeField] private ProgressBar manaBar;
        [SerializeField] private ProgressBar staminaBar;
        private Unit unitModel;

        public void Initialize(BattleUnit unit)
        {
            unitModel = unit.UnitModel;
            UpdateHP();
            UpdateMP();
            UpdateSP();
            unitModel.OnDie += PlayDestroyAnimation;
        }

        private void UpdateHP()
        {
            healthBar.MaxValue = unitModel.BaseAttributes.Health;
            healthBar.CurrentValue = unitModel.CurrentAttributes.Health;
        }

        private void UpdateMP()
        {
            manaBar.MaxValue = unitModel.BaseAttributes.Mana;
            manaBar.CurrentValue = unitModel.CurrentAttributes.Mana;
        }

        private void UpdateSP()
        {
            staminaBar.MaxValue = unitModel.BaseAttributes.Stamina;
            staminaBar.CurrentValue = unitModel.CurrentAttributes.Stamina;
        }

        //TODO: Make events
        private void Update()
        {
            if (unitModel == null)
            {
                return;
            }

            UpdateHP();
            UpdateMP();
            UpdateSP();
        }

        private void PlayDestroyAnimation()
        {
            Destroy(gameObject);
        }
    }
}