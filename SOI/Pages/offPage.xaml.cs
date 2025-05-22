using SOI.AppicationData;
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

namespace SOI.Pages
{
    public partial class offPage : Page
    {
        SOI_Users current_user = new SOI_Users();
        SOI_Assets current_asset = new SOI_Assets();
        public offPage(SOI_Users current_user, SOI_Assets current_asset)
        {
            InitializeComponent();
            this.current_user = current_user;
            this.current_asset = current_asset;
        }
    }
}
