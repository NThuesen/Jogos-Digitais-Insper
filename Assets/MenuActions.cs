using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuActions : MonoBehaviour
{
    public GameObject endGameMenu;

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0 || EventSystem.current == null) return;
        var button = GetComponentInChildren<Button>();
        if (button != null) EventSystem.current.SetSelectedGameObject(button.gameObject);
    }

    public void IniciaJogo()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
