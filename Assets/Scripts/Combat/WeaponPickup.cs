using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Attributes;
using RPG.Control;
using UnityEngine;

namespace RPG.Combat
{
    // 武器拾取类，用于处理玩家拾取武器
    public class WeaponPickup : MonoBehaviour, IRaycastable
    {
        [SerializeField] WeaponConfig weapon = null; // 拾取武器的配置
        [SerializeField] float healthToRestore = 0; // 拾取武器后恢复的生命值
        [SerializeField] float respawnTime = 5; // 武器重新出现的时间

        // 当其他游戏对象的碰撞器进入触发器时调用
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player") // 如果进入触发器的游戏对象是玩家
            {
                Pickup(other.gameObject); // 让玩家拾取武器
            }
        }

        // 拾取武器
        private void Pickup(GameObject subject)
        {
            if (weapon != null) // 如果武器存在
            {
                subject.GetComponent<Fighter>().EquipWeapon(weapon); // 让玩家装备这个武器
            }
            if (healthToRestore > 0) // 如果恢复的生命值大于0
            {
                subject.GetComponent<Health>().Heal(healthToRestore); // 让玩家恢复生命值
            }
            StartCoroutine(HideForSeconds(respawnTime)); // 隐藏一段时间后再显示武器
        }

        // 隐藏一段时间
        private IEnumerator HideForSeconds(float seconds)
        {
            ShowPickup(false); // 隐藏武器
            yield return new WaitForSeconds(seconds); // 等待一段时间
            ShowPickup(true); // 显示武器
        }

        // 显示或隐藏武器
        private void ShowPickup(bool shouldShow)
        {
            GetComponent<Collider>().enabled = shouldShow; // 设置触发器的启用状态
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(shouldShow); // 设置子游戏对象的启用状态
            }
        }

        // 处理射线检测
        public bool HandleRaycast(PlayerController callingController)
        {
            if (Input.GetMouseButtonDown(0)) // 如果按下鼠标左键
            {
                Pickup(callingController.gameObject); // 让玩家拾取武器
            }
            return true;
        }

        // 获取光标类型
        public CursorType GetCursorType()
        {
            return CursorType.Pickup; // 返回拾取光标类型
        }
    }
}
