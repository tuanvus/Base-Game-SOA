using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ConditionEditor
{
    [MenuItem("Assets/LTA/Create/Handle")]
    static void CreateMaterial()
    {
        PopUpCreateLTAScript window = ScriptableObject.CreateInstance<PopUpCreateLTAScript>();
        window.typeScript = "Handle";
        window.templatePath = "ScriptTemplates/C# Script-Handle";
        window.position = new Rect(Screen.width / 2, Screen.height / 2, 512, 256);
        window.ShowPopup();
    }
}
