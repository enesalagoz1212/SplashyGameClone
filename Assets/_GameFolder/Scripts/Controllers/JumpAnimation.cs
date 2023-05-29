using SplashyGame.Managers;
using SplashyGame.Platforms;
using UnityEngine;
using DG.Tweening;
using TMPro;
using SplashyGame.Gems;
using UnityEngine.SceneManagement;

namespace SplashyGame.Controllers
{
	public class JumpAnimation : MonoBehaviour
	{

		private int score = 0;
		private int bestScore = 0;

		public Vector3 endPosition;

		public Ease jumpEase;
		public float jumpPower;
		public int jumpCount;
		public float duration;

		private float _totalJumpingTime;

		private Tween _jumpAnimation;

		

		private void Awake()
		{
			
		}
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
				bestScore++;
			
				UIManager.Instance.PlayerPrefsSet();
				UIManager.Instance.BestScoreTextPlayer(bestScore);
				UIManager.Instance.SetLevelText(LevelManager.Instance.level[0]);
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

				score++;
				UIManager.Instance.ScoreTextPlayer(score);			
				Destroy(other.gameObject);
			}
			
			if (other.gameObject.CompareTag("Finish"))
			{
				
				
				GameManager.Instance.OnGameEnd();
				Debug.Log(Time.time);
				UIManager.Instance.BestScoreText.gameObject.SetActive(false);
				UIManager.Instance.levelPassedText.gameObject.SetActive(true);
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