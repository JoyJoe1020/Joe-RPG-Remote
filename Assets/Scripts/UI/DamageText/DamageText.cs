using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI.DamageText
{
    public class DamageText : MonoBehaviour
    {
        [SerializeField] Text damageText = null;  // 伤害文本

        public void DestroyText()
        {
            Destroy(gameObject);  // 销毁文本游戏对象
        }

        public void SetValue(float amount)
        {
            damageText.text = String.Format("{0:0}", amount);  // 设置伤害文本的值
        }
    }
}