using Unity.Entities;
using Unity.Burst;
using Unity.Transforms;

[BurstCompile]
public partial struct HealWeakestAllySystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        foreach (var (unitData, mapPosition) in SystemAPI.Query<UnitDataComponent, MapPositionComponent>())
        {
            if (unitData.isPlayerControlled && HasHealingItem(unitData))
            {
                var weakestAlly = FindWeakestAlly(ref state, mapPosition, unitData);
                if (weakestAlly != Entity.Null)
                {
                    HealAlly(ref state, unitData, weakestAlly);
                }
            }
        }
    }

    private Entity FindWeakestAlly(ref SystemState state, MapPositionComponent unitPosition, UnitDataComponent unitData)
    {
        Entity weakestAlly = Entity.Null;
        int lowestHealth = int.MaxValue;

        foreach (var (allyData, allyPosition) in SystemAPI.Query<UnitDataComponent, MapPositionComponent>())
        {
            if (allyData.isPlayerControlled == unitData.isPlayerControlled)
            {
                int health = SystemAPI.GetComponent<AttributesComponent>(allyData.entity).stamina;
                if (health < lowestHealth)
                {
                    lowestHealth = health;
                    weakestAlly = allyData.entity;
                }
            }
        }

        return weakestAlly;
    }

    private void HealAlly(ref SystemState state, UnitDataComponent unitData, Entity target)
    {
        var attributes = SystemAPI.GetComponent<AttributesComponent>(target);
        attributes.stamina += 10;
        SystemAPI.SetComponent(target, attributes);
    }

    private bool HasHealingItem(UnitDataComponent unitData)
    {
        // Logic to check if the unit has a healing item
        return true;
    }
}