using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Abilities
{
    // ����Ч�����Եĳ�����
    public abstract class EffectStrategy : ScriptableObject
    {
        // ��ʼ����Ч��
        public abstract void StartEffect(AbilityData data, Action finished);
    }

}