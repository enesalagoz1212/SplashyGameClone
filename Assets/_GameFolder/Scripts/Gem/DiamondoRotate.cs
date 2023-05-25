using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

namespace SplashyGame.Gems
{
	public class DiamondoRotate : MonoBehaviour
	{
		
		public float rotationDuration;
		public Ease rotationEase = Ease.Linear;


		
		private void Start()
		{
			
			
			Rotate();
		}

		public void Rotate()
		{
			transform.DORotate(new Vector3(0f, 360f, 0f), rotationDuration, RotateMode.FastBeyond360).SetEase(rotationEase).SetLoops(-1, LoopType.Restart);
		}
		

	}

}
