using System;
using System.Collections;
using System.Collections.Generic;
using My.Base.Units;
using UnityEngine;

public abstract class Skill
{
    public int ManualTargetAmount { get; protected set; } = 1;
    public abstract bool IsPossibleTarget(Unit caster, Unit target);

    public void Use(Unit caster, List<Unit> targets)
    {
        foreach (var target in targets)
        {
            if (!IsPossibleTarget(caster, target))
            {
                Debug.LogError($"Selected invalid target {target.Name} for skill class [{this.GetType().Name}]");
            }
            ApplySkillEffect(caster, targets);
        }
    }

    protected abstract void ApplySkillEffect(Unit caster, List<Unit> targets);
}
