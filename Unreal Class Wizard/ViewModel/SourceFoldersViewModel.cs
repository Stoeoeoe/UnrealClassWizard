using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unreal_Class_Wizard.Helpers;
using Unreal_Class_Wizard.Model.Tree;

namespace Unreal_Class_Wizard.ViewModel
{
    public class SourceFoldersViewModel : BaseViewModel
    {

        private List<Item> treeItems;

        public SourceFoldersViewModel(string enginePath)
        {
            this.TreeItems = HeaderCrawler.GetSourceFileList(enginePath);
        }

        public List<Item> TreeItems
        {
            get { return treeItems; }
            set { treeItems = value; NotifyPropertyChanged("TreeItems"); }
        }

    }
}
