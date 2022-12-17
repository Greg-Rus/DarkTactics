namespace _Scripts
{
    public enum InputEvents
    {
        MoveActionSelected,
        AttackActionSelected,
        UnitClicked, //Todo it will be better to send those just within the game context, and let it translate it to a unit event.
        GroundClicked,
        EnemyClicked,
    }
}