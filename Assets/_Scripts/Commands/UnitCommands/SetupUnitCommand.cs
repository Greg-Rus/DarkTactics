using _Scripts.EventPayloads;
using _Scripts.EventPayloads.UnitEventPayloads;
using strange.extensions.command.impl;

namespace _Scripts.Commands
{
    public class SetupUnitCommand : EventCommand
    {
        public override void Execute()
        {
            dispatcher.Dispatch(GameEvents.SelectUnit, new SelectUnitPayload(){SelectedUnitId = -1});
        }
    }
}