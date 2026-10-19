using UnityEngine;

// Small shared match state; UIManager resets it on every gameplay scene load.
public static class GameController
{
    public enum Outcome { Playing, Victory, TimeExpired, NoLives }
    public static Outcome Result { get; private set; }
    public static int Total { get; private set; }
    public static int Remaining { get; private set; }
    public static int Score { get; private set; }
    public static int Lives { get; private set; }
    public static float TimeRemaining { get; private set; }
    public static float Elapsed { get; private set; }
    public static bool gameOver { get { return Result != Outcome.Playing; } }

    public static void Init(int total = 4, float seconds = 90f)
    {
        Total = Remaining = total;
        Score = 0;
        Lives = 3;
        Elapsed = 0f;
        TimeRemaining = seconds;
        Result = Outcome.Playing;
    }

    public static void Tick(float seconds)
    {
        if (gameOver) return;
        float step = Mathf.Min(Mathf.Max(seconds, 0f), TimeRemaining);
        TimeRemaining -= step;
        Elapsed += step;
        if (TimeRemaining <= 0f) Result = Outcome.TimeExpired;
    }

    public static bool Collect()
    {
        if (gameOver || Remaining <= 0) return false;
        Remaining--;
        Score += 100;
        if (Remaining == 0) Result = Outcome.Victory;
        return true;
    }

    public static bool Damage()
    {
        if (gameOver) return false;
        Lives = Mathf.Max(0, Lives - 1);
        if (Lives == 0) Result = Outcome.NoLives;
        return true;
    }
}
