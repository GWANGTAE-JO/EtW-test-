using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    GameObject enemyPrefabs;
    GameObject enemy;
    Transform spawnTransform;
    
    private List<GameObject> enemyList;
    void Start()
    {
        enemy = enemyPrefabs;

        enemyList = new List<GameObject>();
        StartCoroutine("SpawnEnemy");
    }

    void Update()
    {
        if (PlayerManager.instance.gameOver) {
            foreach (GameObject tGO in enemyList) {
                Destroy(tGO);
            }
        }
    }

    private IEnumerator SpawnEnemy() {

        while(true) {
            float posX = Random.Range(-10f, 10f);
            float posY = Random.Range(-6f, 6f);

            Vector2 spawnpos = new Vector2(posX, posY);
            
            GameObject enemyGO = Instantiate<GameObject>(enemy, spawnpos, Quaternion.identity);
            enemyList.Add(enemyGO);

            yield return new WaitForSeconds(5);
        }
    }
}
