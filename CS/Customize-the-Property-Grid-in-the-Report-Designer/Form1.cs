using DevExpress.Utils.Svg;
using DevExpress.XtraReports.Design;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.UserDesigner;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Customize_the_Property_Grid_in_the_Report_Designer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            XtraReport.FilterComponentProperties += XtraReport_FilterComponentProperties;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            XtraReport report = new XtraReport1();
            ReportDesignTool designTool = new ReportDesignTool(report);
            designTool.DesignRibbonForm.DesignMdiController.DesignPanelLoaded += ReportDesigner1_DesignPanelLoaded;
            designTool.ShowRibbonDesignerDialog();
        }
        private void XtraReport_FilterComponentProperties(object sender, FilterComponentPropertiesEventArgs e)
        {
            PropertyDescriptor propertyDescriptor1 = e.Properties["PaperKind"] as PropertyDescriptor;
            if (propertyDescriptor1 != null)
            {
                List<Attribute> attributes = new List<Attribute>(propertyDescriptor1.Attributes.Cast<Attribute>().Where(att => !(att is PropertyGridTabAttribute)));
                attributes.Add(new PropertyGridTabAttribute("My tab"));
                e.Properties["PaperKind"] = TypeDescriptor.CreateProperty(
                    propertyDescriptor1.ComponentType,
                    propertyDescriptor1,
                    attributes.ToArray());
            }
        }
        private void ReportDesigner1_DesignPanelLoaded(object sender, DevExpress.XtraReports.UserDesigner.DesignerLoadedEventArgs e)
        {
            // Access the tab icon provider
            IPropertyGridIconsProvider propertyGridImagesProvider = (IPropertyGridIconsProvider)e.DesignerHost.GetService(typeof(IPropertyGridIconsProvider));

            // Assign an svg icon to the "My tab" tab if it does not have any
            if (!propertyGridImagesProvider.Icons.ContainsKey("My tab"))
            {
                SvgImage customTabIcon = SvgImage.FromFile(@"..\..\CustomTabIcon.svg");
                propertyGridImagesProvider.Icons.Add("My tab", new IconImage(customTabIcon));
            }
        }
    }
}
