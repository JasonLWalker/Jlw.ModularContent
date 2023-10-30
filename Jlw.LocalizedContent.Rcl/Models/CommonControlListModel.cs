using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Jlw.ModularContent;

public interface ICommonControlListModel
{
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
    IEnumerable<SelectListItem> Items { get; }

    void Refresh();
}


public class CommonControlListModel : ICommonControlListModel
{
    private readonly List<SelectListGroup> _groups = new List<SelectListGroup>();
    private readonly List<SelectListItem> _items = new List<SelectListItem>();
    private readonly ILocalizedGroupDataItemRepository _repo;

    public IEnumerable<SelectListGroup> Groups => _groups;
    public IEnumerable<SelectListItem> Items => _items;

    public CommonControlListModel(ILocalizedGroupDataItemRepository repo)
    {
        _repo = repo;
        Initialize();
    }

    protected void Initialize()
    {
        _items.Clear();
        /*
            var aList = _repo.GetItems("LocalizedContentLanguages", "EN");
            foreach (var item in aList)
            {
                _items.Add(new SelectListItem(item.Value, item.Key));
            }
            */
        SelectListGroup group;
        
        _groups.Add(group = new SelectListGroup() { Name = "Date/Time Inputs" });
        _items.Add(new SelectListItem() { Value = "DATE", Text = "Date", Group = group});
        _items.Add(new SelectListItem() { Value = "DATETIME-LOCAL", Text = "Date/Time", Group = group });
        _items.Add(new SelectListItem() { Value = "MONTH", Text = "Month", Group = group });
        _items.Add(new SelectListItem() { Value = "TIME", Text = "Time", Group = group });
        _items.Add(new SelectListItem() { Value = "WEEK", Text = "Week", Group = group });

        _groups.Add(group = new SelectListGroup() { Name = "Special Inputs" });
        _items.Add(new SelectListItem() { Value = "COLOR", Text = "Color Picker", Group = group });
        _items.Add(new SelectListItem() { Value = "CHECKBOX", Text = "Check box", Group = group });
        _items.Add(new SelectListItem() { Value = "HIDDEN", Text = "Hidden", Group = group });
        _items.Add(new SelectListItem() { Value = "RADIO", Text = "Radio button", Group = group });
        _items.Add(new SelectListItem() { Value = "SLIDER", Text = "Slider", Group = group });

        _groups.Add(group = new SelectListGroup() { Name = "Textual Inputs" });
        _items.Add(new SelectListItem() { Value = "TEXT", Text = "Text", Group = group });
        _items.Add(new SelectListItem() { Value = "PASSWORD", Text = "Password", Group = group });
        _items.Add(new SelectListItem() { Value = "TEL", Text = "Phone Number", Group = group });
        _items.Add(new SelectListItem() { Value = "URL", Text = "Web Address", Group = group });
        _items.Add(new SelectListItem() { Value = "EMAIL", Text = "Email", Group = group });
        _items.Add(new SelectListItem() { Value = "NUMBER", Text = "Number", Group = group });
        _items.Add(new SelectListItem() { Value = "SEARCH", Text = "Search", Group = group });

    }
    public void Refresh()
    {
        Initialize();
    }

}