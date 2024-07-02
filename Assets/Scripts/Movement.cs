//Trên window

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb; // Biến lưu trữ thành phần Rigidbody2D của nhân vật
    private BoxCollider2D col; // Biến lưu trữ thành phần BoxCollider2D của nhân vật
    private Animator anim; // Biến lưu trữ thành phần Animator của nhân vật
    private float dirx = 0f; // Hướng di chuyển theo trục x
    private SpriteRenderer sprite; // Biến lưu trữ thành phần SpriteRenderer của nhân vật
    [SerializeField] private float movespeed = 8f; // Tốc độ di chuyển của nhân vật
    [SerializeField] private float jumpforce = 9f; // Lực nhảy của nhân vật
    [SerializeField] private LayerMask jumpableGround; // Lớp đất mà nhân vật có thể nhảy lên

    private enum MovementState { idle, running, jumping, falling } // Enum định nghĩa các trạng thái di chuyển của nhân vật

    [SerializeField] private AudioSource Jumpaffect; // Âm thanh khi nhảy

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Lấy thành phần Rigidbody2D của nhân vật
        col = GetComponent<BoxCollider2D>(); // Lấy thành phần BoxCollider2D của nhân vật
        sprite = GetComponent<SpriteRenderer>(); // Lấy thành phần SpriteRenderer của nhân vật
        anim = GetComponent<Animator>(); // Lấy thành phần Animator của nhân vật
    }

    // Update được gọi một lần mỗi khung hình
    private void Update()
    {
        // Lấy hướng di chuyển từ đầu vào của người chơi
        dirx = Input.GetAxisRaw("Horizontal");

        // Cập nhật vận tốc của nhân vật dựa trên hướng di chuyển và tốc độ di chuyển
        rb.velocity = new Vector2(dirx * movespeed, rb.velocity.y);

        // Kiểm tra nếu phím nhảy được nhấn và nhân vật đang đứng trên đất
        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            Jumpaffect.Play(); // Phát âm thanh khi nhảy
            // Cập nhật vận tốc theo trục y để thực hiện nhảy
            GetComponent<Rigidbody2D>().velocity = new Vector3(rb.velocity.x, jumpforce);
        }

        // Cập nhật trạng thái animation của nhân vật
        UpdateAnimationState();
    }

    // Hàm cập nhật trạng thái animation của nhân vật
    private void UpdateAnimationState()
    {
        MovementState state;

        // Kiểm tra hướng di chuyển để đặt trạng thái animation tương ứng
        if (dirx > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false; // Không lật ảnh nhân vật
        }
        else if (dirx < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true; // Lật ảnh nhân vật
        }
        else
        {
            state = MovementState.idle; // Đặt trạng thái idle nếu không di chuyển
        }

        // Kiểm tra vận tốc theo trục y để đặt trạng thái jumping hoặc falling
        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        // Cập nhật trạng thái animation trong Animator
        anim.SetInteger("state", (int)state);
    }

    // Hàm kiểm tra xem nhân vật có đang đứng trên đất hay không
    private bool isGrounded()
    {
        // Sử dụng BoxCast để kiểm tra va chạm với lớp đất có thể nhảy lên
        return Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}

//trên mấy tính
/* 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D col;
    private Animator anim;
    private float dirx = 0f;
    private SpriteRenderer sprite;
    [SerializeField] private float movespeed = 8f;
    [SerializeField] private float jumpforce = 9f;
    [SerializeField] private LayerMask jumpableGround;
    private enum MovementState { idle, running, jumping, falling }
    [SerializeField] private AudioSource Jumpaffect;
    private float lastTapTime = 0f;
    private const float doubleTapTime = 0.3f;
    private bool isMovingLeft = false;
    private bool isMovingRight = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        HandleTouchInput();

        dirx = 0f;
        if (isMovingLeft)
        {
            dirx = -1f;
        }
        if (isMovingRight)
        {
            dirx = 1f;
        }

        rb.velocity = new Vector2(dirx * movespeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            Jump();
        }

        UpdateAnimationState();
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);

                if (touch.phase == TouchPhase.Began)
                {
                    if (Time.time - lastTapTime < doubleTapTime)
                    {
                        Jump();
                    }
                    lastTapTime = Time.time;

                    if (touch.position.x > Screen.width / 2)
                    {
                        sprite.flipX = false; // Quay mặt sang phải
                        isMovingRight = true;
                    }
                    else if (touch.position.x < Screen.width / 2)
                    {
                        sprite.flipX = true; // Quay mặt sang trái
                        isMovingLeft = true;
                    }
                }
                else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    if (touch.position.x > Screen.width / 2)
                    {
                        isMovingRight = false;
                    }
                    else if (touch.position.x < Screen.width / 2)
                    {
                        isMovingLeft = false;
                    }
                }
            }
        }
        else
        {
            isMovingLeft = false;
            isMovingRight = false;
        }
    }

    private void Jump()
    {
        if (isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            Jumpaffect.Play();
        }
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (rb.velocity.x > 0f)
        {
            state = MovementState.running;
        }
        else if (rb.velocity.x < 0f)
        {
            state = MovementState.running;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    private bool isGrounded()
    {
        return Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
*/
