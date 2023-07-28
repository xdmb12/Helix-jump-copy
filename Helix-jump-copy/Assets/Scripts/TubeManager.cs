using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeManager : MonoBehaviour
{
    [Header("Tube Settings")]
    [SerializeField] private float rotationSpeed;
    
    [Header("Spawn Settings")]
    [SerializeField] private float ySpawn;
    [SerializeField] private float tubeLenght;
    [SerializeField] private int countOfTubes;
    [SerializeField] private GameObject[] tubes;
    [SerializeField] private Transform ball;
    
    private bool isRotating;
    private List<GameObject> activeTubes = new List<GameObject>();
    
    void Start()
    {
        for (int i = 0; i < countOfTubes; i++)
        {
            SpawnPlane(Random.Range(0, tubes.Length));
        }
    }

    void Update()
    {
        Rotation();
        
        if (-ball.position.y - (3 * tubeLenght) > ySpawn - (countOfTubes * tubeLenght))
        {
            SpawnPlane(Random.Range(0, tubes.Length));
            DeletePlain();
        }
    }

    private void Rotation()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isRotating = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isRotating = false;
        }

        if (isRotating)
        {
            float rotationX = Input.GetAxis("Mouse X") * rotationSpeed;

            transform.Rotate(Vector3.up, -rotationX, Space.World);
        }
    }
    
    private void SpawnPlane(int planeIndex)
    {
        float randomRotationY = Random.Range(0f, 360f);
        Quaternion randomRotation = Quaternion.Euler(0f, randomRotationY, 0f);
        
        GameObject plane = Instantiate(tubes[planeIndex], -transform.up * ySpawn, randomRotation, gameObject.transform);
        activeTubes.Add(plane);
        ySpawn += tubeLenght;
    }

    private void DeletePlain()
    {
        Destroy(activeTubes[0]);
        activeTubes.RemoveAt(0);
    }
}
