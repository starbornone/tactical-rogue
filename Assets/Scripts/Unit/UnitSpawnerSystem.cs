using Unity.Entities;
using UnityEngine;

public class UnitSpawnerSystem : MonoBehaviour
{
    public GameObject unitPrefab;

    private EntityManager entityManager;

    void Start()
    {
        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        foreach (var unitData in UnitsData.allUnits)
        {
            SpawnUnit(unitData);
        }
    }

    private void SpawnUnit(UnitData unitData)
    {
        Entity unitEntity = entityManager.Instantiate(unitPrefab);

        entityManager.AddComponentData(unitEntity, new UnitDataComponent
        {
            isPlayerControlled = unitData.isPlayerControlled,
            initiative = unitData.initiative,
            entity = unitEntity
        });

        entityManager.AddComponentData(unitEntity, new TimeUnitsComponent
        {
            remaining = unitData.timeUnits.remaining,
            maximum = unitData.timeUnits.maximum
        });

        entityManager.AddComponentData(unitEntity, new MapPositionComponent
        {
            x = unitData.map.x,
            y = unitData.map.y
        });

        entityManager.AddComponentData(unitEntity, new AttributesComponent
        {
            strength = unitData.attributes.strength,
            dexterity = unitData.attributes.dexterity,
            stamina = unitData.attributes.stamina,
            charisma = unitData.attributes.charisma,
            manipulation = unitData.attributes.manipulation,
            composure = unitData.attributes.composure,
            intelligence = unitData.attributes.intelligence,
            resolve = unitData.attributes.resolve
        });
    }
}
