using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace RockSmithTabExplorer.Services
{
    public class PrintService
    {
        public void Print(RockSmithTabExplorer.Controls.TabControl tabControl, string jobTitle)
            {
                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    System.Printing.PrintCapabilities capabilities = printDialog.PrintQueue.GetPrintCapabilities(printDialog.PrintTicket);
                    double scale = Math.Min(capabilities.PageImageableArea.ExtentWidth / tabControl.ActualWidth, capabilities.PageImageableArea.ExtentHeight / tabControl.ActualHeight);
                    //DoWithElementAtSize(tabControl, printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight, () =>
                    //{

                    //Transform the Visual to scale
                    tabControl.LayoutTransform = new ScaleTransform(scale, scale);

                    //get the size of the printer page
                    Size sz = new Size(capabilities.PageImageableArea.ExtentWidth, capabilities.PageImageableArea.ExtentHeight);

                    //update the layout of the visual to the printer page size.
                    tabControl.Measure(sz);
                    tabControl.Arrange(new Rect(new Point(capabilities.PageImageableArea.OriginWidth, capabilities.PageImageableArea.OriginHeight), sz));
                        
                        printDialog.PrintVisual(tabControl, jobTitle);
                    //});
                }
            }
    }
}
