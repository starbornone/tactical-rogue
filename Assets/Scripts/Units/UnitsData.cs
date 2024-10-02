using System.Collections.Generic;
using Unity.VisualScripting;

public static class UnitsData
{
    public static UnitData ogi = new UnitData
    {
        id = "ogi",
        name = "Ogi",
        body = new BodyData
        {
            age = 30,
            gender = "male",
            height = 188f,
            species = "human",
            weight = 85f
        },
        attributes = new Attributes
        {
            strength = 4,
            dexterity = 3,
            stamina = 4,
            charisma = 2,
            manipulation = 2,
            composure = 3,
            intelligence = 3,
            wits = 3,
            resolve = 4
        },
        equippedItems = new EquippedItems(),
        heldItems = new HeldItems
        {
            leftHand = ItemsData.sword,
            rightHand = ItemsData.shield
        },
        carriedItems = new List<Item> { },
        map = new MapPosition
        {
            x = 5,
            y = 5,
        },
        timeUnits = new TimeUnits
        {
            remaining = 50,
            maximum = 50
        },
        isPlayerControlled = false
    };

    public static UnitData sha = new UnitData
    {
        id = "sha",
        name = "Sha",
        body = new BodyData
        {
            age = 25,
            gender = "female",
            height = 180f,
            species = "human",
            weight = 60f
        },
        attributes = new Attributes
        {
            strength = 3,
            dexterity = 4,
            stamina = 3,
            charisma = 3,
            manipulation = 2,
            composure = 4,
            intelligence = 4,
            wits = 4,
            resolve = 3
        },
        equippedItems = new EquippedItems(),
        heldItems = new HeldItems
        {
            leftHand = ItemsData.sword,
            rightHand = ItemsData.shield
        },
        carriedItems = new List<Item> { },
        map = new MapPosition
        {
            x = 10,
            y = 7,
        },
        timeUnits = new TimeUnits
        {
            remaining = 50,
            maximum = 50
        },
        isPlayerControlled = false
    };

    public static UnitData rainie = new UnitData
    {
        id = "rainie",
        name = "Rainie",
        body = new BodyData
        {
            age = 25,
            gender = "female",
            height = 180f,
            species = "human",
            weight = 60f
        },
        attributes = new Attributes
        {
            strength = 3,
            dexterity = 4,
            stamina = 3,
            charisma = 3,
            manipulation = 2,
            composure = 4,
            intelligence = 4,
            wits = 4,
            resolve = 3
        },
        equippedItems = new EquippedItems(),
        heldItems = new HeldItems
        {
            leftHand = ItemsData.sword,
            rightHand = ItemsData.shield
        },
        carriedItems = new List<Item> { },
        map = new MapPosition
        {
            x = 13,
            y = 10
        },
        timeUnits = new TimeUnits
        {
            remaining = 50,
            maximum = 50
        },
        isPlayerControlled = false
    };

    public static UnitData raven = new UnitData
    {
        id = "raven",
        name = "Raven",
        body = new BodyData
        {
            age = 30,
            gender = "male",
            height = 188f,
            species = "human",
            weight = 85f
        },
        attributes = new Attributes
        {
            strength = 4,
            dexterity = 3,
            stamina = 4,
            charisma = 2,
            manipulation = 2,
            composure = 3,
            intelligence = 3,
            wits = 3,
            resolve = 4
        },
        equippedItems = new EquippedItems(),
        heldItems = new HeldItems
        {
            leftHand = ItemsData.sword,
            rightHand = ItemsData.shield
        },
        carriedItems = new List<Item> { },
        map = new MapPosition
        {
            x = 16,
            y = 13
        },
        timeUnits = new TimeUnits
        {
            remaining = 50,
            maximum = 50
        },
        isPlayerControlled = false
    };

    public static List<UnitData> allUnits = new List<UnitData>
    {
        ogi,
        sha,
        rainie,
        raven
    };
}
