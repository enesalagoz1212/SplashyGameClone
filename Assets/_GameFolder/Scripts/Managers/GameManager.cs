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
		
		public static GameManager Instance { get; private set; }

		public static Action OnGameStarted;
		DiamondoRotate _diamondoRotate;
		public GameState GameState { get; private set; }

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

			_diamondoRotate.Rotate();
		}
		
		public void OnGameEnd()
		{
			GameState = GameState.End;
		}
	}
}