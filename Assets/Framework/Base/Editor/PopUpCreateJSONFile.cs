
using UnityEditor;
using UnityEngine;
using LTA.Base;
using System.IO;
using System.Text;
//using UnityEditor.Compilation;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class PopUpCreateJSONFile : EditorWindow
{
    public string typeJSON = "JSON";

    public string templatePath = "";
    string jsonName;
    void OnGUI()
    {
        EditorGUILayout.LabelField("Create JSON " + typeJSON, EditorStyles.wordWrappedLabel);
        GUILayout.Space(70);

        jsonName = EditorGUILayout.TextField("JSON Name", jsonName);

        this.Repaint();

        if (GUILayout.Button("Create JSON"))
        {
            CreateJSON(jsonName, templatePath);
            this.Close();
        }

        if (GUILayout.Button("Close"))
        {
            this.Close();
        }
    }

    void CreateJSON(string scriptName, string templatePath)
    {
        var guids = Selection.assetGUIDs;

        var path = AssetDatabase.GUIDToAssetPath(guids[0]);
        string fileName = scriptName;
        string filepath = path + "/" + fileName + ".json";
        int count = 0;
        while (File.Exists(filepath))
        {
            fileName = fileName + "-" + count;
            filepath = path + "/" + fileName + ".json";
            count++;
        }

        File.WriteAllText(filepath,Resources.Load<TextAsset>(templatePath).text);
        Debug.Log(filepath);
        AssetDatabase.ImportAsset(filepath);
        AssetDatabase.Refresh();
    }
}
