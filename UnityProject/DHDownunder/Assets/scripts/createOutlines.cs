/* create country outlines as map mapped to sphere
 * country outline point data from R-Studio'
 * 
 * This takes quite a bit of time to read all the data in and is not super efficient
 * but once read and drawn should not effect performance
 * 
 * will only draw regions/countries that are added to the countryName array
 * these musty exactly match the region field in the data set
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class createOutlines : MonoBehaviour
{
    // file name for data set for map polygons
    // set in inspector
    // This file must be in the StreamingAssets folder
    public string filename = "outlines.csv";

    // list to hold the data once read in
    List<Dictionary<string, object>> theOutlineData;

    public Vector3[] linePoints;
    public float lastGroup = 0;
    public string lastRegion = "";

    public Material lineMaterial;

    LineRenderer theLineRenderer;

    public string[] countryName;

    // flag to check if you just want everything to be drawn
    // with the current line drawing method this is quite laggy
    public bool drawWholeWorld = false;

    // the map scale
    public int scaleX;
    public int scaleY;

    // wait a fraction of a second on play before loading data
    // a bit of a hack to makes sure everything else is setup first
    void Start()
    {
        Invoke("loadData", 0.1f);
    }

    // load in and draw the map
    private void loadData()
    {
        //file path
        string dataFilePath = Path.Combine(Application.streamingAssetsPath, filename);

        int count = 0;

        if (File.Exists(dataFilePath))
        {
            string dataAsJson = File.ReadAllText(dataFilePath);
            // theOutlineData = JsonHelper.FromJson<outlineData>(dataAsJson);

            if (File.Exists(dataFilePath))
            {
                theOutlineData = CSVReader.Read(dataFilePath);
            }

            print("Read");

            //print("data length: " + theOutlineData.Count);

            linePoints = new Vector3[theOutlineData.Count];
            bool drawThisOne = false;

            for (int i = 0; i < theOutlineData.Count; i++)
            {
                //print(i);
                float thisLat = float.Parse(theOutlineData[i]["lat"].ToString());
                float thisLon = float.Parse(theOutlineData[i]["lon"].ToString());
                float thisGroup = float.Parse(theOutlineData[i]["group"].ToString());

                float radius = 200;

                if(lastGroup != thisGroup && lastGroup != 0 && drawThisOne)
                {
                    GameObject newGameObject = new GameObject(lastRegion);
                    theLineRenderer = newGameObject.gameObject.AddComponent<LineRenderer>() as LineRenderer;
                    theLineRenderer.material = lineMaterial;
                    //theLineRenderer.SetWidth(0.01f, 0.01f);
                    theLineRenderer.startWidth = 0.05f;
                    theLineRenderer.endWidth = 0.05f;
                    theLineRenderer.generateLightingData = true;
                    theLineRenderer.positionCount = count;
                    theLineRenderer.SetPositions(linePoints);
                    count = 0;
                }

                // this is for putting the outline on a sphere
                // currently we want it on a flat map :(
                /*
                float phi = (90 - thisLat) * (Mathf.PI / 180);
                float theta = (thisLon + 180) * (Mathf.PI / 180);
                float z = -((radius) * Mathf.Sin(phi) * Mathf.Cos(theta));
                float x = ((radius) * Mathf.Sin(phi) * Mathf.Sin(theta));
                float y = ((radius) * Mathf.Cos(phi));
                */

                float y = 0.01f;
                float[] thisXY = helpers.getXYPos(thisLat, thisLon, scaleX, scaleY);
                float x = thisXY[0];
                float z = thisXY[1];

                Vector3 thisPos = new Vector3(x, y, z);

                if (System.Array.IndexOf(countryName, theOutlineData[i]["region"]) != -1 || drawWholeWorld)
                {
                    linePoints[count] = thisPos;
                    //print(thisLat + " " + thisLon + " " + thisGroup + " " + theOutlineData[i].region);
                    drawThisOne = true;
                    count++;
                }
                else
                {
                    drawThisOne = false;
                }

                lastGroup = thisGroup;
                lastRegion = theOutlineData[i]["region"].ToString();

            }

            print("done"); 

        }
    }


}