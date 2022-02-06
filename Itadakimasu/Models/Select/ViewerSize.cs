namespace Itadakimasu.Models.Select;

public class ViewerSize : ISelectItem
{
    public string Id => "ViewerSize";
    public string Title => "表示サイズ";
    public string DefaultKey => "S";
    public string CurrentKey { get; set; } = "";
    public Dictionary<string, string> Items => new()
    {
        {"S","S"},
        {"M","M"},
        {"L","L"},
    };
}
