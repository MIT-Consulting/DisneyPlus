using DisneyPlus.Uwp.Controls;
using System.Collections.Generic;

namespace DisneyPlus.Uwp.Utils
{
    public interface IAppState
    {
        List<HeaderControl> HeaderControls { get; set; }
    }
}