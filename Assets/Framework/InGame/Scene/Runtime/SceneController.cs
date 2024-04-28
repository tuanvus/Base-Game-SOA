
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using LTA.Error;


    public class SceneController
    {
        public static string CurrentScene;
        public static string CurrentSubScene;
        public static string LastScene = "";
        private static List<string> ListCurrentSubScene = new List<string>();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void OnBeforeSceneLoadRuntimeMethod()
        {
            // DataController.LoadData();
        }


        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        static void OnAfterSceneLoadRuntimeMethod()
        {
            // if(Application.internetReachability == NetworkReachability.NotReachable)
            // {
            // 	ErrorController.Instance.DoError (ErrorIndex.ErrorNetwork,OnAfterSceneLoadRuntimeMethod);
            // 	return;
            // }
        }

        public static bool ContrainSubScene(string subSceneName)
        {
            return ListCurrentSubScene.Contains(subSceneName);
        }

        public static void OpenScene(string _SceneName)
        {
            SceneHelper.SceneManager.OpenScene(_SceneName, LoadSceneMode.Single);
            PopUp.Instance.CloseAllPopUp();
            if (CurrentScene != null)
                LastScene = CurrentScene;
            CurrentScene = _SceneName;
            ListCurrentSubScene.Clear();


        }
        static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {

        }

        public static void OpenSubScene(string _SceneName)
        {
            if (!ListCurrentSubScene.Contains(_SceneName))
            {
                ListCurrentSubScene.Add(_SceneName);
                SceneHelper.SceneManager.OpenScene(_SceneName, LoadSceneMode.Additive);
            }
        }

        public static void Back()
        {
            if (LastScene != null)
            OpenScene(LastScene);
        }

        public static void OpenSingleSubScene(string _SceneName)
        {
            PopUp.Instance.CloseAllPopUp();
            CurrentSubScene = _SceneName;
            while (ListCurrentSubScene.Count > 0)
            {
                CloseSubScene(ListCurrentSubScene[0]);
            }
            ListCurrentSubScene.Add(_SceneName);
            SceneHelper.SceneManager.OpenScene(_SceneName, LoadSceneMode.Additive);
        }

        public static void CloseAllSubScenes()
        {
            foreach (string sceneName in ListCurrentSubScene)
            {

                SceneHelper.SceneManager.CloseScene(sceneName);
            }
            ListCurrentSubScene.Clear();
        }

        public static void CloseSubScene(string _SceneName)
        {
            if (ListCurrentSubScene.Contains(_SceneName))
            {
                SceneHelper.SceneManager.CloseScene(_SceneName);
                ListCurrentSubScene.RemoveAt(ListCurrentSubScene.IndexOf(_SceneName));
            }
        }

    }

public interface ISceneManager
{
    void OpenScene(string sceneName, LoadSceneMode mode);
    void CloseScene(string sceneName);
}

public class SceneHelper
{
    static ISceneManager sceneManager;

    public static ISceneManager SceneManager
    {
        get
        {
            if (sceneManager == null)
            {
                throw new NullReferenceException<ISceneManager>(sceneManager);
            }
            return sceneManager;
        }
        set
        {
            sceneManager = value;
        }
    }
}
