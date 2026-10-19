
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] public GameObject endGamePanel;

    void Start()
    {
        if (endGamePanel != null)
        {
            endGamePanel.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        if (GameController.gameOver && endGamePanel != null)
        {
            endGamePanel.SetActive(true);
        }
    }
}
