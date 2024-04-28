
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using LTA.Effect;
using System;
using LTA.Base;
public class JoyStickContoller : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    static Dictionary<string, JoyStickContoller> dic_JoyStick = new Dictionary<string, JoyStickContoller>();

    public static JoyStickContoller GetJoyStick(string type)
    {
        if (dic_JoyStick.ContainsKey(type))
        {
            return dic_JoyStick[type];
        }
        throw new Exception("JoyStick with Type " + type + "isn't Exist");
    }

    private void Awake()
    {
        if (dic_JoyStick.ContainsKey(typeJoyStick))
        {
            Debug.LogError("JoyStick with Type " + typeJoyStick + "is Exist");
            Destroy(this.gameObject);
            return;
        }
        dic_JoyStick.Add(typeJoyStick, this);
    }
    private void OnDestroy()
    {
        dic_JoyStick.Remove(typeJoyStick);
    }

    [SerializeField]
    string typeJoyStick;

    protected Vector3 direction;

    protected Vector3 posJoyStick;

    [SerializeField]
    protected Transform joyStick;

    [SerializeField]
    protected Transform BgJoyStick;

    Vector3 OriginalPos;

    EffectController effectJoyStick;

    EffectController effectBgJoyStick;

    protected float maxLength = 70f;

    public string TypeJoyStick
    {
        get
        {
            return typeJoyStick;
        }
    }

    public Vector3 Direction
    {
        get
        {
            return direction;
        }
    }

    public Vector3 PosJoyStick
    {
        get
        {
            return posJoyStick;
        }
    }

    List<IOnJoyStickBeginDrag> onJoyStickBeginDrags = new List<IOnJoyStickBeginDrag>();

    List<IOnJoyStickDrag> onJoyStickDrags = new List<IOnJoyStickDrag>();

    List<IOnJoyStickEndDrag> onJoyStickEndDrags = new List<IOnJoyStickEndDrag>();

    public void AddOnJoyStickBeginDrag(IOnJoyStickBeginDrag onJoyStickBeginDrag)
    {
        if (onJoyStickBeginDrags.Contains(onJoyStickBeginDrag)) return;
        onJoyStickBeginDrags.Add(onJoyStickBeginDrag);
    }

    public void AddOnJoyStickDrag(IOnJoyStickDrag onJoyStickDrag)
    {
        if (onJoyStickDrags.Contains(onJoyStickDrag)) return;
        onJoyStickDrags.Add(onJoyStickDrag);
    }

    public void AddOnJoyStickEndDrag(IOnJoyStickEndDrag onJoyStickEndDrag)
    {
        if (onJoyStickEndDrags.Contains(onJoyStickEndDrag)) return;
        onJoyStickEndDrags.Add(onJoyStickEndDrag);
    }

    public void RemoveOnJoyStickBeginDrag(IOnJoyStickBeginDrag onJoyStickBeginDrag)
    {
        if (!onJoyStickBeginDrags.Contains(onJoyStickBeginDrag)) return;
        onJoyStickBeginDrags.Remove(onJoyStickBeginDrag);
    }

    public void RemoveOnJoyStickDrag(IOnJoyStickDrag onJoyStickDrag)
    {
        if (!onJoyStickDrags.Contains(onJoyStickDrag)) return;
        onJoyStickDrags.Remove(onJoyStickDrag);
    }

    public void RemoveOnJoyStickEndDrag(IOnJoyStickEndDrag onJoyStickEndDrag)
    {
        if (!onJoyStickEndDrags.Contains(onJoyStickEndDrag)) return;
        onJoyStickEndDrags.Remove(onJoyStickEndDrag);
    }

    public float MaxLegnth
    {
        get
        {
            return maxLength;
        }
    }

    protected virtual void Start()
    {
        OriginalPos = BgJoyStick.transform.position;
        maxLength = BgJoyStick.GetComponent<RectTransform>().sizeDelta.x <= 0 ? 70 : (BgJoyStick.GetComponent<RectTransform>().sizeDelta.x / 2);
        effectBgJoyStick = BgJoyStick.GetComponent<EffectController>();
        effectJoyStick = joyStick.GetComponent<EffectController>();
        if (effectBgJoyStick != null)
            effectBgJoyStick.HideEffect(TypeEffect.Drag);
        if (effectJoyStick != null)
            effectJoyStick.HideEffect(TypeEffect.Drag);

    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        // BgJoyStick.position = eventData.position;
        // direction = Vector3.zero;
        // if (effectBgJoyStick != null)
        //     effectBgJoyStick.ShowEffect(TypeEffect.Drag);
        // if (effectJoyStick != null)
        //     effectJoyStick.ShowEffect(TypeEffect.Drag);
        // foreach (IOnJoyStickBeginDrag onJoyStickBeginDrag in onJoyStickBeginDrags)
        // {
        //     onJoyStickBeginDrag.OnJoyStickBeginDrag(this);
        // }
    }

    public void OnDrag(PointerEventData eventData)
    {
        MoveJoyStick(eventData.position);
        foreach (IOnJoyStickDrag onJoyStickDrag in onJoyStickDrags)
        {
            onJoyStickDrag.OnJoyStickDrag(this);
        }
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        joyStick.transform.localPosition = Vector3.zero;
        direction = Vector3.zero;
        BgJoyStick.position = OriginalPos;
        posJoyStick = joyStick.localPosition;
        if (effectBgJoyStick != null)
            effectBgJoyStick.HideEffect(TypeEffect.Drag);
        if (effectJoyStick != null)
            effectJoyStick.HideEffect(TypeEffect.Drag);
        foreach (IOnJoyStickEndDrag onJoyStickEndDrag in onJoyStickEndDrags)
        {
            onJoyStickEndDrag.OnJoyStickEndDrag(this);
        }

    }

    protected virtual void MoveJoyStick(Vector3 touchPos)
    {
        Vector2 offset = touchPos - BgJoyStick.position;
        Vector3 realdirection = Vector2.ClampMagnitude(offset, maxLength);

        direction = realdirection.normalized;
        joyStick.position = new Vector3(BgJoyStick.position.x + realdirection.x, BgJoyStick.position.y + realdirection.y, joyStick.position.z);
        posJoyStick = joyStick.localPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        BgJoyStick.position = eventData.position;
        direction = Vector3.zero;
        if (effectBgJoyStick != null)
            effectBgJoyStick.ShowEffect(TypeEffect.Drag);
        if (effectJoyStick != null)
            effectJoyStick.ShowEffect(TypeEffect.Drag);
        foreach (IOnJoyStickBeginDrag onJoyStickBeginDrag in onJoyStickBeginDrags)
        {
            onJoyStickBeginDrag.OnJoyStickBeginDrag(this);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        BgJoyStick.position = OriginalPos;
        direction = Vector3.zero;
    }
}
