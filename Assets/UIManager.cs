using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject endGamePanel;
    public TMP_Text hud;
    public TMP_Text resultTitle;
    public TMP_Text resultDetails;
    public Button restartButton;
    public float roundSeconds = 90f;
    private bool shown;

    void Awake()
    {
        GameController.Init(GameObject.FindGameObjectsWithTag("Coletavel").Length, roundSeconds);
        if (endGamePanel != null) endGamePanel.SetActive(false);
    }

    void Update()
    {
        GameController.Tick(Time.deltaTime);
        if (hud != null)
            hud.text = string.Format("PONTOS  {0:000}     CRISTAIS  {1}/{2}     TEMPO  {3:00}s     VIDAS  {4}",
                GameController.Score, GameController.Total - GameController.Remaining,
                GameController.Total, Mathf.CeilToInt(GameController.TimeRemaining), GameController.Lives);
        if (!GameController.gameOver || shown) return;
        shown = true;
        if (resultTitle != null)
            resultTitle.text = GameController.Result == GameController.Outcome.Victory ? "EXTRAÇÃO CONCLUÍDA" : "MISSÃO INTERROMPIDA";
        string reason = GameController.Result == GameController.Outcome.Victory ? "Os quatro cristais estão seguros." :
            GameController.Result == GameController.Outcome.TimeExpired ? "O tempo de extração acabou." : "Seu escudo ficou sem energia.";
        if (resultDetails != null)
            resultDetails.text = string.Format("{0}\n\nPontos: {1}   •   Cristais: {2}/{3}\nTempo final: {4:0.0}s   •   Vidas: {5}", reason,
                GameController.Score, GameController.Total - GameController.Remaining, GameController.Total, GameController.Elapsed, GameController.Lives);
        if (endGamePanel != null) endGamePanel.SetActive(true);
        if (restartButton != null && EventSystem.current != null)
            EventSystem.current.SetSelectedGameObject(restartButton.gameObject);
    }
}
