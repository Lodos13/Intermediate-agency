using System;
using System.Windows.Input;

namespace intermediate_agency
{
    /// <summary>
    /// A basic comand that run an Action
    /// </summary>
    class RelayCommand : ICommand
    {
        #region Private members
        /// <summary>
        /// The Action to run
        /// </summary>
        //private Action mAction;
        //private ICommand addPerson;

        private Action<object> execute;
        private Func<object, bool> canExecute;

        #endregion

        #region Constructor

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        #endregion

        #region ICommand methods and events

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }



        /// <summary>
        /// Executes the command's Action
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            this.execute(parameter);
        }

        #endregion
    }
}
