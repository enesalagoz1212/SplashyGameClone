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
				Debug.Log("2");
				LevelManager.Instance.ScalePlatforms();
				Destroy(gameObject);
			}
		}
	}
}