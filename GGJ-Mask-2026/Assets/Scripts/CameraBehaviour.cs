using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public static Pawn Target;

    private Vector2 vel;

    private Vector2 lastTargetPosition;
    
    [Header("Settings")] 
    [SerializeField] private float _smoothTime;
    [SerializeField] private float _anticipationStrength;
    
    void FixedUpdate()
    {
        if (!Target) return;
        if (lastTargetPosition == Vector2.zero) lastTargetPosition = Target.transform.position;
        Vector2 velocity = ((Vector2)Target.transform.position - lastTargetPosition)/Time.deltaTime;
        
        transform.position = Vector2.SmoothDamp(transform.position,(Vector2)Target.transform.position + velocity*_anticipationStrength,ref vel,_smoothTime);
        
        lastTargetPosition = Target.transform.position;
    }
}
