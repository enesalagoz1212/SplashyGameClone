using UnityEngine;

namespace SplashyGame.Controllers
{
	public class CameraController : MonoBehaviour
	{
		public GameObject player;
		public Vector3 offset;
		private float _cameraY;

		//public Transform handTransform;
		//public float moveDistance = 1f;
		//public float moveDuration = 1f;
		//public Ease moveEase;
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
		//public void HandMoveAnimation()
		//{
		//	handTransform.position = new Vector3(0, 0, 0);
		//	handTransform.DOMoveX(moveDistance, moveDuration).SetEase(moveEase).SetLoops(-1, LoopType.Yoyo);
		//}
	}
}