using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;



namespace WindowsFormsApp3
{

  public partial class Form1 : Form
  {

    [DllImport("user32.dll", EntryPoint = "FindWindowEx")]
    public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

    [DllImport("User32.dll")]
    public static extern int SendMessage(IntPtr hWnd, int uMsg, int wParam, string lParam);

    internal delegate int WindowEnumProc(IntPtr hwnd, IntPtr lparam);

    [DllImport("user32.dll")]
    internal static extern bool EnumChildWindows(IntPtr hwnd, WindowEnumProc func, IntPtr lParam);


    public Form1()
    {
      InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {

      var process = Process.GetProcesses();

      var listItems = process.Select(x => x.ProcessName).ToArray();

      this.listBox1.Items.AddRange(listItems);


      Process[] notepads = Process.GetProcessesByName("TIM"); if (notepads.Length == 0) return; if (notepads[0] != null)
      {

        IntPtr child = FindWindowEx(notepads[0].MainWindowHandle, new IntPtr(0), "Edit", null);
        var allChildWindows = new WindowHandleInfo(notepads[0].MainWindowHandle).GetAllChildHandles();
        var allChildItems = allChildWindows.Select(x => x.ToString()).ToArray();
        this.listBox2.Items.AddRange(allChildItems);
        if (child != IntPtr.Zero)
        {
          SendMessage(child, 0x000C, 0, "插入了文本");
        }
      }
    }

  }
}
