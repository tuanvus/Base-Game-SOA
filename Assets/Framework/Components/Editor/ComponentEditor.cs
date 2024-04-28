
using System.IO;
using UnityEditor;
using UnityEngine;
using SimpleJSON;
using UnityEditor.Compilation;
using System.Reflection;
using UnityAssembly = UnityEditor.Compilation.Assembly;
using Assembly = System.Reflection.Assembly;
using System;
using System.Collections.Generic;
using System.Text;



#if (UNITY_EDITOR)
public class ComponentEditor
{

    [MenuItem("Assets/LTA/Create/Components")]
    static void CreateMaterial()
    {
        PopUpCreateLTAScript window = ScriptableObject.CreateInstance<PopUpCreateLTAScript>();
        window.typeScript = "Components";
        window.templatePath = "ScriptTemplates/C# Script-ComponentsScript";
        window.position = new Rect(Screen.width / 2, Screen.height / 2, 512, 256);
        window.ShowPopup();
    }

    //public static JSONObject components;
    [MenuItem("LTA/Config/Create Components")]
    public static void PrintAssemblyNames()
    {
        UnityAssembly[] playerAssemblies =
            CompilationPipeline.GetAssemblies(AssembliesType.Player);
        Dictionary<string, JSONObject> dic_DataName_Data = new Dictionary<string, JSONObject>();
        foreach (UnityAssembly unityAssembly in playerAssemblies)
        {
                Assembly assembly = Assembly.Load(unityAssembly.name);
                Type[] types = assembly.GetTypes();
                foreach(Type type in types)
                {
                    IEnumerable<ComponentTypeAttribute> componentTypes = type.GetCustomAttributes<ComponentTypeAttribute>();
                    foreach(ComponentTypeAttribute componentType in componentTypes)
                    {
                        if (!dic_DataName_Data.ContainsKey(componentType.componentType))
                        {
                            dic_DataName_Data.Add(componentType.componentType, new JSONObject());
                        }
                        JSONObject data = dic_DataName_Data[componentType.componentType];
                        data.Add(componentType.identify,componentType.ToJson());
                    }
                }    
        }
        foreach(KeyValuePair<string,JSONObject> name_Data in dic_DataName_Data)
        {
            JSONObject json = new JSONObject();
            json.Add("data", name_Data.Value);
            SaveData(name_Data.Key, json.ToString(4));
        }
        
    }
    static readonly string pathConfig = Path.Combine(Application.dataPath + "/Resources/Data/");
    public static string FileNameToPath(string fileName)
    {
        if (!Directory.Exists(pathConfig))
        {
            Directory.CreateDirectory(pathConfig);
        }
        string path = Path.Combine(pathConfig + fileName);
        Debug.Log("path : " + path);
        return path;
    }

    public static void SaveData(string fileName, string data)
    {
        var path = FileNameToPath(fileName + ".json");
        try
        {
            File.WriteAllText(path, data, Encoding.UTF8);
            AssetDatabase.Refresh();

        }
        catch (Exception ex)
        {
            Debug.LogError("[SaveData] Exception: " + fileName + " " + ex.Message);
        }
        finally
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
    }
}


#endif
