using System;
using SplashyGame.Managers;
using UnityEngine;

namespace SplashyGame.Movements
{
	public class PlayerHorizontal : MonoBehaviour
	{
		public Transform childTransform;
		public float speed = 0.2f;
		private float firstTouchX;

		private void Update()
		{
			switch (GameManager.Instance.GameState)
			{
				case GameState.Start:
					break;
				
				case GameState.Playing:
					OnGamePlayingState();
					break;
				
				case GameState.End:
					break;
				
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void OnGamePlayingState()
		{
			float diff = 0;
			if (Input.GetMouseButtonDown(0))
			{
				firstTouchX = Input.mousePosition.x;
			}			
			else if(Input.GetMouseButton(0))
			{
				float lastTouch = Input.mousePosition.x;
				diff = lastTouch-firstTouchX;
				firstTouchX = lastTouch;
			}

			transform.position += new Vector3(diff * Time.deltaTime * speed, 0, 0);
		}
	}
}