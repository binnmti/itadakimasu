namespace ItadakimasuWeb.Models.Select;

public class SelectViewerCount : ISelectItem
{
    public string Id => nameof(SelectViewerCount);
    public string Title => "表示件数";
    public string DefaultKey => "50";
    public string CurrentKey { get; set; } = "";
    public Dictionary<string, string> Items => new()
    {
        { "25", "25件" },
        { "50", "50件" },
        { "100", "100件" },
    };
}
