using System.Collections;

public class WaitBehavior : IUnitBehavior
{
    public int Priority => 1;

    public bool IsApplicable(Unit unit)
    {
        return true;
    }

    public IEnumerator Execute(Unit unit)
    {
        yield return unit.ExecuteAction(Actions.Wait);
    }
}
