using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unreal_Class_Wizard.Model;

namespace Unreal_Class_Wizard.ViewModel
{
    public class ConstructorViewModel : BaseViewModel
    {
        public ConstructorViewModel()
        {
            this.AddConstructor = true;
            this.AddDestructor = false;
            this.ConstructorSignature = "";
        }

        private string constructorSignature;

        public string ConstructorSignature
        {
            get { return constructorSignature; }
            set
            {
                constructorSignature = value;
            }
        }

        private bool addConstructor;

        public bool AddConstructor
        {
            get { return addConstructor; }
            set { addConstructor = value; }
        }


        private bool addDestructor;

        public bool AddDestructor
        {
            get { return addDestructor; }
            set { addDestructor = value; }
        }


    }
}
