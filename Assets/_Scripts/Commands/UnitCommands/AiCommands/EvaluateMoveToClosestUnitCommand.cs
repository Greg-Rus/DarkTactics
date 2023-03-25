using strange.extensions.command.impl;

namespace _Scripts.Commands.UnitCommands.AiCommands
{
    public class EvaluateMoveToClosestUnitCommand : Command
    {
        [Inject] public EntityRegistryService EntityRegistryService { private get; set; }
        public override void Execute()
        {
            var units = EntityRegistryService.GetAllPlayerUnitId();
            
        }
    }
}