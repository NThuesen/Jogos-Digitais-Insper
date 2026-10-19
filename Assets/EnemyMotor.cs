using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMotor : MonoBehaviour
{
    public bool chase;
    public Vector2 pointA;
    public Vector2 pointB;
    public float speed = 1.8f;
    public float detectionRadius = 5f;
    private Rigidbody2D rb;
    private PlayerMovement player;
    private bool goingToB = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindAnyObjectByType<PlayerMovement>();
    }

    void FixedUpdate()
    {
        if (GameController.gameOver || player == null)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }
        Vector2 target = goingToB ? pointB : pointA;
        if (chase && Vector2.Distance(rb.position, player.transform.position) < detectionRadius)
            target = player.transform.position;
        else if (Vector2.Distance(rb.position, target) < 0.15f)
            goingToB = !goingToB;

        Vector2 direction = (target - rb.position).normalized;
        // Wall layer is assigned by the arena setup; no navigation framework needed.
        var wall = Physics2D.CircleCast(rb.position, 0.38f, direction, 0.65f, 1 << 6);
        if (wall.collider != null)
            direction = Vector2.Perpendicular(wall.normal) * Mathf.Sign(Vector2.Dot(direction, Vector2.Perpendicular(wall.normal)) + 0.001f);
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision) { Hit(collision); }
    void OnCollisionStay2D(Collision2D collision) { Hit(collision); }

    void Hit(Collision2D collision)
    {
        var target = collision.gameObject.GetComponent<PlayerMovement>();
        if (target != null) target.TakeHit();
    }
}
