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
    //this is how you use the enum eCardSite

    public eCardSite state=eCardSite.drawpile;
    public List<CardProspector> hiddenBy= new List<CardProspector>();
    public int layoutID;
    public SlotDef slotDef;
}
