using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] target;

    [SerializeField]
    private UIManager manager;

    public float spawnInterval = 2f; //make spawn interval smaller as time goes up

    public float minimumSpawnInterval = 1f;

    private float timer;

    public bool isGameOver = false;

    private void Start()
    {
        timer = 0f;
        StartCoroutine(SpawnRoutine());
    }

    private void Update()
    {
        if (manager != null)
        {
            if (manager.currentLives <= 0)
            {
                isGameOver = true;
            }
        }
        if (isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        timer += Time.deltaTime;
    }

    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(1.5f);
        while (!isGameOver)
        {
            float horizontalPosition = Random.Range(-9f, 9f);
            int i = Random.Range(0, 20);

            if(timer > 2f && spawnInterval >= minimumSpawnInterval)
            {
                spawnInterval -= 0.2f;
                timer -= 2f;
            }
            Vector3 position = new Vector3(horizontalPosition, -6f, -1f);
            if(i >= 10)
            {
                Instantiate(target[1], position, Quaternion.identity);
            }
            else
            {
                Instantiate(target[0], position, Quaternion.identity);
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

}
