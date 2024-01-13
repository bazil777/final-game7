using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemyPrefab; // enemy prefab asset
    public int xAxis;
    public int zAxis;
    public int enemyCount; //will use this in for loop to increment and keep track

    public GameObject playerController; // allows us to assign our player

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemyDrop());
    }

    IEnumerator EnemyDrop()
    {
        //small amount of enemies sspawned but in the final game will be waves of more
        while (enemyCount < 2)
        {
            //spawn radius , small but in final game will be larger
            xAxis = Mathf.FloorToInt(Random.Range(400f, 420f));
            zAxis = Mathf.FloorToInt(Random.Range(33f, 44f));
            GameObject newEnemy = Instantiate(enemyPrefab, new Vector3(xAxis, 0.7043388f, zAxis), Quaternion.identity);

            // enemy will target what we select(our player)
            newEnemy.GetComponent<EnemyAI>().target = playerController;

            yield return new WaitForSeconds(4.5f);
            enemyCount++;
        }
    }
}

