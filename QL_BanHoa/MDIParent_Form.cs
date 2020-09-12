using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_BanHoa
{
    public partial class MDIParent_Form : Form
    {
        private int childFormNumber = 0;

        public MDIParent_Form()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        //private void OpenFile(object sender, EventArgs e)
        //{
        //    OpenFileDialog openFileDialog = new OpenFileDialog();
        //    openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        //    openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
        //    if (openFileDialog.ShowDialog(this) == DialogResult.OK)
        //    {
        //        string FileName = openFileDialog.FileName;
        //    }
        //}

        //private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    SaveFileDialog saveFileDialog = new SaveFileDialog();
        //    saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        //    saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
        //    if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
        //    {
        //        string FileName = saveFileDialog.FileName;
        //    }
        //}

    

        private void MDIParent_Form_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.WindowState = FormWindowState.Maximized;

            Point NewLoc = Screen.FromControl(this).WorkingArea.Location;
            //Modifiy the location so any toolbars & taskbar can be easily accessed.
            NewLoc.X += 1;
            NewLoc.Y += 1;
            this.Location = NewLoc;

            Size NewSize = Screen.FromControl(this).WorkingArea.Size;
            //Modifiy the size so any toolbars & taskbar can be easily accessed.
            NewSize.Height -= 1;
            NewSize.Width -= 1;
            this.Size = NewSize;

            this.MinimumSize = this.Size;
            this.MaximumSize = this.MinimumSize;
        }


        private void QuanLyNhanSuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

            if(!CheckExistForm("FrmQuanLyNhanvien"))
            {
                FrmQuanLyNhanvien frmQuanLyNhanvien = new FrmQuanLyNhanvien();
                frmQuanLyNhanvien.MdiParent = this;
                frmQuanLyNhanvien.Show();
            }
            else
            {
                ActiveChildForm("FrmQuanLyNhanvien");
            }
        }

        private bool CheckExistForm(string name)
        {
            bool check = false;

            foreach(Form frm in this.MdiChildren)
            {
                if(frm.Name == name)
                {
                    check = true;
                    break;
                }
            }

            return check;
        }

        private void ActiveChildForm(string name)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm.Name == name)
                {
                    frm.Activate();
                    break;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripStatusLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
