using Unity.Entities;
using Unity.Burst;
using Unity.Transforms;
using Unity.Mathematics;

[BurstCompile]
public partial struct MoveSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        foreach (var (unitData, mapPosition, timeUnits) in SystemAPI.Query<UnitDataComponent, MapPositionComponent, TimeUnitsComponent>())
        {
            if (timeUnits.remaining > 0)
            {
                var targetPosition = new MapPositionComponent { x = mapPosition.x + 1, y = mapPosition.y };
                SystemAPI.SetComponent(unitData.entity, targetPosition);
                timeUnits.remaining -= 1;
                SystemAPI.SetComponent(unitData.entity, timeUnits);
            }
        }
    }
}