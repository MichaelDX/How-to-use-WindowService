using System.Windows.Input;
using DevExpress.Mvvm;

namespace DXSample.ViewModels {
    public class MainViewModel : ViewModelBase {
        protected IWindowService WindowService { get { return this.GetService<IWindowService>(); } }
        public ICommand ShowChildWindowCommand { get; private set; }
        public ICommand CloseChildWindowCommand { get; private set; }
        public ICommand RestoreChildWindowCommand { get; private set; }
        public ChildViewModel ChildWindowViewModel {
            get { return GetValue<ChildViewModel>(); }
            set { SetValue(value); }
        }
        public MainViewModel() {
            ShowChildWindowCommand = new DelegateCommand(ShowChildWindow, CanShowChildWindow);
            CloseChildWindowCommand = new DelegateCommand(CloseChildWindow, CanCloseChildWindow);            
        }

        public void ShowChildWindow() {
            if(ChildWindowViewModel == null) 
                ChildWindowViewModel = new ChildViewModel() { Caption = "Hello, World!" };                                                 
            WindowService.Show(ChildWindowViewModel);            
        }
        private bool CanShowChildWindow() {
            return !WindowService.IsWindowAlive;
        }
        private void CloseChildWindow() {
            ChildWindowViewModel = null;
            WindowService.Close();            
        }
        private bool CanCloseChildWindow() {
            return WindowService.IsWindowAlive;
        }
    }
}