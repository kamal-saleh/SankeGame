using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Food object
public class Food : MonoBehaviour
{

    public int points = 10;
    public int growthAmount = 1;
    public int lifetime = 5;
    float time;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Snake")
        {
            GameManager.instance.AddPoints(points);
            GameManager.instance.createdFood.Remove(gameObject);
            Destroy(gameObject);
            Grow();
        }
    }

    public void Update()
    {
        time = time + Time.deltaTime;
        if (time > lifetime)
        {
            GameManager.instance.createdFood.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    public void randomize()
    {
        float randomx, randomy, randomz;
        randomx = UnityEngine.Random.Range(-10.0f, 10.0f);
        randomy = UnityEngine.Random.Range(-10.0f, 10.0f);
        randomz = UnityEngine.Random.Range(0.5f, 10.0f);
        transform.position = new Vector3(randomx, randomz, randomy);
    }

    void Grow()
    {
        Snake sanke = GameObject.FindGameObjectWithTag("Snake").GetComponent<Snake>();
        for (int i = 0; i < growthAmount; i++)
        {
            GameObject newPart = Instantiate(sanke.bodyPrefab.gameObject);
            newPart.transform.position = sanke.bodyParts[sanke.bodyParts.Count - 1].position + new Vector3(0, 0, 0.7f);
            newPart.transform.parent = sanke.gameObject.transform;
            sanke.bodyParts.Add(newPart.transform);
        }
    }
}