using _Scripts.Models;
using strange.extensions.command.impl;
using UnityEngine;

namespace _Scripts.Commands.UnitCommands
{
    public class DieCommand : Command
    {
        [Inject] public Animator Animator { private get; set; }
        [Inject] public EntityRegistryService EntityRegistryService { private get; set; }
        [Inject] public EntityFasade EntityFasade { private get; set; }
        public override void Execute()
        {
            Animator.SetTrigger(AnimationConstants.Die);
            EntityRegistryService.DeregisterEntityFasade(EntityFasade);
            EntityFasade.UnitContextRoot.BoundsCollider.enabled = false;
        }
    }
}