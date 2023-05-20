using UnityEngine;

namespace SplashyGame.Controllers
{
	public class CameraController : MonoBehaviour
	{
		public GameObject player;
		public Vector3 offset;
		private float _cameraY;
		
		private void Start()
		{
			_cameraY = transform.position.y;
			offset = transform.position - player.transform.position;
		}

		private void LateUpdate()
		{
			Vector3 position = player.transform.position + offset;
			position.y = _cameraY;
			transform.position = position;
		}
	}
}