using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace SplashyGame.Movements
{
	public class PlayerHorizontal : MonoBehaviour
	{
		public bool GameActive { get; set; }
		public float speed = 5f;
		void Start()
		{

		}


		void Update()
		{
			if (GameActive) return;

			var t = transform;
			Vector3 horizontalInput = Vector3.right * speed * (Input.GetAxis("Horizontal"));
			var mouseHorizontalInput = Input.GetAxis("Mouse X") * Time.deltaTime * 50;

			if (Input.GetAxis("Mouse X") != 0)
			{

				transform.DOMoveX(t.position.x + mouseHorizontalInput, 0.1f);
				return;
			}
			if ((Input.GetAxis("Horizontal") == 0)) return;


			transform.DOMove((t.position + horizontalInput), 0.3f);




		}
	}
}

