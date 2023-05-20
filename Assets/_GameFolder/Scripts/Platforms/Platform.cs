using UnityEngine;
using DG.Tweening;

namespace SplashyGame.Platforms
{
	public class Platform : MonoBehaviour
	{
		public Transform plateTransform;
		public GameObject colorObject;
		public GameObject whitePlateObject;

		public bool IsCollidedPlayer { get; private set; }

		public void OnPlatformCreated(bool isColorObjectOpen, bool isWhitePlateOpen)
		{
			IsCollidedPlayer = false;

			colorObject.SetActive(isColorObjectOpen);
			whitePlateObject.SetActive(isWhitePlateOpen);
		}

		public void OnCollidedPlayer()
		{
			IsCollidedPlayer = true;

			plateTransform.DOMoveY(1.2f, 0.1f);
		}
	}
}