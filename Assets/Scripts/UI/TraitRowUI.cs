using RPG.Stats;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class TraitRowUI : MonoBehaviour
    {
        [SerializeField] Trait trait;  // ����
        [SerializeField] TextMeshProUGUI valueText;  // ֵ�ı�
        [SerializeField] Button minusButton;  // ���ٰ�ť
        [SerializeField] Button plusButton;  // ���Ӱ�ť

        TraitStore playerTraitStore = null;

        private void Start()
        {
            playerTraitStore = GameObject.FindGameObjectWithTag("Player").GetComponent<TraitStore>();  // ��ȡ��ҵ������洢���
            minusButton.onClick.AddListener(() => Allocate(-1));  // ע����ٰ�ť����¼������为ֵ
            plusButton.onClick.AddListener(() => Allocate(+1));  // ע�����Ӱ�ť����¼���������ֵ
        }

        private void Update()
        {
            minusButton.interactable = playerTraitStore.CanAssignPoints(trait, -1);  // ���������洢����ж��Ƿ���Է��为ֵ�����ü��ٰ�ť�Ŀɽ�����
            plusButton.interactable = playerTraitStore.CanAssignPoints(trait, +1);  // ���������洢����ж��Ƿ���Է�����ֵ���������Ӱ�ť�Ŀɽ�����

            valueText.text = playerTraitStore.GetProposedPoints(trait).ToString();  // ���������洢�����ȡ�ض�������Ԥ�Ʒ������������ֵ�ı�
        }

        public void Allocate(int points)
        {
            playerTraitStore.AssignPoints(trait, points);  // ���������洢��������ض������ĵ���
        }

    }
}