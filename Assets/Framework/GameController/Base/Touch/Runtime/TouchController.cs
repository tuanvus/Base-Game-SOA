using System;
using UnityEngine;
namespace LTA.Toucher
{
    public enum TypeTouchObject
    {
        Object2D,
        Object3D
    }

    public class TouchController : MonoBehaviour
    {
        private IOnTapDown[] onTapDowns;

        private IOnTapUp[] onTapUps;

        private IOnTapHold[] onTapHolds;
        [SerializeField]
        TypeTouchObject typeTouch;
        [SerializeField]
        LayerMask layer;
        public Vector3 GetTouchPosition()
        {
#if UNITY_EDITOR || UNITY_WEBGL || UNITY_STANDALONE
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
#else
            return Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
#endif
        }
        GameObject GetHitGameObject()
        {
            Vector3 postionTouch = GetTouchPosition();
            switch (typeTouch)
            {
                case TypeTouchObject.Object2D:
                    RaycastHit2D hit = Physics2D.Raycast
                    (
                        new Vector2
                        (
                            postionTouch.x,
                            postionTouch.y
                        )
                    , Vector2.zero, Mathf.Infinity,layer.value
                    );
                    if (hit.transform != null)
                    {
                        return hit.transform.gameObject;
                    }
                    return null;
                case TypeTouchObject.Object3D:
                    RaycastHit hit3d;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    Physics.Raycast(ray, out hit3d, Mathf.Infinity, layer.value);
                    if (hit3d.transform != null)
                    {
                        return hit3d.transform.gameObject;
                    }
                    return null;
            }
            return null;
        }

        bool isDoing = false;
        [SerializeField]
        GameObject hitgameObject;
        private void Update()
        {
            try
            {
#if !UNITY_EDITOR && !UNITY_WEBGL && !UNITY_STANDALONE
             if (Input.touchCount == 0) return;
#endif
                if (isDoing) return;
                isDoing = true;
#if UNITY_EDITOR || UNITY_WEBGL|| UNITY_STANDALONE
                if (Input.GetMouseButton(0)
#else
            if (Input.GetTouch(0).phase == TouchPhase.Moved
#endif
            && onTapHolds != null)
                {
                    foreach (IOnTapHold onTapHold in onTapHolds)
                    {
                        onTapHold.OnTapHold(layer);
                    }
                }
#if UNITY_EDITOR || UNITY_WEBGL|| UNITY_STANDALONE
                if (Input.GetMouseButtonDown(0)
#else
            if (Input.GetTouch(0).phase == TouchPhase.Began
#endif
            && hitgameObject == null)
                {
                   
                    hitgameObject = GetHitGameObject();
                    if (hitgameObject != null)
                    {
                        onTapDowns = hitgameObject.GetComponents<IOnTapDown>();
                        if (onTapDowns != null)
                        {
                            foreach (IOnTapDown onTapDown in onTapDowns)
                                onTapDown.OnTapDown(layer);
                        }
                        onTapDowns = null;
                        onTapHolds = hitgameObject.GetComponents<IOnTapHold>();
                        onTapUps = hitgameObject.GetComponents<IOnTapUp>();
                    }

                }
#if UNITY_EDITOR || UNITY_WEBGL || UNITY_STANDALONE
                if (Input.GetMouseButtonUp(0)
#else
            if (Input.GetTouch(0).phase == TouchPhase.Ended
#endif
                && hitgameObject != null)
                {
                    foreach (IOnTapUp onTapUp in onTapUps)
                        if (onTapUp != null) onTapUp.OnTapUp(layer);
                    onTapHolds = null;
                    onTapUps = null;
                    hitgameObject = null;
                }
                isDoing = false;
            }
            catch (Exception ex)
            {
                onTapHolds = null;
                onTapUps = null;
                hitgameObject = null;
                isDoing = false;
            }
        }
    }
}


