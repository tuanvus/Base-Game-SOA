using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ActionEditor : MonoBehaviour
{
    [MenuItem("Assets/LTA/Create/JSON/Target")]
    static void CreateMaterial()
    {
        PopUpCreateJSONFile window = ScriptableObject.CreateInstance<PopUpCreateJSONFile>();
        window.typeJSON = "Target";
        window.templatePath = "JSONTemplates/JSON Target";
        window.position = new Rect(Screen.width / 2, Screen.height / 2, 512, 256);
        window.ShowPopup();
    }

    [MenuItem("Assets/LTA/Create/Target")]
    static void CreateTargetScript()
    {
        PopUpCreateLTAScript window = ScriptableObject.CreateInstance<PopUpCreateLTAScript>();
        window.typeScript = "Target";
        window.templatePath = "ScriptTemplates/C# Script-Target";
        window.position = new Rect(Screen.width / 2, Screen.height / 2, 512, 256);
        window.ShowPopup();
    }

    [MenuItem("Assets/LTA/Create/BestTarget")]
    static void CreateBestTargetScript()
    {
        PopUpCreateLTAScript window = ScriptableObject.CreateInstance<PopUpCreateLTAScript>();
        window.typeScript = "Best Target";
        window.templatePath = "ScriptTemplates/C# Script-BestTarget";
        window.position = new Rect(Screen.width / 2, Screen.height / 2, 512, 256);
        window.ShowPopup();
    }
}
