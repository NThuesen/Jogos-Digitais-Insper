using UnityEngine;

public class MatchAudio : MonoBehaviour
{
    public AudioSource music;
    public AudioSource effects;
    public AudioClip hit;
    public AudioClip victory;
    public AudioClip defeat;
    private int lastLives = 3;
    private bool ended;

    void Update()
    {
        if (GameController.Lives < lastLives)
        {
            effects.PlayOneShot(hit, 0.75f);
            lastLives = GameController.Lives;
        }
        if (ended || !GameController.gameOver) return;
        ended = true;
        music.volume *= 0.4f;
        effects.PlayOneShot(GameController.Result == GameController.Outcome.Victory ? victory : defeat, 0.85f);
    }
}
