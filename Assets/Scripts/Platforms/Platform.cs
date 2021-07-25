using UnityEngine;

public class Platform : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //play sound;  
        }
    }
}
