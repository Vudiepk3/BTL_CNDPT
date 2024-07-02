using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    // Mảng chứa các điểm đến
    [SerializeField] private GameObject[] waypoints;

    // Chỉ số của điểm đến hiện tại
    private int CurrentWayPointIndex = 0;

    // Tốc độ di chuyển
    [SerializeField] private float speed = 2f;

    void Update()
    {
        // Kiểm tra nếu đối tượng đã đến gần điểm đến hiện tại
        if (Vector2.Distance(waypoints[CurrentWayPointIndex].transform.position, transform.position) < .1f)
        {
            CurrentWayPointIndex++;
            if (CurrentWayPointIndex >= waypoints.Length)
            {
                CurrentWayPointIndex = 0;
            }
        }

        // Di chuyển đối tượng đến điểm đến hiện tại
        transform.position = Vector2.MoveTowards(transform.position, waypoints[CurrentWayPointIndex].transform.position, Time.deltaTime * speed);
    }
}
