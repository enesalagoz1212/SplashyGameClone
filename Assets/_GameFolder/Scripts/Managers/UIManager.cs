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

		[Header("Ui Hand")] 
		public UiTutorialController uiTutorialController;

		[Header("Ui Text")]
		public TextMeshProUGUI BestScoreText;
		public TMP_Text gameScoreText;

		public Button LevelsButton;
		public Button SettingButton;

		public Image fullImage;

		public TextMeshProUGUI diamondText;
		public TextMeshProUGUI levelText;
		public TextMeshProUGUI levelPassedText;

		public Image whiteImage;
		public Image sliderImage;
		public Image diamondImage;
		public TMP_Text levels;
		
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
			GameManager.OnGameReset += OnGameReset;
			GameManager.OnGameScoreIncreased += OnGameScoreIncreased;
			GameManager.OnDiamondScoreIncreased += OnDiamondScoreIncreased;
		}

		private void OnDisable()
		{
			GameManager.OnGameStarted -= OnGameStarted;
			GameManager.OnGameEnded -= OnGameEnded;
			GameManager.OnGameReset -= OnGameReset;
			GameManager.OnGameScoreIncreased -= OnGameScoreIncreased;
			GameManager.OnDiamondScoreIncreased -= OnDiamondScoreIncreased;
		}

		private void Start()
		{
			fullImage.fillAmount = 0f;
			
			whiteImage.gameObject.SetActive(false);
			
			LevelsButton.gameObject.SetActive(true);
			
			uiTutorialController.ActivationOfObject(true);
			
			gameScoreText.gameObject.SetActive(false);

			SetGameScoreText();
			SetBestScoreText();
			SetLevelText(); // DEGISECEK!

			SetDiamondText();
		}
		
		private void Update()
		{
			if (GameManager.Instance.GameState == GameState.Playing)
			{
				fullImage.fillAmount = LevelManager.Instance.ReturnPlayerProgress();
			}
		}
		
		private void OnGameStarted()
		{
			BestScoreText.gameObject.SetActive(false);
			gameScoreText.gameObject.SetActive(true);
			
			SetGameScoreText();
			SetDiamondText();
			
			LevelsButton.gameObject.SetActive(false);
			SettingButton.gameObject.SetActive(false);

			uiTutorialController.OnGameStarted();
		}

		private void OnGameEnded(bool isSuccess)
		{
			diamondImage.gameObject.SetActive(false);
			gameScoreText.gameObject.SetActive(false);
			
			whiteImage.gameObject.SetActive(true);

			var whiteImageColor = whiteImage.color;
			whiteImageColor.a = 0f;
			whiteImage.color = whiteImageColor;

			// ORNEK OLARAK 1 SANIYE BEKLEME - DOTWEEN
			DOVirtual.DelayedCall(1f, () =>
			{
				// BURAYA YAZDIKLARIMIZ, 1 SANIYE SONRASINDA CALISIR
			});

			if (isSuccess)
			{
				levelPassedText.gameObject.SetActive(true);
			}

			StartCoroutine(OnGameEndCoroutine());
		}

		private IEnumerator OnGameEndCoroutine()
		{
			yield return new WaitForSeconds(1f);

			whiteImage.DOFade(1f, 0.5f).OnComplete(() =>
			{
				GameManager.Instance.OnGameResetAction();
			});
		}
		
		private void OnGameReset()
		{
			fullImage.fillAmount = 0f;
			
			levelPassedText.gameObject.SetActive(false);
			
			diamondImage.gameObject.SetActive(true);
			BestScoreText.gameObject.SetActive(true);
			SetBestScoreText();
			
			LevelsButton.gameObject.SetActive(true);

			SettingButton.gameObject.SetActive(true);
			
			uiTutorialController.ActivationOfObject(true);

			DOVirtual.DelayedCall(1f, () =>
			{
				SetGameScoreText();
				whiteImage.DOFade(0f, 0.5f).OnComplete(() =>
				{
					GameManager.Instance.OnGameResetCompleted();
				});
			});
		}
		
		private void SetGameScoreText()
		{
			gameScoreText.text = GameManager.Instance.gameScore.ToString();
		}

		private void SetBestScoreText()
		{
			BestScoreText.text = $"best score: {GameManager.BestScore}";
		}

		public void SetLevelText()
		{
			levelText.text = $"LEVEL  {LevelManager.LevelNumber.ToString()}";
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
	}
}