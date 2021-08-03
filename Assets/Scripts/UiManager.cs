using System;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] private PauseView pauseMenu;

    public void PauseToggle(bool isActive)
    {
        if (isActive)
        {
            pauseMenu.Show();
        }
        else
        {
            pauseMenu.Hide();
        }
    }
}
