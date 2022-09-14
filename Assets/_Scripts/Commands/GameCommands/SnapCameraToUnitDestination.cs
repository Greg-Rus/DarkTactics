using _Scripts.EventPayloads;
using strange.extensions.command.impl;

namespace _Scripts.Commands
{
    public class SnapCameraToUnitDestination : EventCommand
    {
        [Inject] public GameContextRoot GameContextRoot { get; set; }
        public override void Execute()
        {
            var data = (MouseClickGroundPayload)evt.data;
            GameContextRoot.CameraTarget.position = data.ClickPosition;
        }
    }
}