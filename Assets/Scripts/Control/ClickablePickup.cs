using System.Collections;
using System.Collections.Generic;
using GameDevTV.Inventories;
using UnityEngine;

namespace RPG.Control
{
    // 可点击的拾取物品类，用于处理玩家点击拾取物品
    [RequireComponent(typeof(Pickup))]
    public class ClickablePickup : MonoBehaviour, IRaycastable
    {
        Pickup pickup;

        private void Awake()
        {
            pickup = GetComponent<Pickup>();
        }

        // 获取光标类型
        public CursorType GetCursorType()
        {
            if (pickup.CanBePickedUp())
            {
                return CursorType.Pickup;
            }
            else
            {
                return CursorType.FullPickup;
            }
        }

        // 处理射线检测
        public bool HandleRaycast(PlayerController callingController)
        {
            if (Input.GetMouseButtonDown(0))
            {
                pickup.PickupItem();
            }
            return true;
        }
    }
}
