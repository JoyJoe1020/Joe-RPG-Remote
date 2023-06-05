using System.Collections.Generic;
using UnityEngine;

namespace RPG.Abilities.Filters
{
    // 标签过滤器
    [CreateAssetMenu(fileName = "Tag Filter", menuName = "Abilities/Filters/Tag", order = 0)]
    public class TagFilter : FilterStrategy
    {
        [SerializeField] string tagToFilter = ""; // 要过滤的标签名称

        public override IEnumerable<GameObject> Filter(IEnumerable<GameObject> objectsToFilter)
        {
            foreach (var gameObject in objectsToFilter)
            {
                if (gameObject.CompareTag(tagToFilter)) // 检查标签是否匹配
                {
                    yield return gameObject; // 返回符合条件的游戏对象
                }
            }
        }
    }
}
