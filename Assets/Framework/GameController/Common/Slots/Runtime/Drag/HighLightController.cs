using LTA.DesignPattern;
using UnityEngine;
public class HighLightController : MonoBehaviour
{
    public void SetHighLight(Vector3 position)
    {
        transform.position = position;
    }
}

public class HighLight : SingletonMonoBehaviour<HighLightController>
{

}
