// Sử dụng các thư viện cần thiết
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Định nghĩa lớp Finish, kế thừa từ MonoBehaviour
public class Finish : MonoBehaviour
{
    // Biến lưu trữ AudioSource để phát âm thanh khi hoàn thành level
    private AudioSource finishsound;

    // Biến để theo dõi xem level đã hoàn thành hay chưa
    private bool levelcomplete = false;

    // Hàm Start được gọi khi đối tượng khởi tạo
    private void Start()
    {
        // Lấy thành phần AudioSource gắn với đối tượng này
        finishsound = GetComponent<AudioSource>();
    }

    // Hàm được gọi khi có va chạm với collider 2D khác
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra xem đối tượng va chạm có tên là "Player" và level chưa hoàn thành
        if (collision.gameObject.name == "Player" && !levelcomplete)
        {
            bool bananasCollected = FindObjectOfType<ItemCollector>().checkBananas();
            if (bananasCollected)
            {
                finishsound.Play();
                levelcomplete = true;
                Invoke("Completelevel", 2f);

                // Lưu trạng thái hoàn thành level hiện tại
                int currentLevel = SceneManager.GetActiveScene().buildIndex;
                PlayerPrefs.SetInt("Level" + currentLevel, 1);
                PlayerPrefs.Save();
            }
        }
    }

    // Hàm để chuyển sang level tiếp theo
    private void Completelevel()
    {
        // Tải scene tiếp theo dựa trên chỉ số của scene hiện tại
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
