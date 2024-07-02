// Import các thư viện cần thiết cho Unity
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Định nghĩa lớp ItemCollector, kế thừa từ MonoBehaviour
public class ItemCollector : MonoBehaviour
{
    // Biến lưu trữ số lượng chuối đã thu thập
    private int bananas = 0;

    private int totalBananas;
    [SerializeField] private Text BananasText;
    [SerializeField] private AudioSource collectsoundeffect;

    private int currentLevelIndex;

    // Hàm Start được gọi khi đối tượng khởi tạo
    void Start()
    {
        currentLevelIndex = SceneManager.GetActiveScene().buildIndex;

        totalBananas = GameObject.FindGameObjectsWithTag("banana").Length;

        BananasText.text = "Level " + currentLevelIndex + " - Bananas: " + bananas + "/" + totalBananas;
    }

    // Hàm OnTriggerEnter2D được gọi khi có va chạm với collider 2D khác
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra xem đối tượng va chạm có tag "banana" không
        if (collision.gameObject.CompareTag("banana"))
        {      
            collectsoundeffect.Play();
            Destroy(collision.gameObject);

            bananas++;

            // Cập nhật văn bản hiển thị số lượng chuối đã thu thập
            BananasText.text = "Level " + currentLevelIndex + " - Bananas: " + bananas + "/" + totalBananas;
        }
    }

    // Hàm kiểm tra xem người chơi đã thu thập đủ chuối chưa
    public bool checkBananas()
    {
        return bananas == totalBananas;
    }
}
