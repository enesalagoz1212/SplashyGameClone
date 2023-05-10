using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using SplashyGame.Managers;

namespace SplashyGame.Controllers
{
	public class Player : MonoBehaviour
	{
		public GameManager gameManager;


		private void OnTriggerEnter(Collider other)
		{
			gameManager.Trigger(gameObject,other.gameObject);
		}


	}
}