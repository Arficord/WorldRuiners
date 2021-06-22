using System.Collections;
using System.Collections.Generic;
using My.Base.Units;
using UnityEngine;

namespace My.Base.Skills
{
    public class MagicAttackSkill : Skill
    {
        public new int ManualTargetAmount { get; protected set; } = 3;

        public override bool IsPossibleTarget(Unit caster, Unit target)
        {
            return caster.MindedTeam != target.RealTeam;
        }

        public override string GetNameKey()
        {
            //TODO: replace with translation key
            return "MAGIC_SKILL_NAME";
        }

        public override string GetDescriptionKey()
        {
            //TODO: replace with translation key
            return "MAGIC_SKILL_DESCRIPTION";
        }

        protected override void ApplySkillEffect(Unit caster, List<Unit> targets)
        {
            Debug.Log($"Skill {nameof(MagicAttackSkill)}  used by {caster.Name}");
            float healing = caster.CurrentAttributes.MagicalPower;
            targets[0].ReceiveHealing(healing);
        }
    }
}