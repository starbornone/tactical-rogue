using System.Collections.Generic;
using UnityEngine;

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
}

[System.Serializable]
public class BodyData
{
    public int age;
    public string gender;
    public float height;
    public string species;
    public float weight;
}

[System.Serializable]
public class Attributes
{
    public int strength;
    public int dexterity;
    public int stamina;
    public int charisma;
    public int manipulation;
    public int composure;
    public int intelligence;
    public int wits;
    public int resolve;
}

[System.Serializable]
public class Skills
{
    public int athletics;
    public int brawl;
    public int craft;
    public int drive;
    public int firearms;
    public int melee;
    public int larceny;
    public int stealth;
    public int survival;
    public int animalKen;
    public int etiquette;
    public int insight;
    public int intimidation;
    public int leadership;
    public int performance;
    public int persuasion;
    public int streetwise;
    public int subterfuge;
    public int academics;
    public int awareness;
    public int finance;
    public int investigation;
    public int medicine;
    public int occult;
    public int politics;
    public int science;
    public int technology;
}

[System.Serializable]
public class EquippedItems
{
    // Define equipped items if any
}

[System.Serializable]
public class HeldItems
{
    public Item leftHand;
    public Item rightHand;
}

[System.Serializable]
public class MapPosition
{
    public int x;
    public int y;
    public int remainingMoves;
}
