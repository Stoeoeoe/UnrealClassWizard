using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            this.ClassModel = App.CurrentClass;

            this.AddConstructor = ClassModel.AddConstructor;
            this.AddDestructor = ClassModel.AddDestructor;

            this.ConstructorArguments = ClassModel.ConstructorArguments;


            this.ClassModel.GeneratePreviews(this.GetType());
           

        }


        private List<string> constructorArguments;

        public List<string> ConstructorArguments
        {
            get { return constructorArguments; }
            set
            {
                constructorArguments = value;

                NotifyPropertyChanged("ConstructorSignature", true);


            }
        }

        

        /// <summary>
        /// If true, a destructor is added to the class/header
        /// </summary>
        private bool addDestructor;

        public bool AddDestructor
        {
            get { return addDestructor; }
            set
            {
                addDestructor = value;
                ClassModel.AddDestructor = addDestructor;
                NotifyPropertyChanged("AddDestructor", true);
            }
        }


        /// <summary>
        /// If true, a constructor is added to the class/header
        /// </summary>
        private bool addConstructor;

        public bool AddConstructor
        {
            get { return addConstructor; }
            set
            {
                addConstructor = value;
                ClassModel.AddConstructor = addConstructor;
                NotifyPropertyChanged("AddConstructor", true);            
            }
        }





    }
}
