using System;
using System.Windows.Input;

namespace Cinema.UI.Infrastructure
{
    class DelegateCommand : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public DelegateCommand(Action<object> exec, Func<object, bool> canExec = null)
        {
            execute = exec;
            canExecute = canExec;
        }

        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute.Invoke(parameter);
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }
    }
}
