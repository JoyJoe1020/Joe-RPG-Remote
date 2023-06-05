using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    // 仇恨组类，用于管理一组战斗者的仇恨
    public class AggroGroup : MonoBehaviour
    {
        [SerializeField] Fighter[] fighters; // 战斗者数组
        [SerializeField] bool activateOnStart = false; // 是否在启动时激活

        private void Start()
        {
            Activate(activateOnStart); // 在启动时激活/禁用
        }

        public void Activate(bool shouldActivate)
        {
            foreach (Fighter fighter in fighters)
            {
                CombatTarget target = fighter.GetComponent<CombatTarget>();
                if (target != null)
                {
                    target.enabled = shouldActivate; // 激活/禁用战斗目标组件
                }
                fighter.enabled = shouldActivate; // 激活/禁用战斗者组件
            }
        }
    }
}