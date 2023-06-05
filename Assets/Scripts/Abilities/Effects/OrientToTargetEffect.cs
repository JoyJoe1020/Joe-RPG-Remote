using System;
using System.Collections;
using UnityEngine;

namespace RPG.Abilities.Effects
{
    // 对准目标效果类
    [CreateAssetMenu(fileName = "Orient To Target Effect", menuName = "Abilities/Effects/Orient To Target", order = 0)]
    public class OrientToTargetEffect : EffectStrategy
    {
        // 开始效果的方法，这是一个重写的方法，用于开始执行效果
        public override void StartEffect(AbilityData data, Action finished)
        {
            data.GetUser().transform.LookAt(data.GetTargetedPoint()); // 用户的视线对准目标点
            finished(); // 执行完成
        }
    }
}
