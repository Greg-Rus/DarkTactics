using strange.extensions.command.impl;

namespace _Scripts.Commands.UnitCommands
{
    public abstract class ReturnCommand<T> : Command
    {
        public new abstract T Execute();
        
    }
}