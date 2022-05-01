using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//generat enemy based on score (time) player get
public class EnemyGenerator : MonoBehaviour
{
    private ScoreManager scoreManager;
    private Collider2D worldBound;
    [SerializeField] private GameObject sineEnemy, flyEnemy;
    [SerializeField] private float sineSpawnY, flySpawnY;
    private int[] difficultyScore = new int[20]; //array of score to increase difficulty
    private float spawnRate = 1;
    private bool isSpawned = false;
    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        worldBound = FindObjectOfType<WorldBound>().GetComponent<Collider2D>();

        for (int i = 0; i < difficultyScore.Length; i++) {
            difficultyScore[i] = (i+1)*5; 
        }
    }

    
    void Update()
    {
        //decides how many enemy should be spawned
        for (int i = 0; i < difficultyScore.Length; i++) {
            if ((int)scoreManager.Score == difficultyScore[i]) {
                spawnRate += 0.4f;
                difficultyScore[i] = 0;
            }
        }

        //spawn enemies
        if (((int)scoreManager.Score)%10 == 0 && !isSpawned && FindObjectOfType<PlayerControlArcade>() != null) {
            for (int i = 0; i < (int)spawnRate; i++) {
                int randomChoose = (int)Random.Range(0, 1.99f);
                //spawn from left
                if (randomChoose == 0) {
                    GameObject sin = Instantiate(sineEnemy, new Vector2(worldBound.bounds.min.x - 1+ worldBound.transform.position.x, sineSpawnY+ worldBound.transform.position.y), Quaternion.identity);
                    sin.GetComponent<SineEnemy>().IsMovingRight = true;
                    Instantiate(flyEnemy, new Vector2(worldBound.bounds.min.x - 1+ worldBound.transform.position.x, flySpawnY+ worldBound.transform.position.y), Quaternion.identity);
                } else { //spawn from right
                    GameObject sin = Instantiate(sineEnemy, new Vector2(worldBound.bounds.max.x + 1+worldBound.transform.position.x, sineSpawnY+ worldBound.transform.position.y), Quaternion.identity);
                    sin.GetComponent<SineEnemy>().IsMovingRight = false;
                    Instantiate(flyEnemy, new Vector2(worldBound.bounds.max.x + 1+worldBound.transform.position.x, flySpawnY+ worldBound.transform.position.y), Quaternion.identity);
                }
            }
            StartCoroutine(waitToSpawn());
        }
    }

    //let enemy only spawn one frame
    IEnumerator waitToSpawn() {
        isSpawned = true;
        yield return new WaitForSeconds(1.5f);
        isSpawned = false;
    }
}
