using UnityEngine;
using System;
using SplashyGame.Gems;

namespace SplashyGame.Managers
{
	public enum GameState
	{
		Start = 0,
		Playing = 1,
		End = 2,
	}

	public class GameManager : MonoBehaviour
	{

		public const string BestScorePrefsString = "BestScore";
		public const string DiamondScorePrefsString = "DiamondScore";

		public static int DiamondScore
		{
			get
			{
				return PlayerPrefs.GetInt(DiamondScorePrefsString);
			}
			set
			{
				PlayerPrefs.SetInt(DiamondScorePrefsString, value);
			}
		}
		public static int BestScore
		{
			get
			{
				return PlayerPrefs.GetInt(BestScorePrefsString);
			}
			set
			{
				PlayerPrefs.SetInt(BestScorePrefsString, value);
			}
		}

		public int gameScore;
		public static GameManager Instance { get; private set; }

		public static Action OnGameStarted;
		public static Action OnGameEnded;
		public static Action<int> OnGameScoreIncreased;
		public static Action<int> OnDiamondScoreIncreased;
		DiamondoRotate _diamondoRotate;
		public GameState GameState { get;  set; }

		private void Awake()
		{
			// If there is an instance, and it's not me, delete myself.
			if (Instance != null && Instance != this)
			{
				Destroy(this);
			}
			else
			{
				Instance = this;
			}
		}

		private void Start()
		{
			GameState = GameState.Start;
			_diamondoRotate = gameObject.GetComponent<DiamondoRotate>();
		}

		private void Update()
		{
			switch (GameState)
			{
				case GameState.Start:
				
					if (Input.GetMouseButtonDown(0))
					{
						OnGameStart();
					}
					break;

				case GameState.Playing:

					break;

				case GameState.End:
					
					break;

				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public void OnGameStart()
		{
			GameState = GameState.Playing;
			OnGameStarted?.Invoke();

			gameScore = 0;

			_diamondoRotate.Rotate();
		}

		public void OnGameEnd()
		{
			GameState = GameState.End;
			OnGameEnded?.Invoke();

			if (gameScore > BestScore)
			{
				BestScore = gameScore;
			}
		}

		public void IncreaseGameScore(int increaseAmount)
		{
			gameScore += increaseAmount;
			OnGameScoreIncreased?.Invoke(gameScore);
		}
		
		public void IncreaseDiamondScore(int increase)
		{
			DiamondScore += increase;
			OnDiamondScoreIncreased?.Invoke(DiamondScore);
			
		}
	}
}