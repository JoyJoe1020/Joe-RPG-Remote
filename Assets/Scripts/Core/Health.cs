using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

namespace RPG.Core
{
    // Health类：处理角色生命值相关逻辑
    public class Health : MonoBehaviour, ISaveable
    {
        // 角色生命值，可在Unity编辑器中调整
        [SerializeField] float healthPoints = 100f;

        // 标识角色是否死亡的布尔变量
        bool isDead = false;

        // IsDead方法：返回角色是否死亡的状态
        public bool IsDead()
        {
            return isDead;
        }

        // TakeDamage方法：处理角色受到伤害的逻辑，参数damage表示伤害值
        public void TakeDamage(float damage)
        {
            // 使用Mathf.Max确保生命值不低于0，避免出现负数
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            // 当生命值降至0时，执行Die方法
            if(healthPoints == 0)
            {
                Die();
            }
        }

        // Die方法：处理角色死亡逻辑
        private void Die()
        {
            // 如果角色已经死亡，直接返回，不再执行后续逻辑
            if (isDead) return;

            // 设置角色为死亡状态
            isDead = true;
            // 触发角色动画组件的“die”触发器，播放死亡动画
            GetComponent<Animator>().SetTrigger("die");

            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        public object CaptureState()
        {
            return healthPoints;
        }


        public void RestoreState(object state)
        {
            healthPoints = (float)state;

            if (healthPoints == 0)
            {
                Die();
            }
        }

    }
}
