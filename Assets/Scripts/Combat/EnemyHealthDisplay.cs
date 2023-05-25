// 使用System命名空间，此命名空间定义了一些基本的类和基元类型
using System;
// 使用RPG.Attributes命名空间，此命名空间定义了RPG游戏的一些属性
using RPG.Attributes;
// 使用UnityEngine命名空间，此命名空间是Unity引擎的核心API
using UnityEngine;
// 使用UnityEngine.UI命名空间，此命名空间是Unity引擎的UI系统的API
using UnityEngine.UI;

// 定义RPG.Combat命名空间
namespace RPG.Combat
{
    // 定义一个公共类EnemyHealthDisplay，继承自Unity的MonoBehaviour基类
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Fighter fighter;  // 定义一个Fighter对象，用于存储玩家的战斗信息

        // MonoBehaviour的Awake方法，在所有的Start方法之前调用，当脚本实例被载入时调用
        private void Awake()
        {
            // 在场景中查找标签为"Player"的GameObject，然后获取其Fighter组件
            fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
        }

        // MonoBehaviour的Update方法，在每一帧中都会被Unity调用
        private void Update()
        {
            // 如果玩家的战斗目标为空
            if (fighter.GetTarget() == null)
            {
                // 获取当前GameObject的Text组件，并设置其文本为"N/A"
                GetComponent<Text>().text = "N/A";
                return;  // 结束Update方法的执行
            }
            // 获取玩家的战斗目标的Health组件
            Health health = fighter.GetTarget();
            // 获取当前GameObject的Text组件，并设置其文本为目标的当前健康值和最大健康值
            GetComponent<Text>().text = String.Format("{0:0}/{1:0}", health.GetHealthPoints(), health.GetMaxHealthPoints());
        }
    }
}
