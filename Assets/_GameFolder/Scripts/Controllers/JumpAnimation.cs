using UnityEngine;
using DG.Tweening;
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
			if (collision.gameObject.CompareTag("Platform"))
			{
				var platform = collision.gameObject.GetComponent<Platform>();
				if (platform != null && !platform.IsCollidedPlayer)
				{
					StartJumpAnimation();
					endPosition.z += 5f;
					
					platform.OnCollidedPlayer();
				}
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