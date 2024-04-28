
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LTA.Data
{

    public class DataInfo
    {
        public string name;
        public object data;

        public DataInfo(string name,object data)
        {
            this.name = name;
            this.data = data;
        }
    }

    public abstract class MutilData<T> : MapData<string,T> where T : ILoadData,IData,new()
    {
        public override IEnumerator GetEnumerator()
        {
            return m_Data.Values.GetEnumerator();
        }

        public void LoadData(DataInfo[] dataInfos,Action<T> onEditData = null)
        {
            Dictionary<string, T> dic_key_data = new Dictionary<string, T>();
            foreach (DataInfo dataInfo in dataInfos)
            {
                try
                {
                    T loadData = new T();
                    //if (!ValidateData(loadData)) continue;
                    loadData.LoadData(dataInfo);
                    dic_key_data.Add(dataInfo.name,loadData);
                    
                }
                catch (Exception ex)
                {
                    Debug.LogError(ex.Message);
                    continue;
                }
            }
           m_Data = dic_key_data;
        }

        //protected abstract bool ValidateData(T data);
    }

    public class MapData<K,V> : EnumerableData<Dictionary<K,V>>,IMapData<K,V>
    {
        public override Dictionary<K, V> m_Data 
        { 
          get {
                if (data == null) data = new Dictionary<K,V>();
                return data;
          }
          set => base.m_Data = value; 
        }

        public virtual V this[K key]
        {
            get
            {
                if (!m_Data.ContainsKey(key)) throw new MapDataExpection<K>(this, key, "key is not found");
                return m_Data[key];
            }
        }

        public ICollection<K> Keys => m_Data.Keys;

        public ICollection<V> Values => m_Data.Values;
    }

    public class ArrayData<T> : EnumerableData<T[]>
    {
        public T this[int index]
        {
            get
            {
                if (index >= Count) throw new ArrayDataExpection(this, index, "index is out of range");
                return m_Data[index];
            }
        }
    }

    public interface IMapData<K,V>
    {
        V this[K key] { get; }
        ICollection<K> Keys { get;}
        ICollection<V> Values { get; }
    }

    public interface IData<T>
    {
        T m_Data { get; }
    }

    public abstract class EnumerableData<T> : Data<T>, IData<T>,IEnumerable where T : ICollection
    {
        public int Count
        {
            get
            {
                return m_Data.Count;
            }
        }

        public virtual IEnumerator GetEnumerator()
        {
            return m_Data.GetEnumerator();        
        }
    }

    public abstract class Data<T> : Data, IData<T>
    {
        
        protected T data;
        public virtual T m_Data
        {
            get
            {
                if (data == null) throw new DataExpection(this, "data is null");
                return data;
            }
            set
            {
                data = value;
            }
        }
    }

    public class Data : IData
    {
        
        string name;

        public string Name
        {
            get
            {
                return name;
            }
            protected set
            {
                name = value;
            }
        }
        
    }
    
    public interface IDataFile
    {
        string Extension { get; }
    }

    public interface IData
    {
        string Name { get; }
    }

    public interface ILoadData
    {
        void LoadData(DataInfo dataInfo);
    }
}
