using UnityEngine;
using UnityEngine.SceneManagement;

public class WinController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SceneManager.LoadScene("WinGameScene");
        
        }
    }
}
