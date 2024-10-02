using Unity.Entities;
using UnityEngine;

public class UnitConversion : MonoBehaviour, IConvertGameObjectToEntity
{
    public UnitData unitData;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new UnitDataComponent
        {
            isPlayerControlled = unitData.isPlayerControlled,
            initiative = unitData.initiative,
            entity = entity
        });

        dstManager.AddComponentData(entity, new TimeUnitsComponent
        {
            remaining = unitData.timeUnits.remaining,
            maximum = unitData.timeUnits.maximum
        });

        dstManager.AddComponentData(entity, new MapPositionComponent
        {
            x = unitData.map.x,
            y = unitData.map.y
        });

        dstManager.AddComponentData(entity, new AttributesComponent
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
