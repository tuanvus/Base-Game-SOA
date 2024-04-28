using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ActionEditor : MonoBehaviour
{
    [MenuItem("Assets/LTA/Create/JSON/Action")]
    static void CreateMaterial()
    {
        PopUpCreateJSONFile window = ScriptableObject.CreateInstance<PopUpCreateJSONFile>();
        window.typeJSON = "Action";
        window.templatePath = "JSONTemplates/JSON Action";
        window.position = new Rect(Screen.width / 2, Screen.height / 2, 512, 256);
        window.ShowPopup();
    }
}
