using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Abilities
{
    // ����Ŀ��ѡ����Եĳ�����
    public abstract class TargetingStrategy : ScriptableObject
    {
        // ��ʼĿ��ѡ��
        public abstract void StartTargeting(AbilityData data, Action finished);
    }
}