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
			GameManager.OnGameEnded += OnGameEnded;
			GameManager.OnGameReseted += OnGameReseted;
		}

		private void OnDisable()
		{
			GameManager.OnGameStarted -= OnGameStarted;
			GameManager.OnGameEnded -= OnGameEnded;
			GameManager.OnGameReseted -= OnGameReseted;
		}

		private void OnGameStarted()
		{
			Debug.Log($"Player jump animation script, on game started!");

			_totalJumpingTime = 0;

			StartJumpAnimation();
		}

		private void OnGameEnded(bool isSuccess)
		{

			GameManager.Instance.GameState = GameState.End;
			endPosition.z = 5f;
			OnGameReseted();
		}
		public void OnGameReseted()
		{
			Debug.Log("Enes");
			transform.position = new Vector3(0f, 0.5f, 0f);

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
				UIManager.Instance.levelPassedText.gameObject.SetActive(true);
				UIManager.Instance.gameScoreText.gameObject.SetActive(false);

				GameManager.Instance.OnGameEnd(true);
			}
		}
		public void AnimationTime()
		{
			if (GameManager.Instance.GameState == GameState.Playing)
			{
				_totalJumpingTime += Time.deltaTime * 1f;
				if (_totalJumpingTime > duration + 0.1f)
				{

					GameManager.Instance.OnGameEnd(false);
				}
			}
		}
		private void Update()
		{
			AnimationTime();
		}
	}
}