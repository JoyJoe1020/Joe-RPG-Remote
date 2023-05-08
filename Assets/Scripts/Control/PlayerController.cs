using UnityEngine;
using RPG.Movement;
using System;
using RPG.combat;

// 命名空间RPG.Control下定义一个名为PlayerController的类，继承自MonoBehaviour，用于处理玩家控制
namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {

        // 在每一帧更新时调用
        private void Update()
        {
            // 与战斗进行交互
            InteractWithCombat();
            // 与移动进行交互
            InteractWithMovement();
        }

        // 处理与战斗的交互
        private void InteractWithCombat()
        {
            // 获取鼠标点击射线上所有碰撞的物体
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            // 遍历所有碰撞物体
            foreach (RaycastHit hit in hits)
            {
                // 尝试获取物体上的CombatTarget组件
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                // 如果没有CombatTarget组件，则跳过此次循环
                if (target == null) continue;

                // 如果鼠标左键被按下
                if (Input.GetMouseButtonDown(0))
                {
                    // 获取当前对象的Fighter组件并对目标发起攻击
                    GetComponent<Fighter>().Attack(target);
                }
            }
        }

        // 处理与移动的交互
        private void InteractWithMovement()
        {
            // 如果鼠标左键被按下
            if (Input.GetMouseButton(0))
            {
                // 移动至鼠标点击的位置
                MoveToCursor();
            }
        }

        // 移动至鼠标点击的位置
        private void MoveToCursor()
        {
            // 创建一个射线碰撞信息变量
            RaycastHit hit;
            // 发送射线，检查是否与场景中的物体碰撞
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            // 如果与物体发生碰撞
            if (hasHit)
            {
                // 获取当前对象的Mover组件，调用MoveTo方法，将目标位置设置为射线与物体的交点
                GetComponent<Mover>().MoveTo(hit.point);
            }
        }

        // 获取鼠标点击产生的射线
        private static Ray GetMouseRay()
        {
            // 返回从主摄像机产生的射线，射线方向基于鼠标在屏幕上的位置
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}