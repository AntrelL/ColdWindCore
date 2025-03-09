using TMPro;

namespace ColdWind.Core.UniversalUI
{
    public interface ILinkedTextData
    {
        TMP_Text TMPText { get; }

        string BaseText { get; set; }
    }
}
