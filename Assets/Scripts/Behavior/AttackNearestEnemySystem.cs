using Unity.Entities;
using Unity.Burst;
using Unity.Mathematics;
using Unity.Transforms;

[BurstCompile]
public partial struct AttackNearestEnemySystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        foreach (var (unitData, mapPosition) in SystemAPI.Query<UnitDataComponent, MapPositionComponent>())
        {
            if (!unitData.isPlayerControlled)
            {
                var nearestEnemy = FindNearestEnemy(ref state, mapPosition, unitData);
                if (nearestEnemy != Entity.Null)
                {
                    PerformAttack(ref state, unitData, nearestEnemy);
                }
            }
        }
    }

    private Entity FindNearestEnemy(ref SystemState state, MapPositionComponent unitPosition, UnitDataComponent unitData)
    {
        Entity nearestEnemy = Entity.Null;
        float minDistance = float.MaxValue;

        foreach (var (enemyData, enemyPosition) in SystemAPI.Query<UnitDataComponent, MapPositionComponent>())
        {
            if (enemyData.isPlayerControlled != unitData.isPlayerControlled)
            {
                float distance = math.distance(new float2(unitPosition.x, unitPosition.y), new float2(enemyPosition.x, enemyPosition.y));
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestEnemy = enemyData.entity;
                }
            }
        }

        return nearestEnemy;
    }

    private void PerformAttack(ref SystemState state, UnitDataComponent unitData, Entity target)
    {
        // Attack logic goes here
    }
}
