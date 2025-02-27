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

            List<IControlUnitItemS120> controlUnitsS120 = _controller.GetControlUnitsS120();
            List<IDriveItemG120> drivesG120 = _controller.GetDriveItemG120();

            if (drivesG120 != null)
            {
                foreach (var driveG120 in drivesG120)
                {
                    TreeViewItem driveNodeG120 = new TreeViewItem
                    {
                        Header = driveG120.Name
                    };
                    DriveTreeView.Items.Add(driveNodeG120);
                }
            }
            if (controlUnitsS120 != null)
            {
                foreach (var controlUnitS120 in controlUnitsS120)
                {
                    TreeViewItem controlUnitNodeS120 = new TreeViewItem
                    {
                        Header = controlUnitS120.Name
                    };

                    foreach (var driveS120 in controlUnitS120.Drives)
                    {

                        TreeViewItem driveNodeS120 = new TreeViewItem
                        {
                            Header = driveS120.Name
                        };
                        controlUnitNodeS120.Items.Add(driveNodeS120);


                    }
                    DriveTreeView.Items.Add(controlUnitNodeS120);
                }
            }
        }
        private void DriveTreeView_SelectionChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (DriveTreeView.SelectedItem is TreeViewItem selectedItem)
            {
                if (selectedItem.Tag is IDriveItemS120 selectedDrive)
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
            if (DriveTreeView.SelectedItem is TreeViewItem selectedItem)
            {
                if (selectedItem.Tag is IDriveItemG120 selectedDrive)
                {
                    textBlock.Text = _controller.ReadParameter(selectedDrive.DriveObject);
                }
            }
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
