using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Card : MonoBehaviour {

	[Header("Set Dynamically")]
	public string    suit;
	public int       rank;
	public Color     color = Color.black;
	public string    colS = "Black";  // or "Red"
	
	public List<GameObject> decoGOs = new List<GameObject>();
	public List<GameObject> pipGOs = new List<GameObject>();
	
	public GameObject back;  // back of card;
	public CardDefinition def;  // from DeckXML.xml		

	public SpriteRenderer[] spriteRenderers;

	void Start(){
		SetSortOrder(0); //ensure that the card starts properly depth sorted
	}

	//if spriteRenderers is not yet defined, this function defines it
	public void PopulateSpriteRenderers(){
		if(spriteRenderers==null||spriteRenderers.Length==0){
			spriteRenderers=GetComponentsInChildren<SpriteRenderer>();
		}
	}

	//sets the sortingLayerName on all SpriteRenderer Components
	public void SetSortingLayerName(string tSLN){
		PopulateSpriteRenderers();
		foreach(SpriteRenderer tSR in spriteRenderers){
			tSR.sortingLayerName=tSLN;
		}
	}

	//sets the sortingOrder of all SpriteRenderer Components
	public void SetSortOrder(int sOrd){
		PopulateSpriteRenderers();
		//iterate through all the  spriteRenderes as tSR
		foreach(SpriteRenderer tSR in spriteRenderers){
			if(tSR.gameObject==this.gameObject){
				tSR.sortingOrder=sOrd;
				continue;
			}
			switch(tSR.gameObject.name){
				case "back": //if the name is "back"
					//set it to the highest layer to cover the other sprites
					tSR.sortingOrder=sOrd+2;
					break;
				case "face": //if the name is "face"
				default: //of if it's anything else
					//Set it to the middle layer to be above the background
					tSR.sortingOrder=sOrd+1;
					break;
			}
			
		}
	}


	public bool faceUp {
		get {
			return (!back.activeSelf);
		}

		set {
			back.SetActive(!value);
		}
	}

	//VIRTUAL METHODS CAN BE OVERRIDDEN BY SUBCLASS METHODS WITH THE SAME NAME
	virtual public void OnMouseUpAsButton(){
		print(name); //when clicked, this outputs the card name
	}
	
	
	// Update is called once per frame
	void Update () {
	
	}
} // class Card

[System.Serializable]
public class Decorator{
	public string	type;			// For card pips, tyhpe = "pip"
	public Vector3	loc;			// location of sprite on the card
	public bool		flip = false;	//whether to flip vertically
	public float 	scale = 1.0f;
}

[System.Serializable]
public class CardDefinition{
	public string	face;	//sprite to use for face cart
	public int		rank;	// value from 1-13 (Ace-King)
	public List<Decorator> pips = new List<Decorator>();  // Pips Used
}
