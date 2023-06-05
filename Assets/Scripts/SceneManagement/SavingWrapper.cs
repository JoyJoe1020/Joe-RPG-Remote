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
        //定义保存文件的键名
        private const string currentSaveKey = "currentSaveName";
        //定义淡入淡出的时间
        [SerializeField] float fadeInTime = 0.2f;
        [SerializeField] float fadeOutTime = 0.2f;
        //定义第一级和菜单级的构建索引
        [SerializeField] int firstLevelBuildIndex = 1;
        [SerializeField] int menuLevelBuildIndex = 0;

        //继续游戏
        public void ContinueGame()
        {
            StartCoroutine(LoadLastScene());
        }

        //新游戏
        public void NewGame(string saveFile)
        {
            SetCurrentSave(saveFile);
            StartCoroutine(LoadFirstScene());
        }

        //加载游戏
        public void LoadGame(string saveFile)
        {
            SetCurrentSave(saveFile);
            ContinueGame();
        }

        //加载菜单
        public void LoadMenu()
        {
            StartCoroutine(LoadMenuScene());
        }

        //设置当前保存文件
        private void SetCurrentSave(string saveFile)
        {
            PlayerPrefs.SetString(currentSaveKey, saveFile);
        }

        //获取当前保存文件
        private string GetCurrentSave()
        {
            return PlayerPrefs.GetString(currentSaveKey);
        }

        //加载最后一个场景
        private IEnumerator LoadLastScene()
        {
            Fader fader = FindObjectOfType<Fader>();
            yield return fader.FadeOut(fadeOutTime);
            yield return GetComponent<SavingSystem>().LoadLastScene(GetCurrentSave());
            yield return fader.FadeIn(fadeInTime);
        }

        //加载第一场景
        private IEnumerator LoadFirstScene()
        {
            Fader fader = FindObjectOfType<Fader>();
            yield return fader.FadeOut(fadeOutTime);
            yield return SceneManager.LoadSceneAsync(firstLevelBuildIndex);
            yield return fader.FadeIn(fadeInTime);
        }

        //加载菜单场景
        private IEnumerator LoadMenuScene()
        {
            Fader fader = FindObjectOfType<Fader>();
            yield return fader.FadeOut(fadeOutTime);
            yield return SceneManager.LoadSceneAsync(menuLevelBuildIndex);
            yield return fader.FadeIn(fadeInTime);
        }

        //响应按键操作，执行保存、加载和删除操作
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

        //加载游戏
        public void Load()
        {
            GetComponent<SavingSystem>().Load(GetCurrentSave());
        }

        //保存游戏
        public void Save()
        {
            GetComponent<SavingSystem>().Save(GetCurrentSave());
        }

        //删除游戏
        public void Delete()
        {
            GetComponent<SavingSystem>().Delete(GetCurrentSave());
        }

        //列出所有的保存文件
        public IEnumerable<string> ListSaves()
        {
            return GetComponent<SavingSystem>().ListSaves();
        }
    }
}
