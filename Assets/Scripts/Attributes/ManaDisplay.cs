using System;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Attributes
{
    // 法力值显示类，用于在UI中显示法力值
    public class ManaDisplay : MonoBehaviour
    {
        Mana mana; // 法力值组件

        private void Awake()
        {
            mana = GameObject.FindWithTag("Player").GetComponent<Mana>(); // 在标签为"Player"的游戏对象中查找并获取法力值组件
        }

        private void Update()
        {
            GetComponent<Text>().text = String.Format("{0:0}/{1:0}", mana.GetMana(), mana.GetMaxMana()); // 格式化文本，显示当前法力值和最大法力值
        }
    }
}