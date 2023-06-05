using System;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Attributes
{
    // 生命值显示类，用于在UI中显示生命值
    public class HealthDisplay : MonoBehaviour
    {
        Health health; // 生命值组件

        private void Awake()
        {
            health = GameObject.FindWithTag("Player").GetComponent<Health>(); // 在标签为"Player"的游戏对象中查找并获取生命值组件
        }

        private void Update()
        {
            GetComponent<Text>().text = String.Format("{0:0}/{1:0}", health.GetHealthPoints(), health.GetMaxHealthPoints()); // 格式化文本，显示当前生命值和最大生命值
        }
    }
}