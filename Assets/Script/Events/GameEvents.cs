using UnityEngine;
using UnityEngine.SceneManagement;

namespace CrazyHub.Hyderabad.Assignment
{
    public class GameEvents : MonoBehaviour
    {
       public void Replay() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
       }

       public void QuitGame() {
            Application.Quit();
       }
    }
}
