using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class Prospector : MonoBehaviour {

	static public Prospector 	S;

	[Header("Set in Inspector")]
	public TextAsset			deckXML;
	public TextAsset layoutXML;
	public float xOffset=3; 
	public float yOffset= -2.5f; 
	public Vector3 layoutCenter; 


	[Header("Set Dynamically")]
	public Deck					deck;
	public Layout layout;
	public List<CardProspector> drawPile; 
	public Transform layoutAnchor;
	public CardProspector target;
	public List<CardProspector> tableau;
	public List<CardProspector> discardPile; 

	void Awake(){
		S = this;
	}

	void Start() {
		deck = GetComponent<Deck> ();
		deck.InitDeck (deckXML.text);
		Deck.Shuffle(ref deck.cards);

		layout=GetComponent<Layout>();
		layout.ReadLayout(layoutXML.text);
		drawPile= ConvertListCardsToListCardProspectors(deck.cards);
		LayoutGame();
	}
	List<CardProspector>ConvertListCardsToListCardProspectors(List<Card> lCD){
		List<CardProspector> lCP= new List<CardProspector>();
		CardProspector tCP;
		foreach(Card tCD in lCD){
			tCP=tCD as CardProspector;
			lCP.Add(tCP);
		}
		return (lCP);
	}
	//the Draw Function will pull a sinle card from the drawPile and return it
	CardProspector Draw(){
		CardProspector cd=drawPile[0];
		drawPile.RemoveAt(0);
		return(cd);
	}
	//LayoutGame() positions the initial tableau of cards
	void LayoutGame(){
		//create an empty GameObject to serve as an anchor for the tableau
		if(layoutAnchor==null){
			GameObject tGO= new GameObject("LayoutAnchor");
			layoutAnchor=tGO.transform;
			layoutAnchor.transform.position= layoutCenter;
		}
		CardProspector cp;
		//follow the layout
		foreach(SlotDef tSD in layout.slotDefs){
			//iterate through all the slotdefs in the layout.slotDefs as tSD
			cp=Draw();
			cp.faceUp=tSD.faceUp; //set its faceUp to the value in slotdef
			cp.transform.parent= layoutAnchor;
			cp.transform.localPosition= new Vector3(layout.multiplier.x*tSD.x, 
			layout.multiplier.y*tSD.y, -tSD.layerID);
			cp.layoutID=tSD.id;
			cp.slotDef=tSD;
			cp.state= eCardState.tableau;
			tableau.Add(cp);
				
			
		}
	}

}
