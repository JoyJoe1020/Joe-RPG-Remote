// 使用System.Collections命名空间，此命名空间定义了一些类，接口和方法，这些都是用于处理对象的集合的。
using System.Collections;
// 使用System.Collections.Generic命名空间，此命名空间包含定义泛型集合的接口和类，使用户可以创建强类型集合，提高性能。
using System.Collections.Generic;
// 使用Unity命名空间，此命名空间是Unity引擎的核心API
using UnityEngine;

// 定义RPG.Combat命名空间
namespace RPG.Combat
{
    // 定义一个公共类AggroGroup，继承自Unity的MonoBehaviour基类，即这是一个Unity的组件
    public class AggroGroup : MonoBehaviour
    {
        // 使用SerializeField属性，使得下面的字段可以在Unity编辑器中进行设置
        [SerializeField] Fighter[] fighters;  // 定义一个Fighter对象的数组，存储战斗者信息
        [SerializeField] bool activateOnStart = false;  // 定义一个布尔值，表示是否在开始时激活

        // MonoBehaviour的Start方法，在对象第一次启用或游戏开始时被Unity调用
        private void Start()
        {
            Activate(activateOnStart);  // 在Start方法中调用Activate方法，参数为activateOnStart
        }

        // 定义一个公共方法Activate，用于激活或禁用所有战斗者的战斗状态
        public void Activate(bool shouldActivate)
        {
            // 遍历fighters数组中的所有Fighter对象
            foreach (Fighter fighter in fighters)
            {
                // 获取Fighter对象的CombatTarget组件
                CombatTarget target = fighter.GetComponent<CombatTarget>();
                // 如果target不为空，即Fighter对象有CombatTarget组件
                if (target != null)
                {
                    // 根据shouldActivate的值，设置CombatTarget组件的enabled属性，即激活或禁用CombatTarget组件
                    target.enabled = shouldActivate;
                }
                // 根据shouldActivate的值，设置Fighter对象的enabled属性，即激活或禁用Fighter对象
                fighter.enabled = shouldActivate;
            }
        }
    }
}
