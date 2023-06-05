using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Abilities
{
    // 技能目标选择策略的抽象类
    public abstract class TargetingStrategy : ScriptableObject
    {
        // 开始目标选择
        public abstract void StartTargeting(AbilityData data, Action finished);
    }
}