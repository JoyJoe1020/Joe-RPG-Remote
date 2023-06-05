using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    // 仇恨组类，用于管理一组战斗者的仇恨
    public class AggroGroup : MonoBehaviour
    {
        [SerializeField] Fighter[] fighters; // 需要被管理的战斗者数组
        [SerializeField] bool activateOnStart = false; // 是否在游戏启动时激活仇恨组

        private void Start()
        {
            Activate(activateOnStart); // 根据设置决定是否在游戏启动时激活仇恨组
        }

        // 激活或禁用仇恨组
        public void Activate(bool shouldActivate)
        {
            foreach (Fighter fighter in fighters) // 对仇恨组中的每个战斗者执行以下操作
            {
                CombatTarget target = fighter.GetComponent<CombatTarget>();
                if (target != null)
                {
                    target.enabled = shouldActivate; // 激活或禁用战斗目标组件
                }
                fighter.enabled = shouldActivate; // 激活或禁用战斗者组件
            }
        }
    }
}
