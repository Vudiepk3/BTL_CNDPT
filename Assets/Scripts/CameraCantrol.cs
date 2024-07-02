using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Định nghĩa lớp CameraControl, kế thừa từ MonoBehaviour
public class CameraControl : MonoBehaviour
{
    // Biến Player được gán giá trị qua Unity Editor, chỉ định đối tượng Transform của người chơi
    [SerializeField] private Transform Player;

    // Hàm Update được gọi một lần mỗi khung hình
    private void Update()
    {
        // Cập nhật vị trí của camera để theo dõi người chơi
        transform.position = new Vector3(Player.position.x, Player.position.y, transform.position.z);
    }
}
