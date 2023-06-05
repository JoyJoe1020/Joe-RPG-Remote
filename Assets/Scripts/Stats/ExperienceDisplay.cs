using System;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Stats
{
    public class ExperienceDisplay : MonoBehaviour
    {
        Experience experience;  // 经验值组件

        private void Awake()
        {
            experience = GameObject.FindWithTag("Player").GetComponent<Experience>();  // 获取角色经验值组件
        }

        private void Update()
        {
            GetComponent<Text>().text = String.Format("{0:0}", experience.GetPoints());  // 更新UI文本显示经验值
        }
    }
}
