using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Unreal_Class_Wizard.Model.Tree
{
    public class DirectoryItem : Item
    {
        public List<Item> Items { get; set; }

        public DirectoryItem()
        {
            Items = new List<Item>();
        }
    }
}
