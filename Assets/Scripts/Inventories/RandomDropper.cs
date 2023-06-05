using GameDevTV.Inventories;
using RPG.Stats;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Inventories
{
    // 随机掉落物品的类，继承自ItemDropper
    public class RandomDropper : ItemDropper
    {
        // 配置数据
        [Tooltip("How far can the pickups be scattered from the dropper.")]
        [SerializeField] float scatterDistance = 1;  // 掉落物品离掉落者的散布距离
        [SerializeField] DropLibrary dropLibrary;  // 掉落库

        // 常量
        const int ATTEMPTS = 30;  // 尝试获取NavMesh的次数

        // 随机掉落物品的方法
        public void RandomDrop()
        {
            var baseStats = GetComponent<BaseStats>();  // 获取BaseStats组件

            var drops = dropLibrary.GetRandomDrops(baseStats.GetLevel());  // 根据角色等级从掉落库获取随机掉落物品
            foreach (var drop in drops)
            {
                DropItem(drop.item, drop.number);  // 掉落物品
            }
        }

        protected override Vector3 GetDropLocation()
        {
            // 我们可能需要多次尝试才能在NavMesh上获取位置
            for (int i = 0; i < ATTEMPTS; i++)
            {
                Vector3 randomPoint = transform.position + Random.insideUnitSphere * scatterDistance;  // 在离掉落者一定距离内随机选择一个位置
                NavMeshHit hit;
                if (NavMesh.SamplePosition(randomPoint, out hit, 0.1f, NavMesh.AllAreas))  // 在离掉落者一定距离内的位置上获取离NavMesh最近的点
                {
                    return hit.position;  // 返回位置坐标
                }
            }
            return transform.position;  // 如果没有找到合适的位置，则返回掉落者的位置
        }
    }
}