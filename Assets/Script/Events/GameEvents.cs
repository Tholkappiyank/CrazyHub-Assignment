using UnityEngine;
using UnityEngine.SceneManagement;

namespace CrazyHub.Hyderabad.Assignment
{
    public class GameEvents : MonoBehaviour
    {
       public void Replay() {
            SceneManager.LoadScene("Base");
       }

       public void QuitGame() {
            Application.Quit();
       }
    }
}
