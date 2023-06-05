using RPG.Stats;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class TraitUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI unassignedPointsText;  // δ��������ı�
        [SerializeField] Button commitButton;  // �ύ��ť

        TraitStore playerTraitStore = null;

        private void Start()
        {
            playerTraitStore = GameObject.FindGameObjectWithTag("Player").GetComponent<TraitStore>();  // ��ȡ��ҵ������洢���
            commitButton.onClick.AddListener(playerTraitStore.Commit);  // ע���ύ��ť����¼����ύ��������
        }

        private void Update()
        {
            unassignedPointsText.text = playerTraitStore.GetUnassignedPoints().ToString();  // ���������洢�����ȡδ����ĵ���������δ��������ı�
        }
    }
}