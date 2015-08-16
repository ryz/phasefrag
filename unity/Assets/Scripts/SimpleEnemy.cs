using UnityEngine;
using System.Collections;

public class SimpleEnemy : MonoBehaviour
{

    public int HP = 1;

    public GameSystem gameSystem;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0)
            Death();
    }

    public void Hurt()
    {
        // Reduce the number of hit points by one.
        Debug.Log("hurt!");
        HP--;
    }

    public void Death()
    {
        Destroy(gameObject);
        Debug.Log("DESTROYED!");
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log(">>>>> TRIGGER MAH BOI");
            Death();
            //other.GetComponent<Enemy>().Hurt();
            gameSystem.AddScore(2);
        }

    }
}
