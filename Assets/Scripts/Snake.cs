using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public float speed = 1f;
    public Transform bodyPrefab;

    [HideInInspector] public List<Transform> bodyParts = new List<Transform>();
    [HideInInspector] public List<Vector3> previousPositions = new List<Vector3>();

    void Start()
    {
        bodyParts.Add(transform);
        previousPositions.Add(transform.position);
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float diagonal = Input.GetAxis("Depth");

        Vector3 direction = new Vector3(horizontal, diagonal, vertical).normalized;
        Move(direction);

        GameManager.instance.HighlightNearestFood();
    }

    void Move(Vector3 direction)
    {
        transform.position += direction * speed * Time.deltaTime;

        for (int i = 0; i < bodyParts.Count; i++)
        {
            if (i == 0)
            {
                bodyParts[i].position += direction * speed * Time.deltaTime;
            }
            else
            {
                bodyParts[i].position = previousPositions.ElementAtOrDefault(i - 1);
            }
        }

        previousPositions.Insert(0, bodyParts[0].position);

        if (previousPositions.Count > bodyParts.Count)
        {
            previousPositions.RemoveAt(previousPositions.Count - 1);
        }
    }
}