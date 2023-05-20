using UnityEngine;
using DG.Tweening;

namespace SplashyGame.Platforms
{
	public class Platform : MonoBehaviour
	{
		public Transform plateTransform;
		public GameObject colorObject;
		public GameObject whitePlateObject;

		public float onCollidedUpPosY;
		public float onCollidedUpMoveTime;
		public Ease onCollidedUpMoveEase;

		public float onCollidedDownPosY;
		public float onCollidedDownMoveTime;
		public Ease onCollidedDownMoveEase;

		public bool IsCollidedPlayer { get; private set; }

		public void OnPlatformCreated(bool isFirstObject, bool isColorObjectOpen, bool isWhitePlateOpen)
		{
			if (isFirstObject)
			{
				IsCollidedPlayer = true;
			}
			else
			{
				IsCollidedPlayer = false;
			}

			colorObject.SetActive(isColorObjectOpen);
			whitePlateObject.SetActive(isWhitePlateOpen);
		}

		public void OnCollidedPlayer()
		{
			IsCollidedPlayer = true;

			transform.DOMoveY(onCollidedUpPosY, onCollidedUpMoveTime).SetEase(onCollidedUpMoveEase).OnComplete(
				() =>
				{
					transform.DOMoveY(onCollidedDownPosY, onCollidedDownMoveTime).SetEase(onCollidedDownMoveEase);
				});
		}
	}
}