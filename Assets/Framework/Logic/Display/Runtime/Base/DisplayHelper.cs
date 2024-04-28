using LTA.Data;
using UnityEngine;
using LTA.Error;
public class DisplayHelper
{
    public static GameObject CreatePrefab(string path)
    {
        GameObject prefab = AssetHelper.AssetManager.GetAsset<GameObject>(path);
        if (prefab == null)
        {
            throw new NullReferenceException<GameObject>(prefab);
        }
        return GameObject.Instantiate(prefab);
    }

    public static GameObject CreateSpriteRender(string path)
    {
        GameObject instance = new GameObject();
        //instance.transform.SetParent(transform);
        SpriteRenderer spriteRenderer = instance.AddComponent<SpriteRenderer>();
        Sprite sprite = AssetHelper.AssetManager.GetAsset<Sprite>(path);
        if (sprite == null) {
            throw new NullReferenceException<Sprite>(sprite);
        }
        spriteRenderer.sprite = sprite;
        return instance;
    }
}
