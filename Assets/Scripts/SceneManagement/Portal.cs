using System;
using System.Collections;
using RPG.Control;
using GameDevTV.Saving;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        //����һ����ʾ������Ŀ�ĵص�ö������
        enum DestinationIdentifier
        {
            A, B, C, D, E
        }

        //�������ر�ţ�������Ŀ��λ�ã�������Ŀ�ĵر�ʶ������ʱ�䣬����ʱ�䣬�����ȴ�ʱ��
        [SerializeField] int sceneToLoad = -1;
        [SerializeField] Transform spawnPoint;
        [SerializeField] DestinationIdentifier destination;
        [SerializeField] float fadeOutTime = 1f;
        [SerializeField] float fadeInTime = 2f;
        [SerializeField] float fadeWaitTime = 0.5f;

        //����ҽ��봫����ʱ����ʼ����ת��
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                StartCoroutine(Transition());
            }
        }

        //����ת��Э��
        private IEnumerator Transition()
        {
            //��鳡�����ر���Ƿ���Ч
            if (sceneToLoad < 0)
            {
                Debug.LogError("Scene to load not set.");
                yield break;
            }

            //��ֹ��ǰ��Ϸ�����ڼ����³���ʱ������
            DontDestroyOnLoad(gameObject);

            //��ȡFader��SavingWrapper���
            Fader fader = FindObjectOfType<Fader>();
            SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();

            //������ҿ�����
            PlayerController playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            playerController.enabled = false;

            //��ʼ������������Ϸ�������³�����������Ϸ����ʼ���룬���������ҿ����������ٵ�ǰ��Ϸ����
            yield return fader.FadeOut(fadeOutTime);

            savingWrapper.Save();

            yield return SceneManager.LoadSceneAsync(sceneToLoad);
            PlayerController newPlayerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            newPlayerController.enabled = false;

            savingWrapper.Load();

            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

            savingWrapper.Save();

            yield return new WaitForSeconds(fadeWaitTime);
            fader.FadeIn(fadeInTime);

            newPlayerController.enabled = true;
            Destroy(gameObject);
        }

        //�������λ�úͷ���
        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = otherPortal.spawnPoint.position;
            player.transform.rotation = otherPortal.spawnPoint.rotation;
            player.GetComponent<NavMeshAgent>().enabled = true;
        }

        //��ȡĿ�괫����
        private Portal GetOtherPortal()
        {
            //�������еĴ����ţ�ѡ��͵�ǰ�����ŵ�Ŀ�ĵر�ʶ��ͬ���Ҳ��ǵ�ǰ�����ŵĴ�����
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;
                if (portal.destination != destination) continue;

                return portal;
            }

            return null;
        }
    }
}