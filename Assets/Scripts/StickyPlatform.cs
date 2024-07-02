using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPlatform : MonoBehaviour
{
    // Hàm này được gọi khi có một đối tượng khác va chạm với nền tảng
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Kiểm tra nếu đối tượng va chạm có tên là "Player"
        if (collision.gameObject.name == "Player")
        {
            // Đặt đối tượng "Player" làm con của nền tảng
            // Điều này làm cho "Player" di chuyển cùng với nền tảng
            collision.gameObject.transform.SetParent(transform);
        }
    }

    // Hàm này được gọi khi đối tượng rời khỏi va chạm với nền tảng
    private void OnCollisionExit2D(Collision2D collision)
    {
        // Kiểm tra nếu đối tượng rời khỏi va chạm có tên là "Player"
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}
