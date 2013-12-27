using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc4KnockoutCRUD.Models
{
    public class SimpleListModel
    {
        public string ItemToAdd { get; set; }
        public List<string> Items { get; set; }

        public void AddItem()
        {
            Items.Add(ItemToAdd);
            ItemToAdd = "";
        }
    }
}