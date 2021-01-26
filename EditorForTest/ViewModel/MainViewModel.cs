using EditorForTest.Utils;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System;
using System.Windows;
using System.Windows.Input;

namespace EditorForTest.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private string inputText;
        private string outputText;
        private ScriptEngine engine;

        public MainViewModel()
        {
            engine = Python.CreateEngine();
            
            //Commands
            PasteCommand = new RelayCommand(PasteCommandExecute, null);
            CopyCommand = new RelayCommand(CopyCommandExecute, null);
            DoScriptCommand = new RelayCommand(DoScriptCommandExecute, null);
        }

        #region Properties

        public string InputText 
        { 
            get => inputText;
            set
            {
                SetPropertry(ref inputText, value);
                Editor(value);
            }
        }
        public string OutputText 
        { 
            get => outputText;
            set
            {
                SetPropertry(ref outputText, value);
            }
        }

        #endregion

        #region Methods

        private void Editor(string str)
        {
            str = str.ToLower();
            str = str.Replace("_", " ");
            str = str.Replace(", end=''", "");
            Clipboard.SetText(str);
            OutputText = str;
        }

        #endregion

        #region Commands

        public ICommand PasteCommand { get; set; }
        public void PasteCommandExecute(object obj) => InputText = Clipboard.GetText();
        
        public ICommand CopyCommand { get; set; }
        public void CopyCommandExecute(object obj) => Clipboard.SetText(OutputText);

        public ICommand DoScriptCommand { get; set; }
        public void DoScriptCommandExecute(object obj)
        {
            try
            {
                engine.Execute(OutputText);
            }
            catch (System.Exception ex)
            {
                //Console.WriteLine(ex);
                Console.WriteLine("Invalid input.");
            }

        }

        #endregion
    }
}
