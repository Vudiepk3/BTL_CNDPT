using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float speed = 2f; // Tốc độ quay, có thể chỉnh sửa trong Inspector

    private void Update()
    {
        // Quay đối tượng quanh trục Z với tốc độ được xác định
        transform.Rotate(0, 0, 360 * speed * Time.deltaTime);
    }
}
