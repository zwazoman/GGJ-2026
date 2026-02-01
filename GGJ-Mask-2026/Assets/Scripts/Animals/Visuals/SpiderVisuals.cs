using UnityEngine;

namespace Animals.Visuals
{
    public class SpiderVisuals : AnimalVisual
    {
        [SerializeField] Spider _spider;
        [SerializeField] Transform _WalkingSprite;

        void Update()
        {
            _WalkingSprite.transform.localScale = new Vector3(-_spider.orientation , 1, 1);
        }
    }
}