using System.Collections;
using System.Collections.Generic;
using My.Base.Units;
using UnityEngine;

public class PhysicalAttackSkill : Skill
{
    //public override int ManualTargetAmount { get; protected set; } = 1;

    public override bool IsPossibleTarget(Unit caster, Unit target)
    {
        return caster.MindedTeam!=target.RealTeam;
    }

    protected override void ApplySkillEffect(Unit caster, List<Unit> targets)
    {
        Debug.Log($"Skill {nameof(PhysicalAttackSkill)}  used by {caster.Name}");
        Damage damage = new Damage(caster.CurrentAttributes.PhysicalPower, DamageType.Strike);
        targets[0].ReceiveDamage(damage);
    }
}
