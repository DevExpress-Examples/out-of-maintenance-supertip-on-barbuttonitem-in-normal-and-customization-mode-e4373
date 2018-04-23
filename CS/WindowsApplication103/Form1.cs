using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraBars;
using System.Reflection;
using System.Collections;

namespace WindowsApplication103
{
    public partial class Form1 : Form, IMessageFilter
    {
        public Form1()
        {
            InitializeComponent();
            Application.AddMessageFilter(this);
            ToolTipList.Add(new ToolTipObject(barButtonItem1,"ToolTip","CToolTip"));
            ToolTipList.Add(new ToolTipObject(barButtonItem2,"AnotherToolTip","ToolTipShownInCustomizationMode"));
            ToolTipList.Add(new ToolTipObject(barButtonItem3, "Some","It's Work!"));

        }

        List<ToolTipObject> ToolTipList = new List<ToolTipObject>();
        const int WM_MouseMove = 0x0200;


        private Rectangle GetLinksScreenRect(BarItemLink link)
        {
            PropertyInfo info = typeof(BarItemLink).GetProperty("BarControl", BindingFlags.Instance | BindingFlags.NonPublic);
            Control c = (Control)info.GetValue(link, null);
            return c.RectangleToScreen(link.Bounds);
        }



        #region IMessageFilter Members

        public bool PreFilterMessage(ref Message m)
        {
                Rectangle BarItemRect = new Rectangle();
                int index = 0;
                if (m.Msg == WM_MouseMove)
                {
                    for (int i = 0; i < ToolTipList.Count; i++)
                    {
                        try
                        {
                            BarItemRect = GetLinksScreenRect(ToolTipList[i].ToolTipItem.Links[0]);
                        }
                        catch { }
                        if (!BarItemRect.IsEmpty)
                            if (BarItemRect.Contains(MousePosition)) { index = i; break; }

                    }

                    if (BarItemRect.Contains(MousePosition))
                    {
                        ToolTipControllerShowEventArgs te = new ToolTipControllerShowEventArgs();
                        te.ToolTipLocation = ToolTipLocation.Fixed;
                        te.SuperTip = new SuperToolTip();
                        te.SuperTip.Items.Add(!barManager1.IsCustomizing? ToolTipList[index].ToolTip : ToolTipList[index].ToolTipCustomizationMode);
                        Point linkPoint = new Point(BarItemRect.Right, BarItemRect.Bottom);
                        barManager1.GetToolTipController().ShowHint(te, linkPoint);
                    }
                    else barManager1.GetToolTipController().HideHint();

                }
            
            return false;
        }

        #endregion
    }
}