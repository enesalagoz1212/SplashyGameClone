using UnityEngine;
using DG.Tweening;
using TMPro;
using SplashyGame.Managers;
using SplashyGame.Gems;

namespace SplashyGame.Platforms
{
	public class Platform : MonoBehaviour
	{
		//CubeMaterial _cubeMaterial;

		public Transform objectsTransform;
		public GameObject colorObject;
		public GameObject whitePlateObject;
		public GameObject diamondo;
		public GameObject flag;
		public TextMeshPro numberEffectText;
		public MeshRenderer plateRenderer;


		public float onCollidedUpPosY;
		public float onCollidedUpMoveTime;
		public Ease onCollidedUpMoveEase;

		public float onCollidedDownPosY;
		public float onCollidedDownMoveTime;
		public Ease onCollidedDownMoveEase;

		public bool IsCollidedPlayer { get; private set; }

		private void Awake()
		{

			//_cubeMaterial = GetComponent<CubeMaterial>();
		}
		public void OnPlatformCreated(bool isFirstObject, bool isColorObjectOpen, bool isWhitePlateOpen, bool isDiamondo, bool isFlag)
		{
			numberEffectText.gameObject.SetActive(false);

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
			diamondo.SetActive(isDiamondo);
			flag.SetActive(isFlag);
		}

		public void OnCollidedPlayer()
		{
			IsCollidedPlayer = true;


			CreateNumberEffect();
		}

		private void CreateNumberEffect()
		{
			numberEffectText.gameObject.SetActive(true);

			numberEffectText.transform.localScale = Vector3.zero;
			numberEffectText.color = Color.white;



			numberEffectText.transform.DOScale(1f, 0.4f).OnComplete(() =>
			{
				CreateUpDownMoveEffect();

				numberEffectText.DOFade(0f, 0.3f).SetDelay(0.2f);


			});



		}

		
		private void CreateUpDownMoveEffect()
		{
			objectsTransform.DOMoveY(onCollidedUpPosY, onCollidedUpMoveTime).SetEase(onCollidedUpMoveEase).OnComplete(
				() =>
				{
					objectsTransform.DOMoveY(onCollidedDownPosY, onCollidedDownMoveTime).SetEase(onCollidedDownMoveEase);
				});
		}
		
		public void PlatformScalingColoringAnimation(Color color)
		{
			transform.DOScale(new Vector3(1.3f, transform.localScale.y, 1.2f), 0.2f).OnComplete(() =>
			{
				plateRenderer.material.DOColor(color, 0.5f);
			});
		}

		//private void OnTriggerEnter(Collider other)
		//{
		//	if (other.gameObject.CompareTag("Ball"))
		//	{
		//		_cubeMaterial.PrefabColorAndScale();
		//	}
		//}
	}
}