using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab của quái
    public GameObject warningPrefabs; // Prefab của cảnh báo
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

            // Hien thi vong bao hieu quai xuat hien
            GameObject warning = Instantiate(warningPrefabs, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(0.5f); // Chờ một khoảng thời gian trước khi sinh quái

            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity); // Sinh quái
            StartCoroutine(ShrinkAndDestroy(warning, 3f)); // Zoom nhỏ warning trong 0.5s rồi xóa

            yield return new WaitForSeconds(spawnInterval); // Chờ một khoảng thời gian trước khi sinh quái tiếp theo
        }
    }
    private IEnumerator ShrinkAndDestroy(GameObject obj, float shrinkDuration)
    {
        Vector3 originalScale = obj.transform.localScale;
        float timer = 0f;
        while (timer < shrinkDuration)
        {
            float t = timer / shrinkDuration;
            obj.transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, t);
            timer += Time.deltaTime;
            yield return null;
        }
        obj.transform.localScale = Vector3.zero;
        Destroy(obj);
    }
}