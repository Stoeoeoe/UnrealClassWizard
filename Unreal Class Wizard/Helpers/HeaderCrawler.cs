using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Unreal_Class_Wizard.Model.Tree;

namespace Unreal_Class_Wizard.Helpers
{
    public class HeaderCrawler
    {
		public static void CrawlHeaders(string enginePath = @"D:\Unreal Engine\Unreal Engine\4.6")
		{
            var fileList = new DirectoryInfo(enginePath).GetFiles("*.h", SearchOption.AllDirectories);
            List<string> AllClasses = new List<string>();
            foreach (var file in fileList)
            {
                string text = File.ReadAllText(file.FullName);
                MatchCollection matches = Regex.Matches(text, @"struct|class\s(\w*_API)?\s(\w*)");
                foreach (Match match in matches)
                {
                    string value = match.Groups[2].Value;
					if(value != "")
                    {
                        AllClasses.Add(value);
                    }
                }
            }
		}

        public static List<Item> GetSourceFileList(string enginePath = @"D:\Unreal Engine\Unreal Engine\4.6\Engine")
        {
            var items = new List<Item>();

            var dirInfo = new DirectoryInfo(enginePath);

            foreach (var directory in dirInfo.GetDirectories())
            {
                var item = new DirectoryItem
                {
                    Name = directory.Name,
                    Path = directory.FullName,
                    Items = GetSourceFileList(directory.FullName)
                };

                items.Add(item);
            }

            foreach (var file in dirInfo.GetFiles())
            {
                var item = new FileItem
                {
                    Name = file.Name,
                    Path = file.FullName
                };

                items.Add(item);
            }

            return items;
        }
    }
}
