using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using SplashyGame.Platforms;

namespace SplashyGame.Controllers
{
	public class JumpAnimation : MonoBehaviour
	{
		public Vector3 endPosition;

		public Ease jumpEase;
		public float jumpPower;
		public int jumpCount;
		public float duration;

		private Tween _jumpAnimation;

		private void StartJumpAnimation()
		{
			_jumpAnimation = transform.DOLocalJump(endPosition, jumpPower, jumpCount, duration).SetEase(jumpEase);
		}

		private void OnCollisionEnter(Collision collision)
		{
			if (collision != null && collision.gameObject.CompareTag("Ground"))
			{
				StartJumpAnimation();
				endPosition.z += 5f;

				collision.transform.DOMoveY(1.2f, 0.1f);
			}
			else
			{
				_jumpAnimation.Kill();
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other != null && other.gameObject.CompareTag("White"))
			{
				StartJumpAnimation();
				endPosition.z += 5f;
				
				
				Debug.Log("White");
			}
		}
	}
}