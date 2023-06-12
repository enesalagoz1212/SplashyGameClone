using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace SplashyGame.Controllers
{
    public class UiTutorialController : MonoBehaviour
    {
        public RectTransform handRectTransform;
        
        private void OnEnable()
        {
            StartTutorialAnimation();
        }

        private void OnDisable()
        {
            if (DOTween.IsTweening(handRectTransform))
            {
                DOTween.Kill(handRectTransform);
            }
        }

        public void ActivationOfObject(bool isOpen)
        {
            gameObject.SetActive(isOpen);
        }

        private void StartTutorialAnimation()
        {
            handRectTransform.anchoredPosition = new Vector2(-356, -171);
            handRectTransform.DOAnchorPosX(356, 3f).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
        }
        
        public void OnGameStarted()
        {
            ActivationOfObject(false);
        }
    }
}

