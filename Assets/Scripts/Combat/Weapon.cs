using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Combat
{
    // 武器类，用于处理武器的行为
    public class Weapon : MonoBehaviour
    {
        [SerializeField] UnityEvent onHit; // 武器命中事件

        public void OnHit()
        {
            onHit.Invoke(); // 触发武器命中事件
        }
    }
}