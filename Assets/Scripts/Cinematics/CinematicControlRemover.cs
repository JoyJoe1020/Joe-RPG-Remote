using UnityEngine;
using UnityEngine.Playables;
using RPG.Core;
using RPG.Control;

namespace RPG.Cinematics
{
    // ��Ӱ���������Ƴ���
    public class CinematicControlRemover : MonoBehaviour
    {
        GameObject player; // �����Ϸ����

        private void Awake()
        {
            player = GameObject.FindWithTag("Player"); // �ڱ�ǩΪ"Player"����Ϸ�����в�����Ҷ���
        }

        private void OnEnable()
        {
            GetComponent<PlayableDirector>().played += DisableControl; // ע���Ӱ��������ʱ�Ļص�����
            GetComponent<PlayableDirector>().stopped += EnableControl; // ע���Ӱ����ֹͣʱ�Ļص�����
        }

        private void OnDisable()
        {
            GetComponent<PlayableDirector>().played -= DisableControl; // ȡ��ע���Ӱ��������ʱ�Ļص�����
            GetComponent<PlayableDirector>().stopped -= EnableControl; // ȡ��ע���Ӱ����ֹͣʱ�Ļص�����
        }

        void DisableControl(PlayableDirector pd)
        {
            player.GetComponent<ActionScheduler>().CancelCurrentAction(); // ȡ����ǰ����
            player.GetComponent<PlayerController>().enabled = false; // ������ҿ���
        }

        void EnableControl(PlayableDirector pd)
        {
            player.GetComponent<PlayerController>().enabled = true; // ������ҿ���
        }
    }
}