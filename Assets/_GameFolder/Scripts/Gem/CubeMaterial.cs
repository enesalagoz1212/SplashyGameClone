using SplashyGame.Managers;
using UnityEngine;

namespace SplashyGame.Gems
{
	public class CubeMaterial : MonoBehaviour
	{
		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.CompareTag("Ball"))
			{
				LevelManager.Instance.ScalePlatforms();
				Destroy(gameObject);
			}
		}
	}
}