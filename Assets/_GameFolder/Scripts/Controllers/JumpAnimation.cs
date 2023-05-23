using SplashyGame.Managers;
using SplashyGame.Platforms;
using UnityEngine;
using DG.Tweening;

namespace SplashyGame.Controllers
{
	public class JumpAnimation : MonoBehaviour
	{
		public Vector3 endPosition;

		public Ease jumpEase;
		public float jumpPower;
		public int jumpCount;
		public float duration;

		private float _totalJumpingTime;

		private Tween _jumpAnimation;

		private void OnEnable()
		{
			GameManager.OnGameStarted += OnGameStarted;
		}

		private void OnDisable()
		{
			GameManager.OnGameStarted -= OnGameStarted;
		}

		private void OnGameStarted()
		{
			Debug.Log($"Player jump animation script, on game started!");

			_totalJumpingTime = 0;
			StartJumpAnimation();
		}

		private void StartJumpAnimation()
		{
			_totalJumpingTime = 0;
			
			_jumpAnimation?.Kill();
			_jumpAnimation = null;
			_jumpAnimation = transform.DOLocalJump(endPosition, jumpPower, jumpCount, duration).SetEase(jumpEase);
			
			endPosition.z += 5f;
		}

		private void OnTriggerEnter(Collider other)
		{
			if (GameManager.Instance.GameState != GameState.Playing)
			{
				return;
			}
			
			if (other.gameObject.CompareTag("Platform"))
			{
				var platform = other.gameObject.GetComponent<Platform>();
				if (platform != null && !platform.IsCollidedPlayer)
				{
					StartJumpAnimation();
					
					platform.OnCollidedPlayer();
				}
			}

			if (other != null && other.gameObject.CompareTag("White"))
			{
				Debug.Log("White");
			}
			if (other!=null && other.gameObject.CompareTag("Diamondo"))
			{
				Destroy(other.gameObject);
			}
		}

		private void Update()
		{
			if (GameManager.Instance.GameState == GameState.Playing)
			{
				_totalJumpingTime += Time.deltaTime * 1f;
				if (_totalJumpingTime > duration + 0.1f)
				{
					GameManager.Instance.OnGameEnd();
				}
			}
		}
	}
}