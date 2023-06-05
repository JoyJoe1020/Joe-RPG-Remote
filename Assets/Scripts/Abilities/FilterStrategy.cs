using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Abilities
{
    // 技能过滤策略的抽象类
    public abstract class FilterStrategy : ScriptableObject
    {
        // 过滤目标对象
        public abstract IEnumerable<GameObject> Filter(IEnumerable<GameObject> objectsToFilter);
    }
}