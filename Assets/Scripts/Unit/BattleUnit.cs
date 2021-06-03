using System.Collections;
using System.Collections.Generic;
using My.Base.Units;
using UnityEngine;
using UnityEngine.Serialization;

namespace My.Base.Battle
{
    public class BattleUnit : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        public Unit UnitModel { get; set; }

        public void InitLook(UnitTypes unitLookType)
        {
            Debug.Log($"Units/{unitLookType.ToString()}");
            spriteRenderer.sprite = Resources.Load<Sprite>($"Units/{unitLookType.ToString()}");
        }
        
    }
}