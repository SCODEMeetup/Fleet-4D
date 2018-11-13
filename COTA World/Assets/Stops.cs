using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using System.Text.RegularExpressions;


public class Stops : MonoBehaviour {
  // Use this for initialization
  void Start()
  {
    string text = File.ReadAllText("../COTA World/OpenGTFSData/stops.txt");
    char[] separators = { ';', '|', '\n' };
    string[] strValues = text.Split(separators);
    for (int i = 1; i < strValues.Length - 1; i++)
    {
      string[] lineValues = strValues[i].Split(',');
      GameObject stop = GameObject.CreatePrimitive(PrimitiveType.Cube);
      Renderer rend = stop.GetComponent<Renderer>();
      rend.material.color = Color.white;
      rend.material.SetColor("_EmissionColor", Color.white);
      stop.name = lineValues[0];
      //Debug.Log(lineValues[0] + "," + lineValues[lineValues.Length-8] + "," + lineValues[lineValues.Length - 7]);
      stop.transform.position = new Vector3(1000 * float.Parse(lineValues[lineValues.Length - 8]), 0, 1000 * float.Parse(lineValues[lineValues.Length - 7]));
    }
  }

  // Update is called once per frame
  void Update()
  {

  }
}
