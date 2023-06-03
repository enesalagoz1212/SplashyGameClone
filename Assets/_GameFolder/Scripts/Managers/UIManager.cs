using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
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
			GameManager.OnDiamondScoreIncreased += OnDiamondScoreIncreased;
			
		}

		private void OnDisable()
		{
			GameManager.OnGameStarted -= OnGameStarted;
			GameManager.OnGameScoreIncreased -= OnGameScoreIncreased;
			GameManager.OnDiamondScoreIncreased -= OnDiamondScoreIncreased;
		}

		private void Start()
		{
			BestScoreText.text = $"best score: {GameManager.BestScore}";
			fullImage.fillAmount = 0f;


			levelText.text = $"LEVEL {LevelManager.Instance.level[0].ToString()}";
			UIManager.Instance.MoveImageAnimation();
			
			SetDiamondText();
		}

		private void OnGameStarted()
		{
			BestScoreText.gameObject.SetActive(false);

			gameScoreText.gameObject.SetActive(true);
			gameScoreText.text = GameManager.Instance.gameScore.ToString();
			
			SetDiamondText();
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
	}
}