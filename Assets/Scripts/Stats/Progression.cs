using UnityEngine;
using System.Collections.Generic;
using System;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClasses = null;  // 角色职业的进阶数据

        Dictionary<CharacterClass, Dictionary<Stat, float[]>> lookupTable = null;  // 数据查找表

        public float GetStat(Stat stat, CharacterClass characterClass, int level)
        {
            BuildLookup();  // 构建查找表

            if (!lookupTable[characterClass].ContainsKey(stat))
            {
                return 0;
            }

            float[] levels = lookupTable[characterClass][stat];

            if (levels.Length == 0)
            {
                return 0;
            }

            if (levels.Length < level)
            {
                return levels[levels.Length - 1];
            }

            return levels[level - 1];
        }

        public int GetLevels(Stat stat, CharacterClass characterClass)
        {
            BuildLookup();  // 构建查找表

            float[] levels = lookupTable[characterClass][stat];
            return levels.Length;
        }

        private void BuildLookup()
        {
            if (lookupTable != null) return;

            lookupTable = new Dictionary<CharacterClass, Dictionary<Stat, float[]>>();

            foreach (ProgressionCharacterClass progressionClass in characterClasses)
            {
                var statLookupTable = new Dictionary<Stat, float[]>();

                foreach (ProgressionStat progressionStat in progressionClass.stats)
                {
                    statLookupTable[progressionStat.stat] = progressionStat.levels;
                }

                lookupTable[progressionClass.characterClass] = statLookupTable;
            }
        }

        [System.Serializable]
        class ProgressionCharacterClass
        {
            public CharacterClass characterClass;  // 角色职业
            public ProgressionStat[] stats;  // 进阶属性数据
        }

        [System.Serializable]
        class ProgressionStat
        {
            public Stat stat;  // 属性
            public float[] levels;  // 不同等级对应的属性值
        }
    }
}
