using UnityEngine;

public class TypeStats
{
    public const string Static = "Static";
    public const string Dynamic = "Dynamic";
}

public class StatsInfo
{
    public string name;
    public float value;
    public string type = TypeStats.Static;

}

[System.Serializable]
public class StatsComponentInfo
{
    
}

[ComponentType(typeof(StatsComponent),typeof(StatsComponentInfo),"stats")]
public class StatsComponent : MonoBehaviour,ISetInfo
{
    StatsComponentInfo info;

    public object Info
    {
        set
        {
            info = (StatsComponentInfo)value;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
