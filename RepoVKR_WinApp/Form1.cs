using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RepoVKR_WinApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnStartImport_Click(object sender, EventArgs e)
        {
            Do();
        }

        private void Do()
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += (sender, e) => 
            {
                if (e.Argument == null)
                    return;

                string path = e.Argument.ToString();
                BackgroundWorker w = (BackgroundWorker)sender;
                ImportClass.Import(ref w, path);
            };

            bw.ProgressChanged += (sender, e) =>
            {
                progressBar.Value = e.ProgressPercentage;
                if (e.UserState != null)
                {
                    BackgroundWorkerStateClass state = e.UserState as BackgroundWorkerStateClass;
                    lblAllCount.Text = state.AllCount.ToString();
                    lblCount.Text = state.Count.ToString();
                    lblCurrentCatalog.Text = state.CurrentCatalog;
                    lblCurrentFIO.Text = state.CurrentFIO;

                    if (!string.IsNullOrEmpty(state.CurrentBigFile))
                        lblBigFileName.Text = state.CurrentBigFile;
                    else
                        lblBigFileName.Text = "";
                }
            };

            bw.RunWorkerCompleted += (sender, e) =>
            {
                lblCurrentCatalog.Text = "";
                lblCurrentFIO.Text = "";
                lblBigFileName.Text = "Импорт завершён";
            };

            bw.WorkerReportsProgress = true;

            FolderBrowserDialog dlg = new FolderBrowserDialog();
            var dr = dlg.ShowDialog();

            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                string sPath = dlg.SelectedPath;
                bw.RunWorkerAsync(sPath);
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            ImportClass.TestFile();
            MessageBox.Show("Done");
        }
    }
}
