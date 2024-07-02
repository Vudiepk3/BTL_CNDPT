// Import các thư viện cần thiết cho Unity
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Định nghĩa lớp MainMenu, kế thừa từ MonoBehaviour
public class MainMenu : MonoBehaviour
{
    // Hàm này được gọi khi người chơi nhấn vào nút "Play"
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        // Thoát ứng dụng
        Application.Quit();
    }
}
