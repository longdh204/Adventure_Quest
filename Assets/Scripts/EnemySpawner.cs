using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab của quái
    public float spawnInterval = 1f; // Khoảng thời gian giữa các lần sinh quái
    private CameraController cameraController; // Tham chiếu đến CameraController

    void Start()
    {
        cameraController = FindObjectOfType<CameraController>(); // Tìm CameraController trong scene
        StartCoroutine(SpawnEnemies()); // Bắt đầu coroutine sinh quái
    }

    IEnumerator SpawnEnemies()
    {
        while (true) // Vòng lặp vô hạn
        {
            // Lấy các giá trị min và max từ CameraController
            float minX = cameraController.MinX;
            float maxX = cameraController.MaxX;
            float minY = cameraController.MinY;
            float maxY = cameraController.MaxY;

            // Tạo vị trí sinh ra ngẫu nhiên trong khu vực xác định
            Vector3 spawnPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0f);
            Debug.Log("Vị trí sinh ra quái: " + spawnPosition); // In ra vị trí sinh ra

            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity); // Sinh quái

            yield return new WaitForSeconds(spawnInterval); // Chờ một khoảng thời gian trước khi sinh quái tiếp theo
        }
    }
}