using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SplashyGame.Managers
{
	public class UIManager : MonoBehaviour
	{
		public static UIManager Instance { get; private set; }

		public TextMeshProUGUI BestScoreText;
		public int bestScore;

		public Button LevelsButton;
		public Button SettingButton;


		public Image fullImage;
		public bool gameActive;
		public float waitTime = 21.36f;

		public TextMeshProUGUI scoreText;
		public TextMeshProUGUI levelText;
		public TextMeshProUGUI levelPassedText;

		


		private void Awake()
		{
			gameActive = true;

			if (Instance == null)
			{
				Instance = this;
			}
			else
			{
				Destroy(gameObject);
			}
		}
		private void Start()
		{
			PlayerPrefsGet();

			fullImage.fillAmount = 0f;

			LevelsButton.gameObject.SetActive(false);

			SettingButton.gameObject.SetActive(false);

			levelText.text = $"LEVEL {LevelManager.Instance.level[0].ToString()}";
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
		
		public void ScoreTextPlayer(int score)
		{
			scoreText.text = " " + score.ToString();
		}

		public void BestScoreTextPlayer(int bestScore)
		{
			BestScoreText.text = $"  {bestScore.ToString()}";
		}

		public void PlayerPrefsSet()
		{

			BestScoreTextPlayer(bestScore);
			Debug.Log("Set 1");
			if (bestScore > PlayerPrefs.GetInt(nameof(bestScore), bestScore))
			{


				Debug.Log("enes");
				PlayerPrefs.SetInt(nameof(bestScore), bestScore);
				PlayerPrefs.Save();
			}
			Debug.Log(PlayerPrefs.GetInt(nameof(bestScore)));


			Debug.Log("Set 2");
		}
		public void PlayerPrefsGet()
		{
			bestScore = PlayerPrefs.GetInt(nameof(bestScore), bestScore);
			BestScoreText.text = $"best score : {bestScore.ToString()}";
			Debug.Log("Get calisti");
		}

		public void SetLevelText(int level)
		{
			Debug.Log(level);
			levelText.text = "LEVEL " + level.ToString();
		}
	}

}

