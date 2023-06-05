using System;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Stats
{
    public class LevelDisplay : MonoBehaviour
    {
        BaseStats baseStats;  // 基础属性组件

        private void Awake()
        {
            baseStats = GameObject.FindWithTag("Player").GetComponent<BaseStats>();  // 获取角色基础属性组件
        }

        private void Update()
        {
            GetComponent<Text>().text = String.Format("{0:0}", baseStats.GetLevel());  // 更新UI文本显示等级
        }
    }
}
