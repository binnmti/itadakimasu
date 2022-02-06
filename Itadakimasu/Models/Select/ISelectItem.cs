namespace Itadakimasu.Models.Select;
public interface ISelectItem
{
    public string Title { get; }
    public string DefaultKey { get; }
    public string Id { get; }
    public string CurrentKey { get; set; }
    public Dictionary<string, string> Items { get; }
}
