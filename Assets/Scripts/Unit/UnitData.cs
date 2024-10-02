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
    public int remainingTimeUnits;
    internal int maxTimeUnits;
    public int initiative;
    public bool isPlayerControlled;
}
