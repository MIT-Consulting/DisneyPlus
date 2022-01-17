using DisneyPlus.Common;
using DisneyPlus.Data.Models;
using DisneyPlus.Data.Repos;
using DisneyPlus.Uwp.Utils;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace DisneyPlus.Uwp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private IAppState _appState;
        private IContentRepo _contentRepo;
        public MainPageViewModel(
            IAppState appState, 
            IContentRepo contentRepo)
        {
            _appState = appState;
            _contentRepo = contentRepo;
            LoadDataCommand = new AsyncCommand(LoadData);
        }
        public IAsyncCommand LoadDataCommand { get; private set; }
        public async Task LoadData()
        {
            IsBusy = true;

            Headers = new ObservableCollection<ContentTileHeader>(await _contentRepo.GetHeaders());
            Categories = new ObservableCollection<ContentTileVideo>(await _contentRepo.GetCategories());                        
            Collections = new ObservableCollection<ContentCollection>(await _contentRepo.GetCollections());

            IsBusy = false;
        }        
        
        public ObservableCollection<ContentCollection> Collections
        {
            get => _collections ?? (_collections = new ObservableCollection<ContentCollection>());
            set => SetProperty(ref _collections, value);
        }
        private ObservableCollection<ContentCollection> _collections;
       
        public ObservableCollection<ContentTileHeader> Headers
        {
            get => _headers ?? (_headers = new ObservableCollection<ContentTileHeader>());
            set => SetProperty(ref _headers, value);
        }
        private ObservableCollection<ContentTileHeader> _headers;

        public ObservableCollection<ContentTileVideo> Categories
        {
            get => _categories ?? (_categories = new ObservableCollection<ContentTileVideo>());
            set => SetProperty(ref _categories, value);
        }
        private ObservableCollection<ContentTileVideo> _categories;
        
        public ContentTileHeader SelectedHeader
        {
            get => _selectedHeader;
            set
            {
                SetProperty(ref _selectedHeader, value);
                TriggerHeaderAnimation();
            }
        }
        private ContentTileHeader _selectedHeader;

        private void TriggerHeaderAnimation() => 
            _appState.HeaderControls
            .FirstOrDefault(x => (x.DataContext as ContentTileHeader)?.Id == _selectedHeader?.Id)?
            .OnSelected();
    }
}
