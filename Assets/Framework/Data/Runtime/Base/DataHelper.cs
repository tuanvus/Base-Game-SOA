using LTA.Error;
using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
namespace LTA.Data
{
    public class DataHelper
    {
        protected const string WARNING_DATA_MESSAGE = "Warning at {0} : {1} ";

        static IMapData<string, DataInfo[]> dataManager;
        public static IMapData<string, DataInfo[]> DataManager
        {
            get
            {
                if (dataManager == null) throw new NullReferenceException<IMapData<string, DataInfo[]>>(dataManager);
                return dataManager;
            }
            set
            {
                dataManager = value;
            }
        }

        public static string GetDataName(IData data)
        {
            if (!(data is IDataFile)) return data.Name;
            return data.Name + "." + ((IDataFile)data).Extension;
        }
        public static string GetDataName(IData data,int index)
        {
            return GetDataName(data) + " in index = " + index;
        }

        public static string GetDataName<K>(IData data, K key)
        {
            return GetDataName(data) + " in key = " + key.ToString();
        }

        public static void LogWarning(IData data,string message, string dataName = null)
        {
            if (dataName == null) dataName = DataHelper.GetDataName(data);
            Debug.LogWarning(String.Format(WARNING_DATA_MESSAGE, dataName, message));
        }

    }

    public interface IAssetManager
    {
        T GetAsset<T>(string path) where T : UnityEngine.Object;
        T[] GetAllAsset<T>(string path) where T : UnityEngine.Object;
        ResourceRequest GetAssetAsync<T>(string path) where T : UnityEngine.Object;
    }

    public class AssetHelper
    {
        static IAssetManager assetManager;

        public static IAssetManager AssetManager
        {
            get
            {
                if (assetManager == null)
                {
                    throw new NullReferenceException<IAssetManager>(assetManager);
                }
                return assetManager;
            }
            set
            {
                assetManager = value;
            }
        }
    }
}