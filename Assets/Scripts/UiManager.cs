using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    
    public void PauseToggle(bool isActive)
    {
        pauseMenu.SetActive(isActive);
    }
}
