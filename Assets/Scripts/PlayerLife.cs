using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Animator anim; // Biến lưu trữ thành phần Animator của nhân vật
    private Rigidbody2D rb; // Biến lưu trữ thành phần Rigidbody2D của nhân vật
    [SerializeField] private AudioSource dieeffect; // Âm thanh phát khi nhân vật chết

    private void Start()
    {
        anim = GetComponent<Animator>(); // Lấy thành phần Animator từ đối tượng hiện tại
        rb = GetComponent<Rigidbody2D>(); // Lấy thành phần Rigidbody2D từ đối tượng hiện tại
    }

    // Hàm xử lý va chạm
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("trap")) // Kiểm tra nếu va chạm với đối tượng có tag "trap"
        {
            Die(); // Gọi hàm Die nếu va chạm với "trap"
        }
    }

    // Hàm xử lý khi nhân vật chết
    private void Die()
    {
        dieeffect.Play(); // Phát âm thanh chết
        rb.bodyType = RigidbodyType2D.Static; // Đặt Rigidbody thành Static để dừng mọi chuyển động
        anim.SetTrigger("death"); // Kích hoạt trigger "death" trong Animator để phát hoạt cảnh chết
    }

    // Hàm tải lại màn chơi
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Tải lại màn chơi hiện tại
    }
}
