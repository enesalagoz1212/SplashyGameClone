using SplashyGame.Managers;
using UnityEngine;
using System;

namespace SplashyGame.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        public Transform childTransform;
        public float speed;
        
        private float _firstTouchX;

        private void OnEnable()
        {
            GameManager.OnGameReset += OnGameReset;
        }

        private void OnDisable()
        {
            GameManager.OnGameReset -= OnGameReset;
        }

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
                _firstTouchX = Input.mousePosition.x;
            }			
            else if(Input.GetMouseButton(0))
            {
                float lastTouch = Input.mousePosition.x;

                diff = lastTouch - _firstTouchX;
                transform.position += new Vector3(diff * Time.deltaTime * speed, 0, 0);

                _firstTouchX = lastTouch;
            }
        }
        
        private void OnGameReset()
        {
            transform.position = new Vector3(0f, 0.5f, 0f);
        }
    }
}