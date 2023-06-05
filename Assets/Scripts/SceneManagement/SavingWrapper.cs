using System;
using System.Collections;
using System.Collections.Generic;
using GameDevTV.Saving;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        //���屣���ļ��ļ���
        private const string currentSaveKey = "currentSaveName";
        //���嵭�뵭����ʱ��
        [SerializeField] float fadeInTime = 0.2f;
        [SerializeField] float fadeOutTime = 0.2f;
        //�����һ���Ͳ˵����Ĺ�������
        [SerializeField] int firstLevelBuildIndex = 1;
        [SerializeField] int menuLevelBuildIndex = 0;

        //������Ϸ
        public void ContinueGame()
        {
            StartCoroutine(LoadLastScene());
        }

        //����Ϸ
        public void NewGame(string saveFile)
        {
            SetCurrentSave(saveFile);
            StartCoroutine(LoadFirstScene());
        }

        //������Ϸ
        public void LoadGame(string saveFile)
        {
            SetCurrentSave(saveFile);
            ContinueGame();
        }

        //���ز˵�
        public void LoadMenu()
        {
            StartCoroutine(LoadMenuScene());
        }

        //���õ�ǰ�����ļ�
        private void SetCurrentSave(string saveFile)
        {
            PlayerPrefs.SetString(currentSaveKey, saveFile);
        }

        //��ȡ��ǰ�����ļ�
        private string GetCurrentSave()
        {
            return PlayerPrefs.GetString(currentSaveKey);
        }

        //�������һ������
        private IEnumerator LoadLastScene()
        {
            Fader fader = FindObjectOfType<Fader>();
            yield return fader.FadeOut(fadeOutTime);
            yield return GetComponent<SavingSystem>().LoadLastScene(GetCurrentSave());
            yield return fader.FadeIn(fadeInTime);
        }

        //���ص�һ����
        private IEnumerator LoadFirstScene()
        {
            Fader fader = FindObjectOfType<Fader>();
            yield return fader.FadeOut(fadeOutTime);
            yield return SceneManager.LoadSceneAsync(firstLevelBuildIndex);
            yield return fader.FadeIn(fadeInTime);
        }

        //���ز˵�����
        private IEnumerator LoadMenuScene()
        {
            Fader fader = FindObjectOfType<Fader>();
            yield return fader.FadeOut(fadeOutTime);
            yield return SceneManager.LoadSceneAsync(menuLevelBuildIndex);
            yield return fader.FadeIn(fadeInTime);
        }

        //��Ӧ����������ִ�б��桢���غ�ɾ������
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                Delete();
            }
        }

        //������Ϸ
        public void Load()
        {
            GetComponent<SavingSystem>().Load(GetCurrentSave());
        }

        //������Ϸ
        public void Save()
        {
            GetComponent<SavingSystem>().Save(GetCurrentSave());
        }

        //ɾ����Ϸ
        public void Delete()
        {
            GetComponent<SavingSystem>().Delete(GetCurrentSave());
        }

        //�г����еı����ļ�
        public IEnumerable<string> ListSaves()
        {
            return GetComponent<SavingSystem>().ListSaves();
        }
    }
}
