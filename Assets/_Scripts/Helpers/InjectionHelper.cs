using _Scripts.Commands.UnitCommands;
using strange.extensions.command.api;
using strange.extensions.injector.api;
using strange.extensions.injector.impl;

namespace _Scripts.Helpers
{
    public static class InjectionHelper
    {
        public static ICommand InjectWith(this ICommand command, IInjectionBinder binder)
        {
            binder.injector.Inject(command, false);
            return command;
        }
        
        public static ReturnCommand<T> InjectWith<T>(this ReturnCommand<T> command, IInjectionBinder binder)
        {
            binder.injector.Inject(command, false);
            return command;
        }
    }
}