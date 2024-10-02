using System.Collections;

public interface IUnitBehavior
{
    int Priority { get; }
    bool IsApplicable(Unit unit);
    IEnumerator Execute(Unit unit);
}
