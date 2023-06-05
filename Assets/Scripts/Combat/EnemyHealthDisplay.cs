using System;
using RPG.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Combat
{
    // 敌方生命值显示类，用于在UI中显示敌方生命值
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Fighter fighter; // 玩家的战斗者组件

        private void Awake()
        {
            fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>(); // 在标签为"Player"的游戏对象中查找并获取战斗者组件
        }

        private void Update()
        {
            if (fighter.GetTarget() == null) // 如果玩家的目标为空
            {
                GetComponent<Text>().text = "N/A"; // 显示"N/A"
                return;
            }
            Health health = fighter.GetTarget(); // 获取玩家目标的生命值组件
            // 格式化文本，显示当前生命值和最大生命值，例如 "100/200"
            GetComponent<Text>().text = String.Format("{0:0}/{1:0}", health.GetHealthPoints(), health.GetMaxHealthPoints());
        }
    }
}
