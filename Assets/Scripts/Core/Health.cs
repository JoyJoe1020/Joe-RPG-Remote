using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    // 定义一个处理角色生命值的类Health
    public class Health : MonoBehaviour
    {
        // 使用[SerializeField]属性，使得health变量可以在Unity编辑器中进行调整
        [SerializeField] float healthPoints = 100f;

        bool isDead = false;

        // 一个处理角色受到伤害的方法，传入一个表示伤害值的参数damage
        public void TakeDamage(float damage)
        {
            // 使用Mathf.Max确保生命值不低于0，避免出现负数
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if(healthPoints == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (isDead) return;

            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
        }
    }
}
