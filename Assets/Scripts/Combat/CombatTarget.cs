// 引入RPG.Core命名空间，以使用其中的Health类
using RPG.Core;
using UnityEngine;

// 在RPG.combat命名空间下定义一个名为CombatTarget的类，继承自MonoBehaviour
// 该类用于表示游戏中的战斗目标，可以通过其他脚本对其进行攻击等操作
namespace RPG.Combat
{
    // 为CombatTarget类添加RequireComponent属性，确保在使用该类的GameObject上同时添加Health组件
    // 如果在GameObject上添加CombatTarget组件时没有Health组件，Unity将自动添加一个
    [RequireComponent(typeof(Health))]

    public class CombatTarget : MonoBehaviour
    {
        // 该类的目的是为了标识游戏对象为战斗目标，因此不需要定义变量和方法
        // 其他脚本可以通过检查游戏对象上是否存在CombatTarget组件来判断是否为合法战斗目标
    }
}