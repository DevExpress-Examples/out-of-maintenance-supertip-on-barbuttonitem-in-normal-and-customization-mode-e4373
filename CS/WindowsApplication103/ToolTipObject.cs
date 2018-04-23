using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraBars;

namespace WindowsApplication103
{
    class ToolTipObject
    {
        public string ToolTip;
        public BarButtonItem ToolTipItem;
        public string ToolTipCustomizationMode;
        public ToolTipObject(BarButtonItem _toolTipItem, String _toolTip,string _toolTipCustocustomizationMode)
        {
            ToolTipItem = _toolTipItem;
            ToolTip = _toolTip;
            ToolTipCustomizationMode = _toolTipCustocustomizationMode;
        }

    }
}
