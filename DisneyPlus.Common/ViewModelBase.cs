namespace DisneyPlus.Common
{
    public class ViewModelBase : ObservableObject
    {
        private bool isbusy;
        public bool IsBusy
        {
            get => isbusy;
            set => SetProperty(ref isbusy, value);
        }
    }
}
