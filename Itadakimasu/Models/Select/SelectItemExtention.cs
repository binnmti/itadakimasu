namespace ItadakimasuWeb.Models.Select;
public static class SelectItemExtention
{
    public static string SaveCookie(this ISelectItem selectItem, string key, IRequestCookieCollection getCookies, IResponseCookies setCookies)
    {
        if (selectItem.Items.ContainsKey(key))
        {
            setCookies.Append(selectItem.Id, key);
            selectItem.CurrentKey = key;
        }
        else
        {
            var cookie = getCookies[selectItem.Id] ?? "";
            selectItem.CurrentKey = cookie == "" ? selectItem.DefaultKey : cookie;
        }
        return selectItem.CurrentKey;
    }
}
