using UnityEngine;
using System;
using LTA.DesignPattern;


    [DisallowMultipleComponent]
    public abstract class LoadingController : MonoBehaviour
    {
        
        public LoadingProcessController loadingProcessController;

        [SerializeField]
        GameObject normalLoading;


        void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        protected virtual void Init()
        {
            ShowLoadingProcess(1f,EndFirstLoading);
        }

        protected abstract void EndFirstLoading();

        public void ShowLoadingProcess(float percent,Action EndLoading = null)
        {
            if (EndLoading != null)
            {
                loadingProcessController.Reset();
                loadingProcessController.EndLoading = EndLoading;
                loadingProcessController.gameObject.SetActive(true);
            }
            loadingProcessController.SetPercent(percent);
        }

        public void ExitLoading()
        {
            loadingProcessController.EndLoading = null;
            loadingProcessController.gameObject.SetActive(false);
            normalLoading.SetActive(false);
        }

        public void ShowNormalLoading()
        {
            normalLoading.SetActive(true);
        }
    }
    public class Loading : SingletonMonoBehaviour<LoadingController>
    {

    }

