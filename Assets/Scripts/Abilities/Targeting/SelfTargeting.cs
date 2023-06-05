using System;
using UnityEngine;

namespace RPG.Abilities.Targeting
{
    // 自身目标选择策略，用于创建可在编辑器中使用的新的脚本对象
    [CreateAssetMenu(fileName = "Self Targeting", menuName = "Abilities/Targeting/Self", order = 0)]
    public class SelfTargeting : TargetingStrategy
    {
        // 开始目标选择
        public override void StartTargeting(AbilityData data, Action finished)
        {
            // 将目标设置为使用技能的游戏对象本身
            data.SetTargets(new GameObject[] { data.GetUser() });
            // 将目标点设置为使用技能的游戏对象的位置
            data.SetTargetedPoint(data.GetUser().transform.position);
            finished(); // 目标选择完成
        }
    }
}