using UnityEngine;
using UnityEngine.EventSystems;
using LTA.Base;
using LTA.Effect;
using UnityEngine.UI;
public class ScrollViewController : MonoBehaviour,IEndDragHandler
{
    [SerializeField]
    EffectController[] items;
    
    public bool isScroll = false;
    
    public void Setup()
    {
        int numItems = ltaContent.transform.childCount;

        items = new EffectController[numItems];
        for (int i = 0; i < numItems; i++)
        {
            Transform item = ltaContent.transform.GetChild(i);
            items[i] = item.GetComponent<EffectController>();
            if (items[i] == null)
                items[i] = item.gameObject.AddComponent<EffectController>();
        }
        
        isScroll = true;
    }

    [SerializeField]
    TweenBehaviour ltaContent;
    private void Update()
    {
        UpdatePos();
    }

    EffectController selectItem;

    void UpdatePos()
    {
        if(!isScroll) return;
        selectItem = GetNearItem();
        if (selectItem == null)
            selectItem = items[0];
        foreach (EffectController item in items)
        {
            if (item == selectItem)
            {
                item.ShowEffect(TypeEffect.Select);
                continue;
            }
            item.HideEffect(TypeEffect.Select);
        }
    }

    EffectController GetNearItem()
    {
        EffectController result = null;
        float minDistance = 100000;
        foreach (EffectController item in items)
        {
            float distance = Vector3.Distance(transform.position, item.transform.position);
            if (result == null || distance < minDistance)
            {
                result = item;
                minDistance = distance;
            }
        }
        return result;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector3 movePos = transform.position + (ltaContent.transform.position - selectItem.transform.position);
        ltaContent.MoveUpdate(movePos);
    }
    
}
