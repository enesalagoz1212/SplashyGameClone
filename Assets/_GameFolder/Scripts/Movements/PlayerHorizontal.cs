using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace SplashyGame.Movements
{
	public class PlayerHorizontal : MonoBehaviour
	{
		
		public float speed = 0.2f;
		private float firstTouchX;

		void Start()
		{

		}


		void Update()
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
			transform.position += new Vector3(diff * Time.deltaTime*speed, 0, 0);
			
		}
	}
}

