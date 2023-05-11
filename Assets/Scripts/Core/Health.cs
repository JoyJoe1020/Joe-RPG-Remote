using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    // 定义一个处理角色生命值的类Health
    public class Health : MonoBehaviour
    {
        // 使用[SerializeField]属性，使得health变量可以在Unity编辑器中进行调整
        [SerializeField] float health = 100f;

        // 一个处理角色受到伤害的方法，传入一个表示伤害值的参数damage
        public void TakeDamage(float damage)
        {
            // 使用Mathf.Max确保生命值不低于0，避免出现负数
            health = Mathf.Max(health - damage, 0);
            // 输出当前生命值，方便调试和观察角色生命值的变化
            print(health);
        }
    }
}
