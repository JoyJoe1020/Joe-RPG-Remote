using UnityEngine;
using RPG.Movement;
using System;
using RPG.Combat;
using RPG.Core;

// 在RPG.Control命名空间下定义一个名为PlayerController的类，继承自MonoBehaviour，用于处理玩家控制
namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        // 定义一个Health类型的变量，用于存储游戏对象的生命值组件
        Health health;

        // 在Start方法中初始化Health组件
        private void Start()
        {
            // 获取当前游戏对象上的Health组件
            health = GetComponent<Health>();
        }

        // 在每一帧更新时调用
        private void Update()
        {
            // 如果角色已死亡，则不执行后续逻辑
            if (health.IsDead()) { return; }

            // 与战斗进行交互，如果有交互，返回
            if (InteractWithCombat()) return;
            // 与移动进行交互，如果有交互，返回
            if (InteractWithMovement()) return;
        }

        // 处理与战斗的交互
        private bool InteractWithCombat()
        {
            // 获取鼠标点击射线上所有碰撞的物体
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            // 遍历所有碰撞物体
            foreach (RaycastHit hit in hits)
            {
                // 尝试获取物体上的CombatTarget组件
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();

                // 如果目标对象上没有CombatTarget组件，则跳过当前迭代并继续检查下一个物体
                if (target == null) { continue; }

                // 获取目标游戏对象
                GameObject targetGameObject = target.gameObject;

                // 判断当前游戏对象是否可以攻击目标游戏对象，如果不能攻击，则跳过当前迭代并继续检查下一个物体
                if (!GetComponent<Fighter>().CanAttack(target.gameObject))
                {
                    continue;
                }

                // 如果鼠标左键被按下
                if (Input.GetMouseButton(0))
                {
                    // 获取当前游戏对象上的Fighter组件，调用Attack方法对目标游戏对象发起攻击
                    GetComponent<Fighter>().Attack(target.gameObject);
                }
                // 发生交互，返回true
                return true;
            }
            // 未发生交互，返回false
            return false;
        }

        // 处理与移动的交互
        private bool InteractWithMovement()
        {
            // 创建一个射线碰撞信息变量
            RaycastHit hit;
            // 发送射线，检查是否与场景中的物体碰撞
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            // 如果与物体发生碰撞
            if (hasHit)
            {
                // 如果鼠标左键被按下
                if (Input.GetMouseButton(0))
                {
                    // 获取当前对象的Mover组件，调用MoveTo方法，将目标位置设置为射线与物体的交点
                    GetComponent<Mover>().StartMoveAction(hit.point, 1f);
                }
                // 发生交互，返回true
                return true;
            }
            // 未发生交互，返回false
            return false;
        }

        // 获取鼠标点击产生的射线
        private static Ray GetMouseRay()
        {
            // 返回从主摄像机产生的射线，射线方向基于鼠标在屏幕上的位置
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
