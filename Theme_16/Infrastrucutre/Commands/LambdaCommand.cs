using System;
using Theme_16.Infrastrucutre.Commands.Base;

namespace Theme_16.Infrastrucutre.Commands
{
    internal class LambdaCommand : Command
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public LambdaCommand(Action<object> execute, Func<object, bool> canExchange)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExchange;
        }
        public override bool CanExecute(object? parameter) => _canExecute?.Invoke(parameter) ?? true;
        public override void Execute(object? parameter) => _execute(parameter);
    }
}
