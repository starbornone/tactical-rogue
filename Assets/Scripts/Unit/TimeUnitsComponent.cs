using Unity.Entities;

public struct TimeUnitsComponent : IComponentData
{
    public int remaining;
    public int maximum;
}