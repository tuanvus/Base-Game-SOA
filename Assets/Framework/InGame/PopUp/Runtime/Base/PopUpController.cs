using System.Collections.Generic;
using UnityEngine;
using LTA.DesignPattern;
using System;
using LTA.Data;


    [DisallowMultipleComponent]
    public class PopUpController : MonoBehaviour
    {
        
        [SerializeField]
        private Transform canvas;

        void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
            if (canvas == null)
                canvas = GetComponent<Transform>();
        }

        public List<BasePopUp> listCurrentPopUp = new List<BasePopUp>();
        public T ShowPopUp<T>(string _PopUpName,Action endShow = null) where T : BasePopUp
        {
            T PopUp = Instantiate(AssetHelper.AssetManager.GetAsset<GameObject>("PopUp/" + _PopUpName), canvas).GetComponentInChildren<T>();
            PopUp.name = _PopUpName;
            PopUp.Show(endShow);
            listCurrentPopUp.Add(PopUp);
            return PopUp;
        }

        public T ShowLocalPopUp<T>(string _PopUpName, Action endShow = null) where T : BasePopUp
        {
            T PopUp = Instantiate(Resources.Load<GameObject>("PopUp/" + _PopUpName), canvas).GetComponentInChildren<T>();
            PopUp.name = _PopUpName;
            PopUp.Show(endShow);
            listCurrentPopUp.Add(PopUp);
            return PopUp;
        }

        public void ClosePopUp(BasePopUp _PopUp)
        {
            if (listCurrentPopUp.Contains(_PopUp))
            {
                Destroy(_PopUp.transform.parent.gameObject);
                listCurrentPopUp.Remove(_PopUp);
            }
            if (listCurrentPopUp.Count == 0)
                listCurrentPopUp.Clear();
        }

        public void CloseAllPopUp()
        {
            foreach (BasePopUp popUp in listCurrentPopUp)
            {
                Destroy(popUp.transform.parent.gameObject);
            }
            listCurrentPopUp.Clear();
        }
    }
    public class PopUp : SingletonMonoBehaviour<PopUpController>
    {

    }
