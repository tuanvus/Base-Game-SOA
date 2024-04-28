
using UnityEditor;
using UnityEngine;
using LTA.Base;
using System.IO;
using System.Text;
using UnityEditor.Compilation;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class PopUpCreateLTAScript : EditorWindow
{
    public string typeScript = "LTABehaviour";

    public string templatePath = "ScriptTemplates/C# Script-LTABehaviourScript";
    string scriptName;
    void OnGUI()
    {
        EditorGUILayout.LabelField("Create Script " + typeScript, EditorStyles.wordWrappedLabel);
        GUILayout.Space(70);

        scriptName = EditorGUILayout.TextField("Script Name",scriptName);
        this.Repaint();
        if (GUILayout.Button("Create Script"))
        {
            CreateScript(scriptName, templatePath);
            this.Close();
        }
        if (GUILayout.Button("Create Script With Info"))
        {
            CreateScript(scriptName, templatePath + "WithInfo");
            this.Close();
        }

        if (GUILayout.Button("Close"))
        {
            this.Close();
        }
    }
    void CreateScript(string scriptName,string templatePath)
    {
        var guids = Selection.assetGUIDs;

        var path = AssetDatabase.GUIDToAssetPath(guids[0]);
        string fileName = scriptName;
        string filepath = path + "/" + fileName + ".cs";
        int count = 0;
        while (File.Exists(filepath))
        {
            fileName = fileName+ "-" + count;
            filepath = path + "/" + fileName + ".cs";
            count++;
        }
         
        File.WriteAllText(filepath,LTAHelper.PreprocessScriptAssetTemplate(filepath, Resources.Load<TextAsset>(templatePath).text));
        Debug.Log(filepath);
        AssetDatabase.ImportAsset(filepath);
        AssetDatabase.Refresh();
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
