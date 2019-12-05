// sample of reading data using the CSVReader class
// uses a CSV file directly exported from: https://docs.google.com/spreadsheets/d/11EP0cDRxu1Pa3kK0OVMwSt1zpXIdRYSv-T66sj-mrrs/edit?usp=sharing
// which is an part of data set for the word for 'dog' extracted from Simon Greenhill's 'Austronesian basic vocab database' 
// in this case the data file is called data.csv and in located in the **StreamingAssets** folder


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class readData : MonoBehaviour
{
    // list to hold data
    List<Dictionary<string, object>> data;

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
        for (var i = 0; i < data.Count; i++)
        {
            print(i+": ");
            print(data[i]["latitude"]);
            print(data[i]["longitude"]);
            print(data[i]["dog"]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
