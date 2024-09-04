using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureCenterPoints
{
    public Vector3 FindTriangleCenterCoordinates(Vector3 a, Vector3 b, Vector3 c)
    {
        //Calculate centroid coordinates
        var x = (a.x + b.x + c.x) / 3;
        var y = (a.y + b.y + c.y) / 3;

        var centroidCoordinates = new Vector3(x, y);
        return centroidCoordinates;
    }

}
