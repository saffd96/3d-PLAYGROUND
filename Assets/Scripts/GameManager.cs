using NaughtyAttributes;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    [SerializeField] private Portal portal;

    [SerializeField] public int maxCoins = 0;
    [ReadOnly] [SerializeField] private int counter;

    private new void Awake()
    {
        base.Awake();
        FindAndSetUnactivePortal();
        counter = FindObjectsOfType<Coin>().Length;
    }

    private void Update()
    {
        if (IsCoinsEnded())
        {
            portal.gameObject.SetActive(true);
        }
    }

    public void FindAndSetUnactivePortal()
    {
        portal = FindObjectOfType<Portal>();
        portal.gameObject.SetActive(false);
    }

    public bool IsCoinsEnded()
    {
        return maxCoins == counter;
    }
}
