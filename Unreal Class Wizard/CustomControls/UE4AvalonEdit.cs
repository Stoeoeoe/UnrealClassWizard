using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Highlighting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Unreal_Class_Wizard.Model;

namespace Unreal_Class_Wizard.View
{
    class UE4AvalonEdit : TextEditor
    {

        public UE4AvalonEdit()
        {

        }

        protected override void OnInitialized(EventArgs e)
        {
            //string regexText = "";

            //foreach (ClassSpecifier classSpecifier in ClassSpecifier.LoadClassSpecifiers())
            //{
            //    regexText += classSpecifier.Name + "|";
            //}
            //regexText = regexText.Trim('|');
            //regexText += "";

            //var ruleSet = SyntaxHighlighting.MainRuleSet.Rules;
            //HighlightingRule classSpecifierRule = new HighlightingRule();
            //Color color = new Color(){A = 255, R = 10, G = 100, B = 100};
            //classSpecifierRule.Regex = new System.Text.RegularExpressions.Regex(regexText);
            //classSpecifierRule.Color = new ICSharpCode.AvalonEdit.Highlighting.HighlightingColor() { Foreground = new SimpleHighlightingBrush(color), Name = "ClassSpecifiers", FontWeight = FontWeight.FromOpenTypeWeight(600)};
            //ruleSet.Add(classSpecifierRule);
            base.OnInitialized(e);
        }
    }


}
