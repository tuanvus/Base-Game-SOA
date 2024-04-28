using LTA.Display;
using UnityEngine;
using LTA.Data;




[ComponentType(typeof(DisplaySpriteRender), typeof(LTA.Display.DisplayInfo),"sprite")]
public class DisplaySpriteRender : BaseDisplayController<LTA.Display.DisplayInfo>
{
    protected GameObject instance;

    protected override GameObject Instance
    {
        get
        {
            if (instance == null)
            {
                instance = DisplayHelper.CreateSpriteRender(info.path);
                instance.transform.SetParent(transform);
            }
            return instance;
        }
    }
}
