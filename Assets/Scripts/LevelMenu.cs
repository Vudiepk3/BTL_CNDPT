// Import các thư viện cần thiết cho Unity
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Định nghĩa lớp LevelMenu, kế thừa từ MonoBehaviour
public class LevelMenu : MonoBehaviour
{
    // Mảng các nút level
    public Button[] levelButtons;

    // Mảng lưu trạng thái đã mở khóa của từng level
    private bool[] levelUnlocked;

    // Hàm Start được gọi khi đối tượng khởi tạo
    private void Start()
    {
        // Lấy số lượng level trong build settings
        int numberOfLevels = SceneManager.sceneCountInBuildSettings;

        // Khởi tạo mảng lưu trạng thái đã mở khóa của các level (trừ level 0)
        levelUnlocked = new bool[numberOfLevels - 1];

        // Cập nhật trạng thái ban đầu của các nút level
        UpdateLevelButtons();
    }

    // Hàm mở level khi nhấn nút
    public void OpenLevel(int levelId)
    {
        // Xác định tên level dựa trên id
        string levelName = "Level " + levelId;

        // Chuyển đến cảnh có tên tương ứng
        SceneManager.LoadScene(levelName);
    }

    // Hàm reset trạng thái của các level
    public void ResetLevel()
    {
        // Lấy số lượng level trong build settings
        int numberOfLevels = SceneManager.sceneCountInBuildSettings;

        // Xóa trạng thái hoàn thành của tất cả các level (trừ level 0)
        for (int i = 1; i < numberOfLevels; i++)
        {
            PlayerPrefs.DeleteKey("Level" + i);
        }

        // Lưu lại các thay đổi trong PlayerPrefs
        PlayerPrefs.Save();

        // Cập nhật lại trạng thái của các nút level sau khi reset
        UpdateLevelButtons();
    }

    // Hàm cập nhật trạng thái của các nút level
    private void UpdateLevelButtons()
    {
        bool level1Completed = PlayerPrefs.GetInt("Level1", 0) == 1;
        levelUnlocked[0] = level1Completed;
        levelButtons[0].interactable = level1Completed || SceneManager.GetActiveScene().buildIndex == 0;

        // Lặp qua các nút level khác
        for (int i = 1; i < levelButtons.Length; i++)
        {

            bool levelCompleted = PlayerPrefs.GetInt("Level" + (i + 1), 0) == 1;
            levelUnlocked[i] = levelCompleted;
            levelButtons[i].interactable = levelCompleted || levelUnlocked[i - 1];
        }
    }
}
