using System.Xml.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager instance;
    
    bool isLoadingNewScene = false;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("GameManager");
                instance = go.AddComponent<GameManager>();
            }
            return instance;
        }
    }

    public async void LoadScene(int scene)
    {
        isLoadingNewScene = false;
        AsyncOperation op = SceneManager.LoadSceneAsync(scene);
        //todo : animation
        while (!op.isDone)
            await Awaitable.NextFrameAsync();
        isLoadingNewScene = true;
        op.allowSceneActivation = true;
    }
    
    private void Awake()
    {
        if (instance == null || instance == this)
            instance = this;
        else
            Destroy(this);
    }
    #endregion


    [SerializeField] Mask _mask;

    [SerializeField] Pawn _firstPawn;


    private void Start()
    {
        _firstPawn.GainControl(_mask);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            Restart();
    }

    public void Restart()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
