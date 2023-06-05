using System;
using System.Collections.Generic;
using RPG.Control;
using UnityEngine;

namespace RPG.Abilities.Targeting
{
    // 方向性目标选择策略，用于创建可在编辑器中使用的新的脚本对象
    [CreateAssetMenu(fileName = "Directional Targeting", menuName = "Abilities/Targeting/Directional", order = 0)]
    public class DirectionalTargeting : TargetingStrategy
    {
        [SerializeField] LayerMask layerMask; // 用于射线投射的图层掩码
        [SerializeField] float groundOffset = 1; // 地面的偏移量

        // 开始目标选择
        public override void StartTargeting(AbilityData data, Action finished)
        {
            RaycastHit raycastHit; // 射线投射结果
            Ray ray = PlayerController.GetMouseRay(); // 获取从玩家位置到鼠标位置的射线
            if (Physics.Raycast(ray, out raycastHit, 1000, layerMask)) // 在指定的图层上进行射线投射
            {
                // 设置目标点为射线和地面交点的位置，考虑地面的偏移
                data.SetTargetedPoint(raycastHit.point + ray.direction * groundOffset / ray.direction.y);
            }
            finished(); // 目标选择完成
        }
    }
}