using System;
using UnityEngine;

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
		
		public GameState gameState;

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
			gameState = GameState.Start;
		}

		public void OnGameStart()
		{
			gameState = GameState.Playing;
		}

		public void OnGameEnd()
		{
			gameState = GameState.End;
		}
	}
}