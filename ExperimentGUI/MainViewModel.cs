using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace ExperimentGUI
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public string Pattern { get; set; }

        private string folder;
        public string Folder
        {
            get { return folder; }
            set
            {
                folder = value;

                Console.WriteLine(value);

                NotifyPropertyChanged(nameof(Folder));
            }
        }

        private ICommand chooseFolderCommand;
        public ICommand ChooseFolderCommand
        {
            get
            {
                if (chooseFolderCommand == null)
                    chooseFolderCommand = new DelegateCommand
                    {
                        ExecuteHandler = ChooseFolderCommandExecute,
                        CanExecuteHandler = ChooseFolderCommandCanExecute,
                    };
                return chooseFolderCommand;
            }
        }

        private void ChooseFolderCommandExecute(object parameter)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    Folder = dialog.SelectedPath;
                }
            }
        }

        private bool ChooseFolderCommandCanExecute(object parameter)
        {
            return true;
        }

        private ICommand executeSearchCommand;
        public ICommand ExecuteSearchCommand
        {
            get
            {
                if (executeSearchCommand == null)
                    executeSearchCommand = new DelegateCommand
                    {
                        ExecuteHandler = ExecuteSearchCommandExecute,
                        CanExecuteHandler = ExecuteSearchCommandCanExecute,
                    };
                return executeSearchCommand;
            }
        }

        private async void ExecuteSearchCommandExecute(object parameter)
        {
            Console.WriteLine("Start!");

            await Task.Run(() =>
            {
                WriteConsole("Start");

                foreach (string file in Directory.EnumerateFiles(folder, "*.txt", SearchOption.AllDirectories))
                {
                    WriteConsole(file);

                    foreach (string line in File.ReadLines(file))
                    {
                        if (Regex.IsMatch("line", Pattern))
                        {
                            WriteOutputTextBox(line);
                        }
                    }
                }

                WriteOutputTextBox("End");
            });
        }

        private bool ExecuteSearchCommandCanExecute(object parameter)
        {
            return true;
        }

        private void WriteConsole(string s)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                Console.WriteLine(s);
            }));
        }

        private void WriteOutputTextBox(string s)
        {
            /*System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                OutputTextBox.AppendText(s + Environment.NewLine);
            }));*/
        }
    }
}
