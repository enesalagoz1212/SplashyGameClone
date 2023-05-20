using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class JUMP : MonoBehaviour
{
    public float jumpForce = 5.0f;
    public float jumpDuration = 0.5f;
    private bool isJumping = true;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && isJumping)
        {
            isJumping = false;

            transform.DOJump(transform.position + Vector3.up, jumpForce, 1, jumpDuration)
                .OnComplete(() => isJumping = true);
        }
    }
}
