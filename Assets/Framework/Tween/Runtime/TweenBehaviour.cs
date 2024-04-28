using UnityEngine;
using System;
namespace  LTA.Base
{
	public class TweenBehaviour : MonoBehaviour
    {
    public float timePerforme = 0.05f;
	private int id;
	public LeanTweenType _LeanTweenType = LeanTweenType.linear;

	public void MoveToPos(Vector3 posMoveTo,Action callBackComplete = null)
	{
		LeanTween.move(gameObject, posMoveTo, timePerforme).setEase(_LeanTweenType).setOnComplete(callBackComplete);
	}

	public void MoveToPosLocal(Vector3 posMoveTo,Action callBackComplete = null)
    {
        LeanTween.moveLocal(gameObject, posMoveTo, timePerforme).setEase(_LeanTweenType).setOnComplete(callBackComplete);
    }

    public void MoveUpdate(Vector3 posMoveTo, Action callBackComplete = null)
	{
		LeanTween.cancel (id);
		id = LeanTween.move(gameObject, posMoveTo, timePerforme).setEase(_LeanTweenType).setOnComplete(callBackComplete).id;
	}

    public void MoveUpdateLocal(Vector3 posMoveTo,Action callBackComplete = null)
        {
        LeanTween.cancel(id);
        id = LeanTween.moveLocal(gameObject, posMoveTo, timePerforme).setOnComplete(callBackComplete).setEase(_LeanTweenType).id;
    }
        protected void RotationUpdate(Vector3 rotationTo)
	{
		LeanTween.cancel (id);
		id = LeanTween.rotate(gameObject, rotationTo, timePerforme).setEase(_LeanTweenType).id;
	}

	public void UpdateValue(float firstValue , float lastValue, Action<float> updateValue = null,Action onComplete = null)
	{
       LeanTween.cancel(id);
       id = LeanTween.value(gameObject, updateValue,firstValue,lastValue, timePerforme).setEase(_LeanTweenType).setOnComplete(onComplete).id;
	}

	public void UpdateValue(Vector2 firstValue, Vector2 lastValue, Action<Vector2> updateValue = null, Action onComplete = null)
    {
        LeanTween.cancel(id);
        id = LeanTween.value(gameObject, updateValue, firstValue, lastValue, timePerforme).setEase(_LeanTweenType).setOnComplete(onComplete).id;
    }
	public void ScaleUpdate(Vector3 ScaleValue,Action onComplete = null)
	{
		LeanTween.cancel (id);
		id = LeanTween.scale (gameObject, ScaleValue, timePerforme).setEase(_LeanTweenType).setOnComplete(onComplete).id;
	}

	public void ScalePunch(Vector3 scaleTo,Action onComplete = null)
    {
	    LeanTween.cancel(id);
		id =  LeanTween.scale(gameObject, scaleTo, timePerforme).setEasePunch().setOnComplete(onComplete).id;
    }

	public void ScaleTo(Vector3 ScaleValue,Action callBackComplete = null)
	{
		LeanTween.scale (gameObject, ScaleValue, timePerforme).setEase(_LeanTweenType).setOnComplete(callBackComplete);
	}

		public void UpdateColor(Color firstColor, Color lastColor, Action<Color> updateValue = null, Action onComplete = null)
		{
			LeanTween.cancel(id);
			id = LeanTween.value(gameObject, updateValue, firstColor, lastColor, timePerforme).setEase(_LeanTweenType).setOnComplete(onComplete).id;
		}
	}
}

