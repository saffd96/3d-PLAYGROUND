using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    [SerializeField] private EndingPortal endingPortal;


    [SerializeField] private UiManager uiManager;
   
    [ReadOnly] [SerializeField] private int coinCounter;
    public int MAXCoins = 0;

    public bool IsPaused { get; private set; }

    private void Start()
    {
        ResetGame();
    }

    private void Update()
    {
        if (IsCoinsEnded())
        {
            endingPortal.gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseToggle();
        }
    }

    public bool IsCoinsEnded()
    {
        return MAXCoins == coinCounter;
    }

    public void PauseToggle()
    {
        IsPaused = !IsPaused;
        uiManager.PauseToggle(IsPaused);

        Time.timeScale = IsPaused ? 0f : 1f;
    }

    private void ResetGame()
    {
        endingPortal = FindObjectOfType<EndingPortal>();
        endingPortal.gameObject.SetActive(false);        
        
        coinCounter = FindObjectsOfType<Coin>().Length;
        
        IsPaused = false;
        uiManager.PauseToggle(IsPaused);
        Time.timeScale = 1f;
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(0);
    }
}
