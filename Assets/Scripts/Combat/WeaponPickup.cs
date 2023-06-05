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
        [SerializeField] WeaponConfig weapon = null; // 武器配置
        [SerializeField] float healthToRestore = 0; // 恢复的生命值
        [SerializeField] float respawnTime = 5; // 重新生成时间

        // 触发器检测到玩家进入时执行
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                Pickup(other.gameObject); // 玩家拾取武器
            }
        }

        // 拾取武器
        private void Pickup(GameObject subject)
        {
            if (weapon != null)
            {
                subject.GetComponent<Fighter>().EquipWeapon(weapon); // 给玩家装备武器
            }
            if (healthToRestore > 0)
            {
                subject.GetComponent<Health>().Heal(healthToRestore); // 恢复玩家生命值
            }
            StartCoroutine(HideForSeconds(respawnTime)); // 隐藏武器一段时间后重新显示
        }

        // 隐藏一段时间
        private IEnumerator HideForSeconds(float seconds)
        {
            ShowPickup(false); // 隐藏武器
            yield return new WaitForSeconds(seconds); // 等待指定时间
            ShowPickup(true); // 显示武器
        }

        // 显示或隐藏武器
        private void ShowPickup(bool shouldShow)
        {
            GetComponent<Collider>().enabled = shouldShow;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(shouldShow);
            }
        }

        // 处理射线检测
        public bool HandleRaycast(PlayerController callingController)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Pickup(callingController.gameObject); // 玩家拾取武器
            }
            return true;
        }

        // 获取光标类型
        public CursorType GetCursorType()
        {
            return CursorType.Pickup;
        }
    }
}
