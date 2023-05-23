using UnityEngine;
using DG.Tweening;
using TMPro;

namespace SplashyGame.Platforms
{
	public class Platform : MonoBehaviour
	{
		public Transform objectsTransform;
		public GameObject colorObject;
		public GameObject whitePlateObject;
		public GameObject diamondo;
		public TextMeshPro numberEffectText;

		public float onCollidedUpPosY;
		public float onCollidedUpMoveTime;
		public Ease onCollidedUpMoveEase;

		public float onCollidedDownPosY;
		public float onCollidedDownMoveTime;
		public Ease onCollidedDownMoveEase;

		public bool IsCollidedPlayer { get; private set; }

		public void OnPlatformCreated(bool isFirstObject, bool isColorObjectOpen, bool isWhitePlateOpen,bool isDiamondo)
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
	}
}