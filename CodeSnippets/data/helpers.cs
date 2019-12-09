using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class helpers
{

    //
    // getXYPos
    // given a lat and long will map to catresian coordinates on a Mercator style projection
    // see: https://stackoverflow.com/questions/1369512/converting-longitude-latitude-to-x-y-on-a-map-with-calibration-points
    //
    public static float[] getXYPos(float lat, float lon, float scaleX, float scaleY)
    {
        float[] xy = new float[2];
        float x = (scaleX * lon / 180) - 180;
        float y = (scaleY * lat / 360);
        xy[0] = x;
        xy[1] = y;
        return xy;
    }

    //
    // getPointOnSphere
    // given a lat and long will map to coordinates on a sphere of a set radius
    // see: https://stackoverflow.com/questions/36369734/how-to-map-latitude-and-longitude-to-a-3d-sphere
    //
    public static Vector3 getPointOnSphere(float lat, float lon, float radius)
    {
      float phi = (90 - lat) * (Mathf.PI / 180);
      float theta = (lon + 180) * (Mathf.PI / 180);
      float z = -((radius) * Mathf.Sin(phi) * Mathf.Cos(theta));
      float x = ((radius) * Mathf.Sin(phi) * Mathf.Sin(theta));
      float y = ((radius) * Mathf.Cos(phi));

      return new Vector3(x, y, z);
    }

    //
    // remap a value from one range to another
    // similar to processings map() function
    //
    public static float Remap(this float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }


}
