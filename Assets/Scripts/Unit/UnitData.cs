using System.Collections.Generic;

[System.Serializable]
public class UnitData
{
    public string id;
    public string name;
    public BodyData body;
    public Attributes attributes;
    public Skills skills;
    public EquippedItems equippedItems;
    public HeldItems heldItems;
    public List<Item> carriedItems;
    public MapPosition map;
    public TimeUnits timeUnits;
    public int initiative;
    public bool isPlayerControlled;
}
