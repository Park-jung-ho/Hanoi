using System;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public int Num;
    public Color color;

    private void Start()
    {
        GetComponent<Renderer>().material.SetColor("_Top_Color", color);
        GetComponent<Renderer>().material.SetColor("_Side_Color", color);
    }
}
