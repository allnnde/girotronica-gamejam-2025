using UnityEngine;
using UnityEngine.SceneManagement;

public class Menuuu : MonoBehaviour
{
    public void iniciar() {
        SceneManager.LoadScene("BedRoomScene_2");
    }
    public void salir() { Application.Quit(); }
}
