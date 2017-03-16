using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaScript : MonoBehaviour {
    public int id;
    public int[] neighbours;
    public int resistance = 50;
    public int control = 50;
    Material oldColor;

    Ray ray;
    RaycastHit hit;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void changeColorTo(Material mat)
    {
        GetComponent<Renderer>().material = mat;
    }

    public void brigthenColor(Material feher)
    {
        oldColor = GetComponent<Renderer>().material;
        GetComponent<Renderer>().material = feher;
    }

    public void resetColor()
    {
        GetComponent<Renderer>().material = oldColor;
    }

    public int[] getNeighbours()
    {
        return neighbours;
    }

    public int getId()
    {
        return id;
    }
}
