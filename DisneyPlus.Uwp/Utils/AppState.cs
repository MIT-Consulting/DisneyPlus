using DisneyPlus.Common;
using DisneyPlus.Uwp.Controls;
using System.Collections.Generic;

namespace DisneyPlus.Uwp.Utils
{
    public class AppState : ObservableObject, IAppState
    {
        public AppState() { }

        public List<HeaderControl> HeaderControls
        {
            get => _headerControls ?? (_headerControls = new List<HeaderControl>());
            set => SetProperty(ref _headerControls, value);
        }
        private List<HeaderControl> _headerControls;
    }
}
