using AddIn.Contracts;
using AddIn.UI.pages;
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

namespace AddIn.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(IAddInController controller)
        {
            InitializeComponent();


            //load a new page to the Mainwindow
            frame.Content = new Page1(this, controller);
        }

        /// <summary>
        /// Minimize the Window
        /// </summary>
        private void btnMinimizeScreen_Click(object sender, RoutedEventArgs e)
        {
            this.frame.Focus();
            this.WindowState = WindowState.Minimized;

        }

        /// <summary>
        /// Maximize the Window
        /// </summary>
        private void btnMaximizeScreen_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
                this.MinimizeRectangle1.Visibility = Visibility.Visible;
                this.MinimizeRectangle2.Visibility = Visibility.Visible;
                this.MaximizeRectangle1.Visibility = Visibility.Hidden;
                this.MaximizeRectangle2.Visibility = Visibility.Hidden;
            }
            else
            {
                this.WindowState = WindowState.Normal;
                this.MinimizeRectangle1.Visibility = Visibility.Hidden;
                this.MinimizeRectangle2.Visibility = Visibility.Hidden;
                this.MaximizeRectangle1.Visibility = Visibility.Visible;
                this.MaximizeRectangle2.Visibility = Visibility.Visible;

            }
            this.frame.Focus();

        }

        /// <summary>
        /// Reset the Scrollbar
        /// </summary>
        public void SetMyScrollViewerToTop()
        {
            myScrollviewer.ScrollToHorizontalOffset(0);
            myScrollviewer.ScrollToVerticalOffset(0);
        }

        /// <summary>
        /// Close the Window
        /// </summary>
        private void btnCloseScreen_Click(object sender, RoutedEventArgs e)
        {
            this.frame.Focus();
            closeWindow();
        }


        public void closeWindow()
        {
            Close();
        }

        /// <summary>
        /// Set the title of the Window
        /// </summary>
        public void setTitle(String newTitle)
        {
            this.Title = newTitle;
        }
    }
}
