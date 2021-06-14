using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillSelectItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI NameText;
    [SerializeField] private Button button;
    private event Action<Skill> onActivate;
    private Skill skill;

    public void Initialize(Skill skillData, Action<Skill> onItemActivated)
    {
        skill = skillData;
        NameText.text = skill.GetNameKey();
        onActivate = onItemActivated;
        button.onClick.AddListener(OnItemClick);
    }

    private void OnItemClick()
    {
        onActivate?.Invoke(skill);
    }
}
