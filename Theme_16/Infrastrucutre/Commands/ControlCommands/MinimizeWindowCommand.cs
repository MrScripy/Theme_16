using Theme_16.Infrastrucutre.Commands.Base;

namespace Theme_16.Infrastrucutre.Commands.ControlCommands
{
    internal class MinimizeWindowCommand : Command
    {
        public override bool CanExecute(object parameter) => true;
        public override void Execute(object parameter) => App.CurrentWindow.WindowState = System.Windows.WindowState.Minimized;
    }
}
