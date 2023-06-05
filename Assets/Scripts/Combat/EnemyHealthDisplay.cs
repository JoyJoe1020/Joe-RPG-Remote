using System;
using RPG.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Combat
{
    // 敌方生命值显示类，用于在UI中显示敌方生命值
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Fighter fighter; // 战斗者组件

        private void Awake()
        {
            fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>(); // 在标签为"Player"的游戏对象中查找并获取战斗者组件
        }

        private void Update()
        {
            if (fighter.GetTarget() == null) // 如果战斗者的目标为空
            {
                GetComponent<Text>().text = "N/A"; // 显示"N/A"
                return;
            }
            Health health = fighter.GetTarget(); // 获取目标的生命值组件
            GetComponent<Text>().text = String.Format("{0:0}/{1:0}", health.GetHealthPoints(), health.GetMaxHealthPoints()); // 格式化文本，显示当前生命值和最大生命值
        }
    }
}