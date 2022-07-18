namespace ItadakimasuWeb.Models.Select;

public class SelectViewerSize : ISelectItem
{
    public string Id => nameof(SelectViewerSize);
    public string Title => "表示サイズ";
    public string DefaultKey => "ImageS";
    public string CurrentKey { get; set; } = "";
    public Dictionary<string, string> Items => new()
    {
        { "ImageS", "S" },
        { "ImageM", "M" },
        { "ImageL", "L" },
    };
}
