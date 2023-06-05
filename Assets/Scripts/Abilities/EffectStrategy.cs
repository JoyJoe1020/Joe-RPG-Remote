using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Abilities
{
    // 技能效果策略的抽象类
    public abstract class EffectStrategy : ScriptableObject
    {
        // 开始技能效果
        public abstract void StartEffect(AbilityData data, Action finished);
    }

}