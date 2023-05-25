using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using SplashyGame.Managers;
namespace SplashyGame.Gems
{
	public class CubeMaterial : MonoBehaviour
	{
		LevelManager _levelManager;

		private void Awake()
		{
			_levelManager = GetComponentInParent<LevelManager>();
		}
		void Start()
		{
			
			
		}


		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.CompareTag("Ball"))
			{
				Destroy(gameObject);
				Debug.Log("2");
				_levelManager.ScalePlatforms();

			}
		}

	}

}
