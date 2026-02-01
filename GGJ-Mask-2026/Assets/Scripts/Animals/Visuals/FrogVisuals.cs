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
            UpdateSpritesOrientation();
            _IdleSprite.gameObject.SetActive(false);
        };

        _frog.OnBounce += UpdateSpritesOrientation;
        
        _frog.OnLand += () =>
        {
            _IdleSprite.gameObject.SetActive(true);
            _jumpSprite.gameObject.SetActive(false);
            isJumping = false;
        };
    }

    public void UpdateSpritesOrientation()
    {
        _jumpSprite.transform.localScale = new Vector3(1, _frog._orientation, 1);
        _IdleSprite.transform.localScale = new Vector3(_frog._orientation, 1, 1);
    }

    void Start()
    {
        UpdateSpritesOrientation();
    }

    void Update()
    {
        _jumpSprite.transform.rotation = 
            Quaternion.AngleAxis(Mathf.Rad2Deg * Mathf.Atan2(_frog.Velocity.y,_frog.Velocity.x), Vector3.forward);
        
    }
}
