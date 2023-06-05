using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Abilities
{
    // ���ܹ��˲��Եĳ�����
    public abstract class FilterStrategy : ScriptableObject
    {
        // ����Ŀ�����
        public abstract IEnumerable<GameObject> Filter(IEnumerable<GameObject> objectsToFilter);
    }
}