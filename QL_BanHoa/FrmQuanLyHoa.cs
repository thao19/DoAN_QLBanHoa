using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;

namespace QL_BanHoa
{
    public partial class FrmQuanLyHoa : Form
    {
        BUS.Bus_Sanpham bus_sanpham = new BUS.Bus_Sanpham();
        QL_BanHoaEntities1 db = new QL_BanHoaEntities1();
        public FrmQuanLyHoa()
        {
            InitializeComponent();
        }

        private void btnChoseImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();

            opf.Filter = "Choose Image(*.JPG;*.PNG;*.GIF)|*.jpg;*.png;*.gif";

            if (opf.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(opf.FileName);
            }
        }

        private void FrmQuanLyHoa_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            dataGridView1.ClearSelection();
            LoadDataGridView();
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                if (e.Value != null)
                {
                    int loaihoa = (int)e.Value;
                    if (loaihoa == 1)
                    {
                        e.Value = "Hoa thật";
                    }
                    else
                    {
                        e.Value = "Hoa giả";
                    }
                    e.FormattingApplied = true;

                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {



            if (ValidateTextBox()  )
            {
                if(pictureBox1.Image != null)
                {
                    btnLuu.Enabled = true;
                }
                else
                {
                    MessageBox.Show("vui lòng chọn hình ảnh");
                }
                
            }
            else
            {
                MessageBox.Show("Có trường chưa nhập !");
            }


        }

        private bool ValidateTextBox()
        {
            if (string.IsNullOrEmpty(txtTensp.Text) || string.IsNullOrEmpty(txtDvt.Text) || string.IsNullOrEmpty(txtNoisanxuat.Text) || string.IsNullOrEmpty(txtGianhap.Text) || string.IsNullOrEmpty(txtGiaban.Text))
            {
                
                return false;
                
            }
            else
            {
                return true;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnXoa.Enabled = true;
            BtnCapnhat.Enabled = true;
            btnThem.Enabled = false;
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                int rowId = int.Parse(row.Cells[0].Value.ToString());
                DAL.SanPham sp = bus_sanpham.GetSanPhamByID(rowId);

                if (sp != null)
                {
                    txtMasp.Text = sp.MaSP.ToString();
                    if (sp.MaLoai == 1)
                    {
                        rbHoathat.Checked = true;
                    }
                    else
                    {
                        rbHoaGia.Checked = true;
                    }
                    txtTensp.Text = sp.TenSP;
                    txtDvt.Text = sp.DVT;
                    txtNoisanxuat.Text = sp.NoiSanXuat;
                    txtGianhap.Text = sp.GiaNhap.ToString();
                    txtGiaban.Text = sp.GiaBan.ToString();
                    dateTimePicker1.Value = sp.NgayNhap;

                    if(sp.HinhAnh != null)
                    {
                        Byte[] img = sp.HinhAnh;

                        MemoryStream ms = new MemoryStream(img);

                        pictureBox1.Image = Image.FromStream(ms);
                    }
                    

                }
                else
                {
                    MessageBox.Show("Lỗi trong quá trình xử lý");
                }

            }
        }

        private void txtGianhap_TextChanged(object sender, EventArgs e)
        {
            if (!IsNumber(txtGianhap.Text))
            {
                MessageBox.Show("Chỉ có thể nhập số ở trường này");
                txtGianhap.Text = txtGianhap.Text.Remove(txtGianhap.Text.Length - 1);
            }
        }

        private void txtGiaban_TextChanged(object sender, EventArgs e)
        {
            if (!IsNumber(txtGiaban.Text))
            {
                MessageBox.Show("Chỉ có thể nhập số ở trường này");
                txtGiaban.Text = txtGiaban.Text.Remove(txtGiaban.Text.Length - 1);
            }
        }

        public bool IsNumber(string pValue)
        {
            foreach (Char c in pValue)
            {
                if (!Char.IsDigit(c))
                    return false;
            }
            return true;
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {


        }
        private void LoadDataGridView()
        {
            var result = from c in db.SanPhams
                         select new
                         {
                             MaSP = c.MaSP,
                             MaLoai = c.MaLoai,
                             TenSP = c.TenSP,
                             DVT = c.DVT,
                             NoiSanXuat = c.NoiSanXuat,
                             GiaNhap = c.GiaNhap,
                             GiaBan = c.GiaBan,
                             NgayNhap = c.NgayNhap,
                             HinhAnh = c.HinhAnh
                         };
            dataGridView1.DataSource = result.ToList();

            dataGridView1.Columns[0].HeaderText = "Mã sp";
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView1.Columns[1].HeaderText = "Loại";
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView1.Columns[2].HeaderText = "Tên sản phẩm";
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView1.Columns[3].HeaderText = "Đơn vị tính";
            dataGridView1.Columns[4].HeaderText = "Nơi SX";
            dataGridView1.Columns[5].HeaderText = "Giá nhập";
            dataGridView1.Columns[6].HeaderText = "Giá bán";
            dataGridView1.Columns[7].HeaderText = "Ngày nhập";
            dataGridView1.Columns[8].HeaderText = "Hình ảnh";



            DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
            imgCol = (DataGridViewImageColumn)dataGridView1.Columns[8];
            imgCol.ImageLayout = DataGridViewImageCellLayout.Stretch;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


            dataGridView1.ClearSelection();
            dataGridView1.CurrentCell = null;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.EnableHeadersVisualStyles = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {

                var confirmation = MessageBox.Show("Bạn có chắc muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmation == DialogResult.Yes)
                {
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        int rowId = int.Parse(row.Cells[0].Value.ToString());

                        bus_sanpham.DeteteSanPham(rowId);
                        Clear();
                        LoadDataGridView();



                    }
                }
            }
            else
                MessageBox.Show("bạn chưa chọn item để xóa!!!");
        }
        private void Clear()
        {
            txtTensp.Text = "";
            txtDvt.Text = "";
            txtNoisanxuat.Text = "";
            txtGianhap.Text = "";
            txtGiaban.Text = "";
            dateTimePicker1.Value = DateTime.Now;

            pictureBox1.Image = null;

            btnThem.Enabled = true;
            BtnCapnhat.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = false;
        }

        private void btnMoi_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void BtnCapnhat_Click(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    int rowId = int.Parse(row.Cells[0].Value.ToString());
                    int maloai = rbHoathat.Checked ? 1 : 2;
                    string tensp = txtTensp.Text.Trim();
                    string dvt = txtDvt.Text.Trim();
                    string noisx = txtNoisanxuat.Text.Trim();
                    float gianhap = float.Parse(txtGianhap.Text.Trim());
                    float giaban = float.Parse(txtGiaban.Text.Trim());
                    DateTime ngaynhap = dateTimePicker1.Value;

                    MemoryStream ms = new MemoryStream();
                    pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                    byte[] img = ms.ToArray();


                    if (bus_sanpham.UpdateSanPham(rowId, maloai, tensp, dvt, noisx, gianhap, giaban, ngaynhap, img))
                    {
                        MessageBox.Show("Cập nhật thành công");
                        LoadDataGridView();

                        Clear();
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi trong qua trình xử lý");
                    }
                }
            }
            else
            {
                MemoryStream ms = new MemoryStream();
                pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                byte[] img = ms.ToArray();
                int value;
                if (rbHoathat.Checked)
                {
                    value = 1;
                }
                else
                {
                    value = 2;
                }

                bus_sanpham.InsertSanPham(value, txtTensp.Text, txtDvt.Text, txtNoisanxuat.Text, float.Parse(txtGianhap.Text), float.Parse(txtGiaban.Text), DateTime.Now.Date, img);

                LoadDataGridView();
            }


        }
    }
}
