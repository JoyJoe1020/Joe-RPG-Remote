using System.Collections.Generic;
using UnityEngine;

namespace RPG.Abilities.Filters
{
    // ��ǩ������
    [CreateAssetMenu(fileName = "Tag Filter", menuName = "Abilities/Filters/Tag", order = 0)]
    public class TagFilter : FilterStrategy
    {
        [SerializeField] string tagToFilter = ""; // Ҫ���˵ı�ǩ����

        public override IEnumerable<GameObject> Filter(IEnumerable<GameObject> objectsToFilter)
        {
            foreach (var gameObject in objectsToFilter)
            {
                if (gameObject.CompareTag(tagToFilter)) // ����ǩ�Ƿ�ƥ��
                {
                    yield return gameObject; // ���ط�����������Ϸ����
                }
            }
        }
    }
}
