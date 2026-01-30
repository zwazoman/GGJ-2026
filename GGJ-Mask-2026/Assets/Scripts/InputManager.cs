using System;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    #region Singleton
    private static InputManager instance;

    public static InputManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("InputManager");
                instance = go.AddComponent<InputManager>();
            }
            return instance;
        }
    }

    #endregion

    public event Action<Vector2> OnLeftClick;
    public event Action<Vector2> OnRightClick;

    private void Awake()
    {
        if (instance == null || instance == this)
            instance = this;
        else
            Destroy(this);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            OnLeftClick?.Invoke(Input.mousePosition);
        if(Input.GetKeyDown(KeyCode.Mouse1))
            OnRightClick?.Invoke(Input.mousePosition);
    }
}
