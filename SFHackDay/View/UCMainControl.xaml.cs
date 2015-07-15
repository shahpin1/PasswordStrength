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
using SFHackDay.ViewModel;

namespace SFHackDay.View
{
    /// <summary>
    /// Interaction logic for UCMainControl.xaml
    /// </summary>
    public partial class UCMainControl : UserControl
    {
        public UCMainControl()
        {
            InitializeComponent();
            this.BNSubmit.IsEnabled = false;
            this.DataContext = new VMMainControl(); 
        }

        private void PBPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            VMMainControl vm = (VMMainControl)this.DataContext;
           if (sender is PasswordBox)
            {
                vm.DoSomething(((PasswordBox)sender));
               if (((PasswordBox)sender).Password.Count()>0)
                {
                    this.BNSubmit.IsEnabled = true;
                }
               else
               {
                   this.BNSubmit.IsEnabled = false;
               }
            }
        }

    }
}
