using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu; // Đối tượng UI của menu tạm dừng

    // Hàm tạm dừng trò chơi
    public void Pause()
    {
        pauseMenu.SetActive(true); // Hiển thị menu tạm dừng
        Time.timeScale = 0; // Dừng thời gian trong trò chơi

        // Tìm và tạm dừng âm thanh nền
        GameObject backgroundSound = GameObject.FindGameObjectWithTag("BackgroundSound");
        if (backgroundSound != null)
        {
            AudioSource audioSource = backgroundSound.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.Pause(); // Tạm dừng âm thanh
            }
        }
    }

    // Hàm trở về màn hình chính
    public void Home()
    {
        SceneManager.LoadScene("Main Menu"); // Tải lại màn hình chính
        Time.timeScale = 1; // Khôi phục thời gian trong trò chơi
    }

    // Hàm tiếp tục trò chơi
    public void Resume()
    {
        pauseMenu.SetActive(false); // Ẩn menu tạm dừng
        Time.timeScale = 1; // Khôi phục thời gian trong trò chơi

        // Tìm và tiếp tục phát âm thanh nền
        GameObject backgroundSound = GameObject.FindGameObjectWithTag("BackgroundSound");
        if (backgroundSound != null)
        {
            AudioSource audioSource = backgroundSound.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.UnPause(); // Tiếp tục phát âm thanh
            }
        }
    }

    // Hàm tải lại màn chơi hiện tại
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Tải lại màn chơi hiện tại
        Time.timeScale = 1; // Khôi phục thời gian trong trò chơi
    }
}
