using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI.DamageText
{
    public class DamageTextSpawner : MonoBehaviour
    {
        [SerializeField] DamageText damageTextPrefab = null;  // 伤害文本预制体

        public void Spawn(float damageAmount)
        {
            DamageText instance = Instantiate<DamageText>(damageTextPrefab, transform);  // 实例化伤害文本预制体，并将其设置为当前对象的子对象
            instance.SetValue(damageAmount);  // 设置伤害文本的值
        }
    }
}