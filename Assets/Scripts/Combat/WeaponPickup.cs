using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    // WeaponPickup类用于实现武器拾取功能
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] Weapon weapon = null; // 武器对象，由Unity Inspector指定

        // OnTriggerEnter方法会在有其它对象进入此对象的Collider时被调用
        private void OnTriggerEnter(Collider other)
        {
            // 检查进入Collider的对象是否被标记为"Player"
            if (other.gameObject.tag == "Player")
            {
                // 如果是玩家，调用Fighter组件的EquipWeapon方法装备这个武器
                other.GetComponent<Fighter>().EquipWeapon(weapon);
                // 销毁武器拾取物（本对象）
                Destroy(gameObject);
            }
        }
    }
}
