using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using System.Text.RegularExpressions;



public class RouteDropdown : MonoBehaviour {
  List<string> routesList = new List<string>();
  // Use this for initialization
  void Start()
  {
    Dropdown routeDropdown = GameObject.Find("RouteDropdown").GetComponent<Dropdown>();
    string text = File.ReadAllText("../COTA World/OpenGTFSData/routes.txt");
    char[] separators = { ';', '|', '\n' };
    string[] strValues = text.Split(separators);
    
    routeDropdown.options.Clear();
    for (int i = 1; i < strValues.Length - 1; i++)
    {
      string[] lineValues = strValues[i].Split(',');
      routeDropdown.options.Add(new Dropdown.OptionData(lineValues[2] + " - " + lineValues[3]));
      routesList.Add(lineValues[0]);
      Debug.Log("Line "+i+" route_id: "+lineValues[0]);
    }
    //Match trips
    //"routes.txt".lineValues[0]    >>>   dropdown.value   >>>>>
    //>>>  "trips.txt".lineValues[0]     >>>   "trips.txt".lineValues[2] (tripLine_ID)  >>>    Need to get to "stop_times".lineValues[0]           

    //MapSelectedRoute(routeDropdown.value);
  }
  public void MatchRouteStop()
  {
    int selectedRoute = GameObject.Find("RouteDropdown").GetComponent<Dropdown>().value;
    string route = routesList[selectedRoute];
    Debug.Log("route_id: " + route);
    string text = File.ReadAllText("../COTA World/OpenGTFSData/trips.txt");
    char[] separators = { ';', '|', '\n' };
    string[] strValues = text.Split(separators);
    for (int i = 1; i < strValues.Length - 1; i++)
    {
      string[] lineValues = strValues[i].Split(',');
      //int tripLine = Int32.Parse(lineValues[2]);
      if (lineValues[0] == route)
      {
        //MapSelectedRoute(lineValues[2]);
        Debug.Log("Mapping line " + lineValues[2] + " of selected route " + route);
      }
    }
  }
  void MapSelectedRoute(string selectedRoute)
  {
    Debug.Log("Loading tripLine: " + selectedRoute);
    string text = File.ReadAllText("../COTA World/OpenGTFSData/stop_times.txt");
    int previousStop = 0;
    char[] separators = { ';', '|', '\n' };
    string[] strValues = text.Split(separators);
    for (int i = 1; i < 10 - 1; i++)
    {
      Debug.Log("We made it into the for loop!");
      string[] lineValues = strValues[i].Split(',');
      int currentStop = Int32.Parse(lineValues[0]);
      if (selectedRoute == lineValues[0])
      {
        Debug.Log("We're in the first if statement!");

        GameObject trip;
        LineRenderer tripLine;
        if (currentStop != previousStop)
        {
          Debug.Log("We're in the second if statement!");

          trip = new GameObject();
          trip.name = lineValues[0];
          tripLine = trip.AddComponent<LineRenderer>();
          tripLine.positionCount = 1;
        }
        else
        {
          Debug.Log("We're in the else of the second if statement!");

          tripLine = GameObject.Find(lineValues[0]).GetComponent<LineRenderer>();
          tripLine.positionCount++;
        }
        Vector3 station = GameObject.Find(lineValues[3]).GetComponent<Transform>().position;
        tripLine.SetPosition(tripLine.positionCount - 1, station + new Vector3(0, timeHeight(lineValues[1]) / 100, 0));
        Renderer rend = tripLine.GetComponent<Renderer>();
        Color randomColor = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value, 1.0f);
        rend.material.color = randomColor;
        rend.material.SetColor("_EmissionColor", randomColor);
        previousStop = currentStop;
      }
      Debug.Log("We're on our way out!");
    }
  }
  int timeHeight(string time)
  {
    string[] vars = time.Split(':');
    int height = Int32.Parse(vars[0]) * 24 * 60 + Int32.Parse(vars[1]) * 60 + Int32.Parse(vars[2]);
    return height;
  }

  // Update is called once per frame
  void Update () {
		
	}
}
