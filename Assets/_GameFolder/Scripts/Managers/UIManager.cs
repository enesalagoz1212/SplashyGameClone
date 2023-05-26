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
		public Slider sliderBar;

	

		public TextMeshProUGUI scoreText;

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
		private void Start()
		{
			sliderBar.minValue = 0;
			sliderBar.maxValue = 31;
			sliderBar.wholeNumbers = true;
			sliderBar.value = 0;
		}

		public void ScoreTextPlayer(int score)
		{
			scoreText.text = " " + score.ToString();
		}
		
		public void UpdateSliderBar()
		{
			sliderBar.value+=Time.timeScale;
		}
	}

}

