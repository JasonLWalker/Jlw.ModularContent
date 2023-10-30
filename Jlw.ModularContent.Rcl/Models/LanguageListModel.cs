using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Jlw.ModularContent
{
    public interface ILanguageListModel
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
        IEnumerable<SelectListItem> Items { get; }

        void Refresh();
    }

    public class LanguageListModel : ILanguageListModel
    {
        private readonly List<SelectListItem> _items = new List<SelectListItem>();
        private readonly ILocalizedGroupDataItemRepository _repo;

        public IEnumerable<SelectListItem> Items => _items;//.Select(o=>new SelectListItem(o.Name, o.Id.ToString()));

        public LanguageListModel(ILocalizedGroupDataItemRepository repo)
        {
            _repo = repo;
            Initialize();
        }

        protected void Initialize()
        {
            _items.Clear();
            var aList = _repo.GetItems("LocalizedContentLanguages", "EN");
            foreach (var item in aList)
            {
                _items.Add(new SelectListItem(item.Value, item.Key));
            }
        }
        public void Refresh()
        {
            Initialize();
        }


    }
}