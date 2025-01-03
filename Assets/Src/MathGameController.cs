using UnityEngine;
using UnityEngine.UI;

public class MathGameController : MonoBehaviour
{
    public GameObject cubePrefab;  // Prefab Cube untuk jawaban
    // public Text questionText;      // Teks untuk menampilkan soal matematika
    // public Text timerText;         // Teks untuk menampilkan timer
    // public Text scoreText;         // Teks untuk menampilkan skor

    private int score = 0;         // Skor pemain
    private float timeLimit = 10f; // Batas waktu untuk menjawab
    private float timeRemaining;   // Sisa waktu
    private bool questionActive = false; // Apakah soal aktif
    private int correctCubeIndex;  // Index cube yang benar
    private GameObject[] cubes = new GameObject[3]; // Array untuk cube jawaban

    private Vector3 spawnPosition1 = new Vector3(-3.72f, -1.3f, 2.49f); // Posisi spawn cube pertama
    private Vector3 spawnPosition2 = new Vector3(0f, -1.3f, 2.49f);   // Posisi spawn cube kedua
    private Vector3 spawnPosition3 = new Vector3(3.72f, -1.3f, 2.49f); // Posisi spawn cube ketiga

    void Start()
    {
        SpawnCubes();
        StartNewQuestion();
    }

    void Update()
    {
        if (questionActive)
        {
            timeRemaining -= Time.deltaTime;
            // timerText.text = "Time: " + Mathf.RoundToInt(timeRemaining).ToString();

            if (timeRemaining <= 0)
            {
                EndQuestion(false); // Jika waktu habis, dianggap salah
            }
        }
    }

    // Fungsi untuk Spawn 3 cubes
    void SpawnCubes()
    {
        // Menyebar cube di posisi yang telah ditentukan
        cubes[0] = Instantiate(cubePrefab, spawnPosition1, Quaternion.identity);
        cubes[1] = Instantiate(cubePrefab, spawnPosition2, Quaternion.identity);
        cubes[2] = Instantiate(cubePrefab, spawnPosition3, Quaternion.identity);

        // Setelah 3 cube pertama, spawn cube berikutnya dengan z bertambah 2.49 per cube
        for (int i = 3; i < 10; i++) // Contoh spawn 10 cube, kamu bisa sesuaikan jumlahnya
        {
            // Spawn posisi 1
            Vector3 spawnPosition1 = new Vector3(-3.72f, -1.3f, 2.49f * (i + 1)); // Z bertambah dengan kelipatan 2.49
            GameObject cube1 = Instantiate(cubePrefab, spawnPosition1, Quaternion.identity);

            // Tambahkan BoxCollider jika belum ada
            if (cube1.GetComponent<BoxCollider>() == null)
            {
                cube1.AddComponent<BoxCollider>();
            }

            // Menambahkan text pada cube
            Text cubeText1 = cube1.GetComponentInChildren<Text>();
            if (cubeText1 != null)
            {
                cubeText1.text = "Answer " + (i + 1);  // Ganti dengan angka acak atau soal sesuai kebutuhan
            }

            // Spawn posisi 2
            Vector3 spawnPosition2 = new Vector3(0f, -1.3f, 2.49f * (i + 1)); // Z bertambah dengan kelipatan 2.49
            GameObject cube2 = Instantiate(cubePrefab, spawnPosition2, Quaternion.identity);

            // Tambahkan BoxCollider jika belum ada
            if (cube2.GetComponent<BoxCollider>() == null)
            {
                cube2.AddComponent<BoxCollider>();
            }

            // Menambahkan text pada cube
            Text cubeText2 = cube2.GetComponentInChildren<Text>();
            if (cubeText2 != null)
            {
                cubeText2.text = "Answer " + (i + 1);  // Ganti dengan angka acak atau soal sesuai kebutuhan
            }

            // Spawn posisi 3
            Vector3 spawnPosition3 = new Vector3(3.72f, -1.3f, 2.49f * (i + 1)); // Z bertambah dengan kelipatan 2.49
            GameObject cube3 = Instantiate(cubePrefab, spawnPosition3, Quaternion.identity);

            // Tambahkan BoxCollider jika belum ada
            if (cube3.GetComponent<BoxCollider>() == null)
            {
                cube3.AddComponent<BoxCollider>();
            }

            // Menambahkan text pada cube
            Text cubeText3 = cube3.GetComponentInChildren<Text>();
            if (cubeText3 != null)
            {
                cubeText3.text = "Answer " + (i + 1);  // Ganti dengan angka acak atau soal sesuai kebutuhan
            }
        }
    }

    // Fungsi untuk memulai soal baru
    void StartNewQuestion()
    {
        questionActive = true;
        timeRemaining = timeLimit;

        int num1 = Random.Range(1, 10);
        int num2 = Random.Range(1, 10);
        int correctAnswer = num1 + num2;

        // questionText.text = num1 + " + " + num2 + " = ?";

        correctCubeIndex = Random.Range(0, cubes.Length);

        // Mengisi cube dengan angka-angka
        for (int i = 0; i < cubes.Length; i++)
        {
            Text cubeText = cubes[i].GetComponentInChildren<Text>();
            if (i == correctCubeIndex)
            {
                cubeText.text = correctAnswer.ToString(); // Cube yang benar
            }
            else
            {
                cubeText.text = Random.Range(1, 20).ToString(); // Cube yang salah
            }
        }
    }

    // Fungsi untuk mengakhiri soal
    void EndQuestion(bool correct)
    {
        questionActive = false;

        if (correct)
        {
            score++;
            // scoreText.text = "Score: " + score.ToString();
        }

        // Setelah soal selesai, spawn lagi
        Invoke("SpawnCubes", 1f);
        Invoke("StartNewQuestion", 1f);
    }

    // Fungsi dipanggil saat pemain memilih cube
    public void PlayerLandsOnCube(int cubeIndex)
    {
        if (!questionActive) return;

        if (cubeIndex == correctCubeIndex)
        {
            EndQuestion(true); // Jika cube yang dipilih benar
        }
        else
        {
            EndQuestion(false); // Jika cube yang dipilih salah
            cubes[cubeIndex].GetComponent<Rigidbody>().isKinematic = false; // Cube jatuh
        }
    }
}
