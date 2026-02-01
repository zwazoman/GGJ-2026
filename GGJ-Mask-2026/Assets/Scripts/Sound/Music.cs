using UnityEngine;

public class Music : MonoBehaviour
{
    static bool exists;
    private void Awake()
    {
        if(exists)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        exists = true;
    }
}
