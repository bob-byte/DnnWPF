using System;
using System.Windows.Input;

namespace DnnWPF.ViewModels
{
    public class RelayCommand : ICommand
    {
        private Action<Object> execute;
        private Func<Object, Boolean> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public RelayCommand(Action<Object> execute, Func<Object, Boolean> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public Boolean CanExecute(Object parameter) =>
            canExecute == null || canExecute(parameter);

        public void Execute(Object parameter)
        {
            execute(parameter);
        }
    }
}
