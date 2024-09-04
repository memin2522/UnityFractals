using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class SierspinskiFractal : MonoBehaviour
{
    [SerializeField] private ObjectPool pool;
    [SerializeField] private int fractalSize;
    [SerializeField] private FractalDimension dimension;
    [SerializeField] private CameraPositionController positionController;

    public List<GameObject> figureVertex = new List<GameObject>();
    public List<GameObject> fractalObjects = new List<GameObject>();

    public Vector3 currentPosition;
    private bool isInit = false;

    void Start()
    {
        if(dimension == FractalDimension.Two_Dimension)
        {
            CreateTriangleVertex(fractalSize);
        }
        else 
        {
            CreatePiramidVertex(fractalSize);
        }
    }

    private void Update()
    {
        if (!isInit) { return ; }

        FractalIteration();
    }

    void CreateTriangleVertex(int size)
    {
        for (int i = 0; i < 3; i++)
        {
            figureVertex.Add(pool.GetObjectFromPool("Sphere"));
        }
        figureVertex[0].transform.position = new Vector3(0, 0, 0);
        figureVertex[1].transform.position = new Vector3(size, 0, 0);
        figureVertex[2].transform.position = new Vector3(size / 2, (Mathf.Sqrt(3) / 2) * size, 0);

        currentPosition = figureVertex[0].transform.position;
        positionController.SetCameraPosition(figureVertex[0].transform.position, figureVertex[1].transform.position, figureVertex[2].transform.position, fractalSize);
        isInit = true;
    }

    void CreatePiramidVertex(int size)
    {
        for (int i = 0; i < 5; i++)
        {
            figureVertex.Add(pool.GetObjectFromPool("Sphere"));
        }
        figureVertex[0].transform.position = new Vector3(0, 0, 0);
        figureVertex[1].transform.position = new Vector3(size, 0, 0);
        figureVertex[2].transform.position = new Vector3(0, 0, size);
        figureVertex[3].transform.position = new Vector3(size, 0, size);
        figureVertex[4].transform.position = new Vector3(size/2, (Mathf.Sqrt(3) / 2) * size, size/2);

        currentPosition = figureVertex[0].transform.position;
        positionController.SetCameraPosition(
            figureVertex[0].transform.position, // (0, 0, 0)
            figureVertex[1].transform.position, // (size, 0, 0)
            figureVertex[4].transform.position,
            fractalSize);

        isInit = true;
    }

    void FractalIteration()
    {
        //Select random vertex
        var randomIndex = UnityEngine.Random.Range(0, figureVertex.Count);
        Transform selectedVertex = figureVertex[randomIndex].transform;

        GameObject currentObject = pool.GetObjectFromPool("Sphere");
        fractalObjects.Add(currentObject);

        var posX = (currentPosition.x + selectedVertex.position.x) / 2;
        var posY = (currentPosition.y + selectedVertex.position.y) / 2;
        var posZ = (currentPosition.z + selectedVertex.position.z) / 2;

        if (dimension == FractalDimension.Two_Dimension)
        {
            currentObject.transform.position = new Vector3(posX, posY);
        }
        else
        {
            currentObject.transform.position = new Vector3(posX, posY, posZ);
            currentPosition.z = posZ;
        }
            
        currentPosition.x = posX;
        currentPosition.y = posY;
        
    }

    
}
