using System.Collections;
using System.Collections.Generic;
using My.Base.Units;
using UnityEngine;

namespace My.Base.Skills
{
    public class PhysicalAttackSkill : Skill
    {
        public override bool IsPossibleTarget(Unit caster, Unit target)
        {
            return caster.MindedTeam != target.RealTeam;
        }

        public override string GetNameKey()
        {
            //TODO: replace with translation key
            return "Punch";
        }

        public override string GetDescriptionKey()
        {
            //TODO: replace with translation key
            return "PUNCH_SKILL_DESCRIPTION";
        }

        protected override void ApplySkillEffect(Unit caster, List<Unit> targets)
        {
            Debug.Log($"Skill {nameof(PhysicalAttackSkill)}  used by {caster.Name}");
            Damage damage = new Damage(caster.CurrentAttributes.PhysicalPower, DamageType.Strike);
            targets[0].ReceiveDamage(damage);
        }
    }
}