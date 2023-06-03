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

		public float fadeDuration = 0.5f;
	

		private void Awake()
		{

		}
		private void OnEnable()
		{
			GameManager.OnGameStarted += OnGameStarted;
			GameManager.OnGameEnded += OnGameEnded;
		}

		private void OnDisable()
		{
			GameManager.OnGameStarted -= OnGameStarted;
			GameManager.OnGameEnded -= OnGameEnded;
		}

		private void OnGameStarted()
		{
			Debug.Log($"Player jump animation script, on game started!");



			_totalJumpingTime = 0;

			StartJumpAnimation();
		}
		private void OnGameEnded()
		{
			if (GameManager.Instance.GameState== GameState.End)
			{
				return;
			}
			GameManager.Instance.OnGameEnd();
		
			
		}
		private void StartJumpAnimation()
		{
			UIManager.Instance.LevelsButton.gameObject.SetActive(false);
			UIManager.Instance.SettingButton.gameObject.SetActive(false);

			_totalJumpingTime = 0;

			_jumpAnimation?.Kill();
			_jumpAnimation = null;
			_jumpAnimation = transform.DOLocalJump(endPosition, jumpPower, jumpCount, duration).SetEase(jumpEase);

			endPosition.z += 5f;

			UIManager.Instance.handImage.DOFade(0f, fadeDuration).OnComplete(() =>
			 {
				 Destroy(UIManager.Instance.panel.gameObject);
				
			 });

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
				OnGameEnded();
			}
		}

		private void Update()
		{

			if (GameManager.Instance.GameState == GameState.Playing)
			{
				
				_totalJumpingTime += Time.deltaTime * 1f;
				if (_totalJumpingTime > duration + 0.1f)
				{
					OnGameEnded();
				}
			}
		}
	}
}