using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Unreal_Class_Wizard.Model
{
    public class HeaderParser
    {
        private static UnrealClass parsedClass;


        public static void ParseHeader(string name)
        {
            parsedClass = new UnrealClass();
            // Okay, this is gonna be kinda messy

            // Load the text
            string text = File.ReadAllText(@"D:\Unreal Projects\ShooterGame3\Source\ShooterGame\Classes\Bots\ShooterAIController.h");
            string[] lines = text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            GetCopyrightText(lines);
            GetIncludes(text);

            // TODO: Only works with UClass

            // Get all Class Specifiers
            List<string> specifiers = new List<string>();
            string specifierString = Regex.Match(text, "UCLASS\\(([\\w\\W].*)\\)").Groups[1].Value;
            specifiers.AddRange(specifierString.Split(','));

            // Check if abstract
            //parsedClass.IsAbstract = specifierString.ToLower().Contains("abstract");


        }

        private static void GetIncludes(string text)
        {
            // Get all includes
            List<string> includeClasses = new List<string>();
            MatchCollection includeMatches = Regex.Matches(text, "#include \"([\\w\\W]*?)\"");
            foreach (Match match in includeMatches)
            {
                string includeClass = match.Groups[1].Value;
                // Only include other classes
                if (includeClass.Contains(".generated.h") == false)
                {
                    // Ignore the '.h'
                    includeClasses.Add(includeClass.Substring(0, includeClass.Length - 2));
                }
            }
            parsedClass.IncludedClasses.AddRange(includeClasses);
        }

        private static void GetCopyrightText(string[] lines)
        {
            // Check if the first line is a comment, then it is most probably the copyright info
            if (lines[0].StartsWith("//"))
            {
                parsedClass.CopyrightText = lines[0];
            }
        }
    }
}
