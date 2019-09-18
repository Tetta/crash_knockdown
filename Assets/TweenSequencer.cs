 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TweenSequencer : MonoBehaviour {


	[Header("Цель")]
	public Transform target;

	[Header("Перемещение")]
	public bool doMove;
	public bool isRelativeMove;
	public bool doLocalMove;
	public Vector3 moveVector;
	public float moveDuration;

	[Header("Поворот")]
	public bool doRotation;
	public bool isRelativeRotation;
	public Vector3 rotateVector;
	public float rotateDuration;

	[Header("Размер")]
	public bool doScale;
	public bool isRelativeScale;
	public Vector3 ScaleVector;
	public float scaleDuration;

	[Header("Циклы")]
	public bool loopSequence;
	public LoopType looptype;
	public int loopCount=-1;// вечный луп


	[Header("Задержки")]
	public float beforeDelay;
	public float afterDelay;

	public float afterScaleDelay;

	public float afterRotateDelay;

	public float afterMoveDelay;

	[Header("Активация")]
	public bool playOnAwake;
	public bool playOnCollide;
	public bool clearOnCallback;

	[Header("Опции")]
	public Ease sequenceEase;
	public bool advancedEaseMode;
	public float easeAmplitude;
	public float easePeriod;
	public bool joinAll;



	 Sequence sequence;
	


	// Use this for initialization
	public void Initialize()
	{
	
		sequence = DOTween.Sequence();
		if(loopSequence)sequence.SetLoops(loopCount, looptype);

		

		if (joinAll)
		{
			
			if (doScale)
			{
				sequence.Join(target.DOScale(ScaleVector, scaleDuration)).SetRelative(isRelativeScale);
			}
			if (doMove)
			{
				sequence.Join(target.DOMove(moveVector, moveDuration)).SetRelative(isRelativeMove);
			}
			if (doRotation)
			{
				sequence.Join(target.DORotate(rotateVector, rotateDuration)).SetRelative(isRelativeRotation);
			}
		}
		else
		{
			if (doScale)
			{
				sequence.Append(target.DOScale(ScaleVector, scaleDuration)).SetRelative(isRelativeScale).SetDelay(afterScaleDelay);
			}
			if (doMove)
			{
				if (doLocalMove)
				{
					sequence.Append(target.DOLocalMove(moveVector, moveDuration	)).SetRelative(isRelativeMove).SetDelay(afterMoveDelay);
				}
				else
				{
					sequence.Append(target.DOMove(moveVector, moveDuration)).SetRelative(isRelativeMove).SetDelay(afterMoveDelay);
				}
			}
			if (doRotation)
			{
				sequence.Append(target.DORotate(rotateVector, rotateDuration)).SetRelative(isRelativeRotation).SetDelay(afterRotateDelay);
			}
		}
		if (beforeDelay>0f)
		{
			sequence.PrependInterval(beforeDelay);
		}
		if (afterDelay>0f)
		{
			sequence.AppendInterval(afterDelay);
		}
		if (advancedEaseMode)
		{
			sequence.SetEase(sequenceEase, easeAmplitude, easePeriod);
		}
		else {
			sequence.SetEase(sequenceEase);
		}
	}
	private void OnCollisionEnter(Collision collision)
	{
		if (playOnCollide)
		{
			if (collision.transform.tag == "Bullet")
			{
				Play();
			}
		}
	}
	public void callback() {
		sequence.Kill();
		Initialize();
	}
	public void Play() {
		sequence.PlayForward();
		if(clearOnCallback)sequence.onComplete = callback;

	
		
	}
	void Awake() {
		Initialize();
		if (playOnAwake) Play();
	}
}
