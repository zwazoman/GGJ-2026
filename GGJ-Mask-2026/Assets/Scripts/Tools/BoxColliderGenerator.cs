using UnityEditor;
using UnityEngine;

public class BoxColliderGenerator : MonoBehaviour
{
    public void generateBoxes()
    {
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent(out BoxCollider2D box))
            {
                if (child.TryGetComponent(out SpriteRenderer sprite))
                {
                    box.size = new Vector2(sprite.size.x, sprite.size.y);
                }
            }
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(BoxColliderGenerator))]
class BoxColliderGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("generate boxes"))
        {
            ((BoxColliderGenerator)target).generateBoxes();
        }
    }
}
#endif
