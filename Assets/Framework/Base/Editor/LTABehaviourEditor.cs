
using UnityEditor;
using UnityEngine;

public class LTABaseEditor
{
    [MenuItem("Assets/LTA/Create/LTABehaviour")]
    static void CreateMaterial()
    {
        PopUpCreateLTAScript window = ScriptableObject.CreateInstance<PopUpCreateLTAScript>();
        window.position = new Rect(Screen.width / 2, Screen.height / 2, 512, 256);
        window.ShowPopup();
    }
    
}
