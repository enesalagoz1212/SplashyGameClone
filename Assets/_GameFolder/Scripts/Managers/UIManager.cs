using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SplashyGame.Managers
{
	public class UIManager : MonoBehaviour
	{
		public static UIManager Instance { get; private set; }

		public TextMeshProUGUI BestScoreText;
		public TMP_Text gameScoreText;
		public TMP_Text diamondoScoreText;

		public Button LevelsButton;
		public Button SettingButton;

		public Image fullImage;
		public float waitTime = 21.36f;

		public TextMeshProUGUI diamondText;
		public TextMeshProUGUI levelText;
		public TextMeshProUGUI levelPassedText;

		
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
			GameManager.OnGameScoreIncreased += OnGameScoreIncreased;
			GameManager.OnDiamodoScoreIncreased += OnDiamondoScoreIncreased;
		}

		private void OnDisable()
		{
			GameManager.OnGameStarted -= OnGameStarted;
			GameManager.OnGameScoreIncreased -= OnGameScoreIncreased;
			GameManager.OnDiamodoScoreIncreased -= OnDiamondoScoreIncreased;
		}
		
		private void Start()
		{
			BestScoreText.text = $"best score: {GameManager.BestScore}";
			diamondoScoreText.text = $" {GameManager.DiamondoScore}";
			fullImage.fillAmount = 0f;
			
			gameScoreText.gameObject.SetActive(false);

			LevelsButton.gameObject.SetActive(false);

			SettingButton.gameObject.SetActive(false);

			levelText.text = $"LEVEL {LevelManager.Instance.level[0].ToString()}";
		}

		private void OnGameStarted()
		{
			BestScoreText.gameObject.SetActive(false);
			
			gameScoreText.gameObject.SetActive(true);
			gameScoreText.text = GameManager.Instance.gameScore.ToString();
			diamondoScoreText.text = GameManager.Instance.diamondoScore.ToString();
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

		private void OnGameScoreIncreased(int gameScore)
		{
			gameScoreText.text = gameScore.ToString();
		}
		private void OnDiamondoScoreIncreased(int diamondoScore)
		{
			diamondoScoreText.text = diamondoScore.ToString();
		}
	}
}