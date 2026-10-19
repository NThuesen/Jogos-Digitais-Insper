using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(AudioSource))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private AudioSource collectAudio;
    private SpriteRenderer sprite;
    private Vector2 movement;
    private Color normalColor;
    private float invulnerableUntil;
    public float speed = 5f;
    public float damageCooldown = 1.25f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collectAudio = GetComponent<AudioSource>();
        sprite = GetComponent<SpriteRenderer>();
        if (sprite != null) normalColor = sprite.color;
    }

    void Update()
    {
        movement = Vector2.zero;
        var keyboard = Keyboard.current;
        if (keyboard != null)
        {
            movement.x = (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed ? 1f : 0f) -
                         (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed ? 1f : 0f);
            movement.y = (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed ? 1f : 0f) -
                         (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed ? 1f : 0f);
        }
        if (Gamepad.current != null)
        {
            Vector2 stick = Gamepad.current.leftStick.ReadValue();
            Vector2 dpad = Gamepad.current.dpad.ReadValue();
            Vector2 pad = dpad.sqrMagnitude > stick.sqrMagnitude ? dpad : stick;
            if (pad.sqrMagnitude > movement.sqrMagnitude) movement = pad;
        }
        movement = Vector2.ClampMagnitude(movement, 1f);
        if (sprite != null)
            sprite.color = Time.time < invulnerableUntil && Mathf.FloorToInt(Time.time * 12f) % 2 == 0 ? new Color(1f, 0.3f, 0.3f) : normalColor;
    }

    void FixedUpdate()
    {
        if (GameController.gameOver)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GameController.gameOver || !other.enabled || !other.CompareTag("Coletavel")) return;
        if (!GameController.Collect()) return;
        other.enabled = false;
        collectAudio.Play();
        Destroy(other.gameObject);
    }

    public bool TakeHit()
    {
        if (Time.time < invulnerableUntil || !GameController.Damage()) return false;
        invulnerableUntil = Time.time + damageCooldown;
        return true;
    }
}
