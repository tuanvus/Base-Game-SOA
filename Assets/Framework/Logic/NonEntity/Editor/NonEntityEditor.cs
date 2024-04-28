using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NonEntityEditor
{
    [MenuItem("Assets/LTA/Create/NonEntity")]
    static void CreateMaterial()
    {
        PopUpCreateLTAScript window = ScriptableObject.CreateInstance<PopUpCreateLTAScript>();
        window.typeScript = "NonEntity";
        window.templatePath = "ScriptTemplates/C# Script-NonEntityScript";
        window.position = new Rect(Screen.width / 2, Screen.height / 2, 512, 256);
        window.ShowPopup();
    }
}
