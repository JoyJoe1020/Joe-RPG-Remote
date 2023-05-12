    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using RPG.Combat;
    using RPG.Core;
    using RPG.Movement;

    // RPG.Control命名空间下定义一个AIController类，继承自MonoBehaviour，用于处理AI角色的控制逻辑
    namespace RPG.Control
    {
        public class AIController : MonoBehaviour
        {
            // 设置AI追击范围距离
            [SerializeField] float chaseDistance = 5f;
            // 定义Fighter类型变量
            Fighter fighter;
            // 定义Health类型变量
            Health health;
            // 定义Mover类型变量
            Mover mover;
            // 定义player对象
            GameObject player;
            // 定义初始守卫位置
            Vector3 guardPosition;

            // 在Start方法中进行组件初始化
            private void Start() 
            {
                // 获取Fighter组件
                fighter = GetComponent<Fighter>();
                // 获取Health组件
                health = GetComponent<Health>();
                // 获取Mover组件
                mover = GetComponent<Mover>();
                // 查找标签为"Player"的游戏对象
                player = GameObject.FindWithTag("Player");

                // 获取当前角色位置作为初始守卫位置
                guardPosition = transform.position;
            }

            // 在Update方法中处理AI角色的行为逻辑
            private void Update()
            {
                // 如果角色已死亡，则不执行后续逻辑
                if (health.IsDead()) { return; }

                // 如果玩家在攻击范围内且可以攻击
                if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
                {
                    // 发起攻击
                    fighter.Attack(player);
                }
                else
                {
                    // 否则返回守卫位置
                    mover.StartMoveAction(guardPosition);
                }
            }

            // 判断玩家是否在攻击范围内的方法
            private bool InAttackRangeOfPlayer()
            {
                // 计算玩家与AI角色之间的距离
                float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
                // 判断距离是否小于追击距离
                return distanceToPlayer < chaseDistance;
            }

            // 当AI角色被选中时，在Unity编辑器中绘制表示攻击范围的球体(Called by Unity)
            private void OnDrawGizmosSelected() 
            {
                // 设置球体颜色为蓝色
                Gizmos.color = Color.blue;
                // 绘制一个以角色位置为中心，追击距离为半径的线框球体
                Gizmos.DrawWireSphere(transform.position, chaseDistance);
            }
        }
    }
