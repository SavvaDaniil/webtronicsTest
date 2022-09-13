using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using WebtronicsTestWPF.ViewModel.FileData;
using WebtronicsTestWPF.Model;
using WebtronicsTestWPF.Facade;
using System.Diagnostics;
using WebtronicsTestWPF.Component;
using WebtronicsTestWPF.Data;
using WebtronicsTestWPF.Observer.FileData;

namespace WebtronicsTestWPF.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ApplicationDbContext _dbc;
        private string _currentPath;
        public List<FileDataLiteViewModel> fileDataLiteViewModels;
        private FileDataComponent _fileDataComponent;
        private FileDataFacade _fileDataFacade;
        private FileDataObserver _fileDataObserver;

        public MainWindow()
        {
            _dbc = new ApplicationDbContext();
            _dbc.Database.EnsureCreated();

            _currentPath = Directory.GetCurrentDirectory();
            fileDataLiteViewModels = new List<FileDataLiteViewModel>();
            _fileDataComponent = new FileDataComponent();
            _fileDataFacade = new FileDataFacade();
            _fileDataObserver = new FileDataObserver(_dbc);

            InitializeComponent();

            this.DataContext = fileDataLiteViewModels;
            inputFolderPath.Text = _currentPath;
            fileInfoPanel.Visibility = Visibility.Hidden;
            getFolder(_currentPath);
        }


        private void getFolder(string folderPath)
        {
            //проверка, есть ли такая директория
            if (!Directory.Exists(folderPath))
            {
                MessageBox.Show("Директория не найдена. Проверьте пожалуйста путь", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.Yes);
                return;
            }

            folderPath = _fileDataComponent.SecurityCheckFolderPath(folderPath);
            _currentPath = folderPath;
            inputFolderPath.Text = _currentPath;
            fileDataLiteViewModels.Clear();

            //проверяем, не корневая ли директория
            if (folderPath.Split(":").Last() != "\\")fileDataLiteViewModels.Add(new FileDataLiteViewModel(new FileData("...", "", null)));

            fileDataLiteViewModels.AddRange(_fileDataFacade.ListFileDataLiteViewModels(folderPath));

            //System.Diagnostics.Debug.WriteLine("fileDataLiteViewModels.size: " + fileDataLiteViewModels.Count());
            this.listViewFolders.ItemsSource = null;
            this.listViewFolders.ItemsSource = fileDataLiteViewModels;
        }

        private void InputFolderPath_ChangedHandler(object sender, KeyEventArgs keyEventArgs)
        {
            if(keyEventArgs.Key == Key.Return && sender != null)
            {
                TextBox? textBox = sender as TextBox;
                if (textBox == null) return;
                //System.Diagnostics.Debug.WriteLine("InputFolderPath_ChangedHandler Value: " + textBox.Text);

                this.getFolder(textBox.Text);
            }
        }

        private void ListView_DoubleClick(object sender, RoutedEventArgs e)
        {
            FileDataLiteViewModel item = (sender as ListViewItem).DataContext as FileDataLiteViewModel;
            if (item != null)
            {
                //System.Diagnostics.Debug.WriteLine("ListView_DoubleClick value: " + item.Name);
                if (item.Name == "...")
                {
                    string newPath = _fileDataComponent.RemoveLastFolder(_currentPath) ?? "C:\\";
                    //System.Diagnostics.Debug.WriteLine("На папку выше: " + newPath);
                    inputFolderPath.Text = newPath;
                    this.getFolder(newPath);
                }
                else
                {
                    if (Directory.Exists(_currentPath + "\\" + item.Name))
                    {
                        this.getFolder(_currentPath + "\\" + item.Name);
                    } else if (File.Exists(_currentPath + "\\" + item.Name))
                    {
                        _fileDataFacade.openFile(_currentPath + "\\" + item.Name);
                        _fileDataObserver.FileDataVisited(_currentPath + "\\" + item.Name);
                    }
                }
            }
        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            FileDataLiteViewModel item = (sender as ListViewItem).DataContext as FileDataLiteViewModel;
            if (item == null) return;
            if (item.Name == "...")return;

            fileInfoPanel.Visibility = Visibility.Visible;
            fileInfoWarning.Text = "";
            fileInfoName.Text = "";
            fileInfoSize.Text = "";
            fileInfoDateCreation.Text = "";
            fileInfoDateWrite.Text = "";
            fileInfoDateAccess.Text = "";
            fileInfoAmountOfFiles.Text = "";


            FileDataInfoViewModel? fileDataInfoViewModel = _fileDataFacade.getMetaData(_currentPath + "\\" + item.Name);
            if(fileDataInfoViewModel == null)
            {
                fileInfoWarning.Text = "- ошибка при чтении метаданных -";
                return;
            }

            fileInfoName.Text = fileDataInfoViewModel.Name;
            fileInfoSize.Text = (!fileDataInfoViewModel.IsCountSizeError ? _fileDataComponent.BytesToString(fileDataInfoViewModel.Size) : "-");
            fileInfoDateCreation.Text = fileDataInfoViewModel.CreationTime.ToString("HH:mm:ss dd.MM.yyyy");
            fileInfoDateWrite.Text = fileDataInfoViewModel.LastWriteTime.ToString("HH:mm:ss dd.MM.yyyy");
            fileInfoDateAccess.Text = fileDataInfoViewModel.LastAccessTime.ToString("HH:mm:ss dd.MM.yyyy");

            if (fileDataInfoViewModel.IsFile)
            {
                fileInfoTitle.Text = "Аттрибуты файла";
                rowAmountOfFiles.Visibility = Visibility.Hidden;
            } else
            {
                fileInfoTitle.Text = "Аттрибуты папки";
                fileInfoAmountOfFiles.Text = fileDataInfoViewModel.AmountOfFiles + " файлов";
                rowAmountOfFiles.Visibility = Visibility.Visible;
            }
        }

    }
}
