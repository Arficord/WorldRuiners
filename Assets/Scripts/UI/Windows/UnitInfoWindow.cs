using My.Base.Units;
using My.UI.Utils;
using TMPro;
using UnityEngine;

namespace My.UI.Windows
{
    public class UnitInfoWindow : MonoBehaviour, IShowHide
    {
        [SerializeField] private ProgressBar healthBar;
        [SerializeField] private ProgressBar staminaBar;
        [SerializeField] private ProgressBar manaBar;
        [SerializeField] private TextMeshProUGUI unitNameTextField;
        [SerializeField] private TextMeshProUGUI healthRegenerationTextField;
        [SerializeField] private TextMeshProUGUI staminaRegenerationTextField;
        [SerializeField] private TextMeshProUGUI manaRegenerationTextField;
        [SerializeField] private TextMeshProUGUI physicalPowerTextField;
        [SerializeField] private TextMeshProUGUI physicalDefenceTextField;
        [SerializeField] private TextMeshProUGUI magicalPowerTextField;
        [SerializeField] private TextMeshProUGUI magicalDefenceTextField;
        [SerializeField] private TextMeshProUGUI dodgeTextField;
        [SerializeField] private TextMeshProUGUI accuracyTextField;
        [SerializeField] private TextMeshProUGUI criticalChanceTextField;
        [SerializeField] private TextMeshProUGUI criticalMultiplierTextField;
        [SerializeField] private TextMeshProUGUI speedTextField;
        [SerializeField] private TextMeshProUGUI levelTextField;

        public void UpdateView(Unit unitToShow)
        {
            UnitAttributes curAttributes = unitToShow.CurrentAttributes;
            UnitAttributes baseAttributes = unitToShow.BaseAttributes;
            
            healthBar.MaxValue = unitToShow.BaseAttributes.Health;
            healthBar.CurrentValue = unitToShow.CurrentAttributes.Health;
            manaBar.MaxValue = unitToShow.BaseAttributes.Mana;
            manaBar.CurrentValue = unitToShow.CurrentAttributes.Mana;
            staminaBar.MaxValue = unitToShow.BaseAttributes.Stamina;
            staminaBar.CurrentValue = unitToShow.CurrentAttributes.Stamina;

            unitNameTextField.text = unitToShow.Name;
            healthRegenerationTextField.text = curAttributes.HealthRegeneration.ToString("+0.0");
            staminaRegenerationTextField.text = curAttributes.StaminaRegeneration.ToString("+0.0");
            manaRegenerationTextField.text = curAttributes.ManaRegeneration.ToString("+0.0");
            physicalPowerTextField.text = curAttributes.PhysicalPower.ToString("0.0");
            physicalDefenceTextField.text = curAttributes.PhysicalDefence.ToString("0.0");
            magicalPowerTextField.text = curAttributes.MagicalPower.ToString("0.0");
            magicalDefenceTextField.text = curAttributes.MagicalDefence.ToString("0.0");
            dodgeTextField.text = curAttributes.Dodge.ToString("0.0");
            accuracyTextField.text = curAttributes.Accuracy.ToString("0.0");
            criticalChanceTextField.text = (curAttributes.CriticalHitChance).ToString("0.0%");
            criticalMultiplierTextField.text = curAttributes.CriticalHitMultiplier.ToString("0.0x");
            speedTextField.text = curAttributes.Speed.ToString("0.0");
            levelTextField.text = unitToShow.Level.ToString();
        }

        public void Hide()
        {
            //TODO: Closing animation
            gameObject.SetActive(false);
        }

        public void Show()
        {
            //TODO: Opening animation
            gameObject.SetActive(true);
        }
    }
}