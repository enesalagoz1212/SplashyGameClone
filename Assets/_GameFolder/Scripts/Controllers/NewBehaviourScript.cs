using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NewBehaviourScript : MonoBehaviour
{
	public Vector3 endPosition;
	public float jumpPower;
	public int jumpCount;
	public float duration;

	private Tween _jumpAnimation;

	private void Awake()
	{

	}
	private void Start()
	{
		StartJumpAnimation();
	}

	private void Update()
	{

	}
	private void StartJumpAnimation()
	{                                                                                         //iptal
		_jumpAnimation = transform.DOLocalJump(endPosition, jumpPower, jumpCount, duration).OnComplete(() =>
		 {
			 endPosition.z += 5;
			 StartJumpAnimation();
		 }
		);
	}
	private void StopJumpAnimation()
	{
		Debug.Log("A");
		if (_jumpAnimation != null)
		{
			Debug.Log("B");
			_jumpAnimation.Kill();
		}
	}
	private void OnCollisionEnter(Collision collision)
	{
		if (true)
		{

		}
	}

}
