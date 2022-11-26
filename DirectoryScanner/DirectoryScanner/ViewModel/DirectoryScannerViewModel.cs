﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DirectoryScanner.Model;

namespace DirectoryScanner.ViewModel
{
    public class DirectoryScannerViewModel: INotifyPropertyChanged
    {
        private CancellationTokenSource _cancelTokenSource;

        private Scanner _scanner;

        private IDirectoryComponent? _root;

        public IDirectoryComponent Root { 
            get { return _root; }
            set
            {
                _root = value;
                OnPropertyChanged("Root");  
            }
        }

        private void StartScanner()
        {
            var fbd = new FolderBrowserForWPF.Dialog();
            string path = String.Empty;
            if(!fbd.ShowDialog().GetValueOrDefault())
                return;

            _cancelTokenSource = new CancellationTokenSource(); 
            var token = _cancelTokenSource.Token;


            Task.Run(() =>
            {
                var root = new DirectoryComponent("", "", ComponentType.Directory);
                root.ChildNodes = new ObservableCollection<IDirectoryComponent> { _scanner.StartScanner(fbd.FileName, token) };
                Root = root;
            });

        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private BaseCommand? _startScannerCommand;
        public BaseCommand StartScannerCommand
        {
            get { return _startScannerCommand ??= new BaseCommand(obj => StartScanner()); }
        }

        private BaseCommand? _cancelScannerCommand;
        public BaseCommand CancelScannerCommand
        {
            get { return _cancelScannerCommand ??= new BaseCommand(obj => CancelScanner()); }
        }

        public void CancelScanner()
        {
            if(_cancelTokenSource != null && !_cancelTokenSource.IsCancellationRequested)
            {
                _cancelTokenSource.Cancel();
                _cancelTokenSource.Dispose();
            }
        }

        public DirectoryScannerViewModel()
        {
            var threadCount = 10;
            _scanner = new Scanner(threadCount);
        }
    }
}
