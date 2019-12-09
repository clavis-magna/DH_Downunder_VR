// sample of reading data using the CSVReader class
// uses a CSV file directly exported from: https://docs.google.com/spreadsheets/d/11EP0cDRxu1Pa3kK0OVMwSt1zpXIdRYSv-T66sj-mrrs/edit?usp=sharing
// which is an part of data set for the word for 'dog' extracted from Simon Greenhill's 'Austronesian basic vocab database'
// Developed for the Layered Horizons project. 
// in this case the data file is called data.csv and in located in the **StreamingAssets** folder

// this verison will display data on the map by instatiating some text at given locations from the CSV.


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class readDataAndDisplay : MonoBehaviour
{
    // list to hold data
    List<Dictionary<string, object>> data;
    TextMeshPro theText;

    public GameObject locationMarker;

    // csv filename
    // we will look for it in the StreamingAssets folder (include .csv extension)
    // can change this in the inspector
    public string CSVFileName = "data.csv";



    // Start is called before the first frame update
    void Start()
    { 
        
        // create the file path to the CSV data as a string
        // you would change this if you wanted to look for the file elsewhere
        string dataFilePath = Path.Combine(Application.streamingAssetsPath, CSVFileName);

        if (File.Exists(dataFilePath))
        {
            data = CSVReader.Read(dataFilePath);
        }

        // test read of data
        // we iterate through all the data and print it out to the console
        // we will use lat, long and dog columns to display the data on a map
        for (var i = 0; i < data.Count; i++)
        {

            float lat = float.Parse(data[i]["latitude"].ToString());
            float lon = float.Parse(data[i]["longitude"].ToString());
            string dogWord = data[i]["dog"].ToString();

            Vector3 positionToInstatiate = helpers.getPointOnSphere(lat, lon, 200);
            GameObject newMarker = Instantiate(locationMarker, positionToInstatiate, Quaternion.identity);
            theText = newMarker.GetComponentInChildren<TextMeshPro>();
            theText.text = dogWord;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
