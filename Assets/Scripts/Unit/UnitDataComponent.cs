using Unity.Entities;

public struct UnitDataComponent : IComponentData
{
    public bool isPlayerControlled;
    public int initiative;
    public Entity entity;
}