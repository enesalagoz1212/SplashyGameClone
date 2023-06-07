using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using SplashyGame.Controllers;
using System.Collections;

namespace SplashyGame.Managers
{
	public class UIManager : MonoBehaviour
	{
		public static UIManager Instance { get; private set; }


		public GameObject panel;
		public Image handImage;

		public float moveDistance;
		public float moveDuration;

		public TextMeshProUGUI BestScoreText;
		public TMP_Text gameScoreText;

		public Button LevelsButton;
		public Button SettingButton;

		public Image fullImage;
		//public float waitTime = 21.36f;

		public TextMeshProUGUI diamondText;
		public TextMeshProUGUI levelText;
		public TextMeshProUGUI levelPassedText;

		public Image whiteImage;
		public Image sliderImage;
		public Image diamondImage;
		public TMP_Text levels;

		private bool _isEndSuccess;
		private void Awake()
		{
			if (Instance == null)
			{
				Instance = this;
			}
			else
			{
				Destroy(gameObject);
			}
		}

		private void OnEnable()
		{

			GameManager.OnGameStarted += OnGameStarted;
			GameManager.OnGameEnded += OnGameEnded;
			GameManager.OnGameReseted += OnGameReseted;
			GameManager.OnGameScoreIncreased += OnGameScoreIncreased;
			GameManager.OnDiamondScoreIncreased += OnDiamondScoreIncreased;

		}

		private void OnDisable()
		{
			GameManager.OnGameStarted -= OnGameStarted;
			GameManager.OnGameEnded -= OnGameEnded;
			GameManager.OnGameReseted -= OnGameReseted;
			GameManager.OnGameScoreIncreased -= OnGameScoreIncreased;
			GameManager.OnDiamondScoreIncreased -= OnDiamondScoreIncreased;
		}

		private void Start()
		{


			BestScoreText.text = $"best score: {GameManager.BestScore}";
			fullImage.fillAmount = 0f;

			SetLevelText(1); // DEGISECEK!

			MoveImageAnimation();

			SetDiamondText();
		}
		public void OnGameReseted()
		{
			StartCoroutine(ResetPlatformCoroutine());
		}
		private void OnGameEnded(bool isSuccess)
		{
			_isEndSuccess = isSuccess;
			if (!_isEndSuccess)
			{
				
				gameScoreText.gameObject.SetActive(false);
				diamondText.gameObject.SetActive(false);
				diamondImage.gameObject.SetActive(false);

				whiteImage.gameObject.SetActive(true);


				levels.DOFade(0f, 0.9f).OnComplete(() =>
				{
					
					sliderImage.gameObject.SetActive(false);
					whiteImage.DOFade(255f, 3f).OnComplete(() =>
					{
						
						whiteImage.DOFade(0f, 1f);

						panel.gameObject.SetActive(true);

					});

				});


				OnGameReseted();

			}

		}
		private void OnGameStarted()
		{
			BestScoreText.gameObject.SetActive(false);
			gameScoreText.gameObject.SetActive(true);

			gameScoreText.text = GameManager.Instance.gameScore.ToString();

			SetDiamondText();

			// // // // 

			LevelsButton.gameObject.SetActive(false);
			SettingButton.gameObject.SetActive(false);

			handImage.DOFade(0f, 0.5f).OnComplete(() =>
			{

				panel.transform.localScale = Vector3.zero;

			});
		}

		private void Update()
		{
			if (GameManager.Instance.GameState == GameState.Playing)
			{
				fullImage.fillAmount = LevelManager.Instance.ReturnPlayerProgress();
				// if (gameActive)
				// {
				// 	Debug.Log("decreasing");
				// 	fullImage.fillAmount -= 1.02f / waitTime * Time.deltaTime;
				// }
			}
		}

		public void SetLevelText(int level)
		{
			Debug.Log(level);
			levelText.text = "LEVEL " + level.ToString();
		}

		private void SetDiamondText()
		{
			diamondText.text = $"{GameManager.DiamondScore}";
		}

		private void OnGameScoreIncreased(int gameScore)
		{
			gameScoreText.text = gameScore.ToString();
		}


		private void OnDiamondScoreIncreased(int diamondScore)
		{
			SetDiamondText();
		}

		public void MoveImageAnimation()
		{
			handImage.rectTransform.anchoredPosition = new Vector2(-356, -171);
			handImage.rectTransform.DOAnchorPosX(moveDistance, moveDuration).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
		}
		public IEnumerator ResetPlatformCoroutine()
		{

			yield return LevelManager.Instance.ResetPlatforms();

		}
	}
}