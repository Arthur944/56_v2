using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overseer : MonoBehaviour {
    Ray ray;
    RaycastHit hit;
    AreaScript myScript;
    bool needTurningBack = false;
    bool itsMyFirstTimeSenpai = true;
    public Material fekete;
    public Material feher;
    public Material aktiv;

    public GameObject[] Areas;
    int activeArea = 100;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.tag == "Area") //Ha az egér egy terület felett van...
        {
            if (itsMyFirstTimeSenpai) //Please be gentle
            {
                myScript = hit.collider.gameObject.GetComponent<AreaScript>(); //myScript az azé a területé, amelyik fölött az egér van.
                if (myScript.id != activeArea)
                {
                    myScript.brigthenColor(feher);  //Az egér alatti területet szinezzük fehérre.
                }
                needTurningBack = true; //Jelöljük hogy szükségünk lesz a terület visszaszínezésére feketére amikor levisszük róla az egeret.
                itsMyFirstTimeSenpai = false; //( ͡° ͜ʖ ͡°)
            }
            
            if (Input.GetMouseButton(0))    //Ha rákattintunk egy területre, aktívvá tesszük.
            {
                if (activeArea != 100)      //Mielőtt aktív területet váltunk, szinezzük vissza az előző aktív terület szomszédait. Ezt akkor ne tegyük meg ha ez az első aktív terület.
                {
                    setNeighbourColors(fekete);
                    Areas[activeArea - 1].GetComponent<AreaScript>().changeColorTo(fekete);
                }
                activeArea = myScript.getId(); //Felvesszük az aktív terület számát
                setNeighbourColors(feher);  //A szomszédok színeit fehérre váltjuk.
                myScript.changeColorTo(aktiv); //Az aktív területet szinezzük accordingly
                needTurningBack = false;    //És ne szinezzük vissza ha lemegy az egér.
            }
        }
        else
        {
            itsMyFirstTimeSenpai = true; //I don't think that's how it works in real life
            if (needTurningBack && myScript.id != activeArea) //Ha az egér már nincs a terület fölött, váltsuk vissza a színét.
            {
                myScript.resetColor();
                needTurningBack = false;
            }
        }
    }

    void setNeighbourColors(Material mat)
    {
        Debug.Log("Active area: " + activeArea);
        int[] neighbours = Areas[activeArea - 1].GetComponent<AreaScript>().getNeighbours();
        int i = 0;
        while (i < neighbours.Length)
        {
            Areas[neighbours[i] - 1].GetComponent<AreaScript>().changeColorTo(mat);
            i++;
        }
    }

    int[,] activeAreaStats()
    {
        int[,] stats = new int[Areas[activeArea].GetComponent<AreaScript>().resistance, Areas[activeArea].GetComponent<AreaScript>().control];
        return stats;
    }

  
}
