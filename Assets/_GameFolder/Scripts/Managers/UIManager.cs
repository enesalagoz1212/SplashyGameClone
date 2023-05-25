using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SplashyGame.Managers
{
	public class UIManager : MonoBehaviour
	{
		public static UIManager Instance { get; private set; }

		public TextMeshProUGUI scoreText;

		private void Awake()
		{
			if (Instance==null)
			{
				Instance = this;
			}
			else
			{
				Destroy(gameObject);
			}
		}

		public void ScoreTextPlayer(int score)
		{
			scoreText.text = " " + score.ToString();
		}
	}

}

