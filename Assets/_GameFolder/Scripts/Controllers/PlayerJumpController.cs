using System;
using SplashyGame.Managers;
using SplashyGame.Platforms;
using UnityEngine;
using DG.Tweening;

namespace SplashyGame.Controllers
{
    public class PlayerJumpController : MonoBehaviour
    {
		public Vector3 endPosition;

		public Ease jumpEase;
		public float jumpPower;
		public int jumpCount;
		public float duration;

		private float _totalJumpingTime;

		private Vector3 _initialEndPosition;

		private Tween _jumpAnimation;
		private bool _isEndSuccess;

		private void Awake()
		{
			_initialEndPosition = endPosition;
		}

		private void OnEnable()
		{
			GameManager.OnGameStarted += OnGameStarted;
			GameManager.OnGameEnded += OnGameEnded;
			GameManager.OnGameReset += OnGameResetAction;
		}

		private void OnDisable()
		{
			GameManager.OnGameStarted -= OnGameStarted;
			GameManager.OnGameEnded -= OnGameEnded;
			GameManager.OnGameReset -= OnGameResetAction;
		}

		private void OnGameStarted()
		{
			Debug.Log($"Player jump animation script, on game started!");

			_totalJumpingTime = 0;

			StartJumpAnimation();
		}

		private void OnGameEnded(bool isSuccess)
		{
			_isEndSuccess = isSuccess;
			if (!_isEndSuccess)
			{
				GameManager.Instance.GameState = GameState.End;
				endPosition.z = 5f;
			}
		}

		private void OnGameResetAction()
		{
			transform.localPosition = Vector3.zero;

			endPosition = _initialEndPosition;
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
				GameManager.Instance.IncreaseGameScore(1);


				var platform = other.gameObject.GetComponent<Platform>();
				if (platform != null && !platform.IsCollidedPlayer)
				{
					StartJumpAnimation();

					Vector3 collisionPosition = transform.position;
					platform.OnCollidedPlayer(collisionPosition);
				}
			}

			if (other != null && other.gameObject.CompareTag("White"))
			{

				Debug.Log("White");
			}

			if (other.CompareTag("Diamondo"))
			{
				// Increased diamond
				GameManager.Instance.IncreaseDiamondScore(1);
				Destroy(other.gameObject);
			}

			if (other.gameObject.CompareTag("Finish"))
			{
				GameManager.Instance.OnGameEnd(true);
			}
		}
		
		private void Update()
		{
			switch (GameManager.Instance.GameState)
			{
				case GameState.Start:
					break;
				
				case GameState.Playing:
					AnimationTime();
					break;
				
				case GameState.End:
					break;
				
				case GameState.Reset:
					break;
				
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void AnimationTime()
		{
			_totalJumpingTime += Time.deltaTime * 1f;
			if (_totalJumpingTime > duration + 0.1f)
			{
				GameManager.Instance.OnGameEnd(false);
			}
		}
    }
}
