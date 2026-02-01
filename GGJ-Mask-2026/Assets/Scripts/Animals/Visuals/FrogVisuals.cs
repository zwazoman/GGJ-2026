using System;
using UnityEngine;

public class FrogVisuals : MonoBehaviour
{
    [Header("Scene References")]
    [SerializeField] private Transform _jumpSprite;
    [SerializeField] private Transform _IdleSprite;
    [SerializeField] private Transform _VisualsRoot;
    [SerializeField] private Frog _frog;
    private bool isJumping = false;
    private void Awake()
    {
        _frog.OnJump += () =>
        {
            isJumping = true;
            _jumpSprite.gameObject.SetActive(true);
            _jumpSprite.transform.localScale = new Vector3(1, Mathf.Sign(_frog.Velocity.x), 1);
            _IdleSprite.gameObject.SetActive(false);
        };
        
        _frog.OnBounce += () =>
        {
            _jumpSprite.transform.localScale = new Vector3(1, Mathf.Sign(_frog.Velocity.x), 1);
        };
        
        _frog.OnLand += () =>
        {
            _IdleSprite.gameObject.SetActive(true);
            _jumpSprite.gameObject.SetActive(false);
            isJumping = false;
        };
    }

    void Update()
    {
        _jumpSprite.transform.rotation = 
            Quaternion.AngleAxis(Mathf.Rad2Deg * Mathf.Atan2(_frog.Velocity.y,_frog.Velocity.x), Vector3.forward);
        
    }
}
