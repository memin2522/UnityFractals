using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositionController : MonoBehaviour
{
    [SerializeField] private GameObject _camera;
    [SerializeField] private GameObject _cameraPivot;

    [SerializeField] private float rotationSpeed = 20f;

    public void SetCameraPosition(Vector3 a, Vector3 b, Vector3 c, int fractalSize)
    {
        FigureCenterPoints figureCenterPoints = new FigureCenterPoints();

        var cameraPosition = figureCenterPoints.FindTriangleCenterCoordinates(a, b, c);
        _cameraPivot.transform.position = new Vector3(cameraPosition.x, cameraPosition.y);
        _camera.transform.localPosition = new Vector3(0, 0, fractalSize * -1.5f);
    }

    private void Update()
    {
        _cameraPivot.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

}
