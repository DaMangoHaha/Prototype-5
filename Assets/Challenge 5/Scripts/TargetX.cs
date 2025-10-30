using System.Collections;
using UnityEngine;

public class TargetX : MonoBehaviour
{
    private Rigidbody rb;
    private GameManagerX gameManagerX;
    public int pointValue;
    public GameObject explosionFx;

    public float timeOnScreen = 1.0f;

    private float minValueX = -3.75f; // the x value of the center of the left-most square
    private float minValueY = -3.75f; // the y value of the center of the bottom-most square
    private float spaceBetweenSquares = 2.5f; // the distance between the centers of squares on the game board

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameManagerX = GameObject.Find("Game Manager").GetComponent<GameManagerX>();

        transform.position = RandomSpawnPosition();
        StartCoroutine(RemoveObjectRoutine()); // begin timer before target leaves screen

    }

    // When target is clicked, destroy it, update score, and generate explosion
    private void OnMouseDown()
    {
        if (gameManagerX.isGameActive)
        {
            Destroy(gameObject);
            Explode();
        }

    }

    // Generate a random spawn position based on a random index from 0 to 3
    Vector3 RandomSpawnPosition()
    {
        float spawnPosX = minValueX + (RandomSquareIndex());
        float spawnPosY = minValueY + (RandomSquareIndex());

        Vector3 spawnPosition = new Vector3(spawnPosX, spawnPosY, 0);
        return spawnPosition;

    }

    int RandomSquareIndex()
    {
        return Random.Range(0, 8);
    }


    // If target that is NOT the bad object collides with sensor, trigger game over
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        gameManagerX.GameOver();
    }

    // Display explosion particle at object's position
    void Explode()
    {
        Instantiate(explosionFx, transform.position, explosionFx.transform.rotation);
    }

    // After a delay, Moves the object behind background so it collides with the Sensor object
    IEnumerator RemoveObjectRoutine()
    {
        yield return new WaitForSeconds(timeOnScreen);
        transform.Translate(Vector3.forward * 5, Space.World);
    }
}