using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameSystem : MonoBehaviour {


    [HideInInspector] public float scoreCount = 0;
    public Text score;

    public GameObject enemySpawn;


    void Awake()
    {
        StartCoroutine(Spawn());

        // Initialize UI Score label
        score.text = "Score: " + scoreCount;
    }

    // Use this for initialization
    void Start()
    {
	
	}
	
	// Update is called once per frame
	void Update()
    {
	
	}

    void FixedUpdate()
    {

    }

    public void AddScore(int scoreNum)
    {
        scoreCount += scoreNum;
        score.text = "Score: " + scoreCount;
    }

    // Enemy spawner
    IEnumerator Spawn()
    {
        while (true)
        {
            Vector3 spawnPosition = new Vector3(0.0f, 4.0f, 0.0f);
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(enemySpawn, spawnPosition, spawnRotation);
            yield return new WaitForSeconds(Random.Range(4.0f, 8.0f));
            Debug.Log("Spawned " + enemySpawn.name + "!");
        }
    }
}
