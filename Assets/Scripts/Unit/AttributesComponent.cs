using Unity.Entities;

public struct AttributesComponent : IComponentData
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