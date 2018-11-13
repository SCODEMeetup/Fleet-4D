using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using System.Text.RegularExpressions;


public class StopTimes : MonoBehaviour
{

  // Use this for initialization
  void Start()
  {
    string text = File.ReadAllText("../COTA World/OpenGTFSData/stop_times.txt");
    int previousStop = 0;
    char[] separators = { ';', '|', '\n' };
    string[] strValues = text.Split(separators);
    for (int i = 1; i < 3000; i++)
    {
      string[] lineValues = strValues[i].Split(',');
      int currentStop = Int32.Parse(lineValues[0]);

      GameObject trip;
      LineRenderer tripLine;
      if (currentStop != previousStop)
      {
        trip = new GameObject();
        trip.name = lineValues[0];
        tripLine = trip.AddComponent<LineRenderer>();
        tripLine.positionCount = 1;
      }
      else
      {
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
  }
  int timeHeight(string time)
  {
    string[] vars = time.Split(':');
    int height = Int32.Parse(vars[0]) * 24 * 60 + Int32.Parse(vars[1]) * 60 + Int32.Parse(vars[2]);
    return height;
  }
  // Update is called once per frame
  void Update()
    {
    }
}
