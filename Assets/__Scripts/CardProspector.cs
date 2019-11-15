using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eCardState{
        drawpile,
        tableau,
        target,
        discard
}

public class CardProspector : Card
{
    [Header("Set Dynamically: CardProspector")]
    //this is how you use the enum eCardState

    public eCardState state=eCardState.drawpile;
    public List<CardProspector> hiddenBy= new List<CardProspector>();
    public int layoutID;
    public SlotDef slotDef;

    //this allows the card to react to being clicked
    override public void OnMouseUpAsButton(){
        //call the cardclicked method on the Prospector singleton 
        Prospector.S.CardClicked(this);
        //also call the base class card.cs version of this method
        base.OnMouseUpAsButton();
    }
}
