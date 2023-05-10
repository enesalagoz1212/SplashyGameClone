using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SplashyGame.Controllers
{
	public class CameraController : MonoBehaviour
	{
		public GameObject player;
		public Vector3 offset;
		void Start()
		{
			offset = transform.position - player.transform.position;
		}


		private void LateUpdate()
		{
			transform.position = player.transform.position + offset;
		}



		
		

	}
}