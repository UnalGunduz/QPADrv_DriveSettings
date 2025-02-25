using System;
using System.IO;
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
using AddIn.Contracts;
using System.Xml.Linq;

namespace AddIn.UI.pages
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        MainWindow _window;
        string _text;
        private readonly IAddInController _controller;

        public Page1(MainWindow window, IAddInController controller)
        {
            InitializeComponent();
            _controller = controller;
            LoadDrives();
            button_createLog.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            _window = window;
            _window.setTitle("MessageBox");

        }

        private void LoadDrives()
        {
            DriveTreeView.Items.Clear();

            List<IControlUnitItem> controlUnits = _controller.GetControlUnits();

            if (controlUnits != null)
            {
                foreach (var controlUnit in controlUnits)
                {
                    TreeViewItem controlUnitNode = new TreeViewItem
                    {
                        Header = controlUnit.Name
                    };

                    foreach (var drive in controlUnit.Drives)
                    {

                        TreeViewItem driveNode = new TreeViewItem
                        {
                            Header = drive.Name
                        };
                        controlUnitNode.Items.Add(driveNode);


                    }
                    DriveTreeView.Items.Add(controlUnitNode);
                }
            }
        }
        private void DriveTreeView_SelectionChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (DriveTreeView.SelectedItem is TreeViewItem selectedItem)
            {
                if (selectedItem.Tag is IDriveItem selectedDrive)
                {
                    _controller.HandleSelectedDrive(selectedDrive);
                }
            }
            //string logText = _controller.GetLogText();
            //textBlock.Text = logText;
        }

        /// <summary>
        /// Close the Window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ok_Click(object sender, RoutedEventArgs e)
        {
            _window.closeWindow();
        }
        private void Button_Test1(object sender, RoutedEventArgs e)
        {
            _controller.WriteLogText();
        }

        /// <summary>
        /// Create a new log file on the Desktop named LogAddIn.txt 
        /// </summary>
        private void Button_createLog_Click(object sender, RoutedEventArgs e)
        {
            // Set a variable to the Documents path.
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            // Write the log text to a new file named "LogAddIn.txt".
            File.WriteAllText(System.IO.Path.Combine(docPath, "LogAddIn.txt"), _text);

            //Change the buttons background color to green
            button_createLog.Background = new SolidColorBrush(Color.FromRgb(0, 255, 0));
        }
    }
}
