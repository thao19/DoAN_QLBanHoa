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
    public partial class FrmQuanLyNhanvien : Form
    {
        BUS.Bus_Nhanvien bus_Nhanvien = new BUS.Bus_Nhanvien();
        public FrmQuanLyNhanvien()
        {
            InitializeComponent();

        }

        private void FrmQuanLyNhanvien_Load(object sender, EventArgs e)
        {

            this.WindowState = FormWindowState.Maximized;
            dataGridView1.ClearSelection();
            LoadDataToGridview();

        }

        public void LoadDataToGridview()
        {
            // TODO: This line of code loads data into the 'qL_BanHoaDataSet_Nhanvien.NhanVien' table. You can move, or remove it, as needed.
            //this.nhanVienTableAdapter.Fill(this.qL_BanHoaDataSet_Nhanvien.NhanVien);

            dataGridView1.DataSource = bus_Nhanvien.GetAllNhanvien();


            dataGridView1.Columns[0].HeaderText = "Mã nhân viên";
            dataGridView1.Columns[1].HeaderText = "Họ tên";
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView1.Columns[2].HeaderText = "Giới tính";
            dataGridView1.Columns[3].HeaderText = "Ngày sinh";
            dataGridView1.Columns[4].HeaderText = "Quê quán";
            dataGridView1.Columns[5].HeaderText = "Địa chỉ";
            dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[6].HeaderText = "Điện thoại";
            dataGridView1.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView1.Columns[7].HeaderText = "Email";
            dataGridView1.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dataGridView1.Columns[8].HeaderText = "";
            dataGridView1.Columns[9].HeaderText = "";
            dataGridView1.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView1.Columns[9].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;


            dataGridView1.ClearSelection();
            dataGridView1.CurrentCell = null;
            dataGridView1.EnableHeadersVisualStyles = false;
            cbbQuyenquantri.DataSource = bus_Nhanvien.getAllQuyen();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                if (e.Value is bool)
                {
                    e.Value = ((bool)e.Value) ? "nam" : "nữ";
                    e.FormattingApplied = true;

                }
            }

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (ValidateTextBox())
            {
                btnLuu.Enabled = true;
            }
            else
            {
                MessageBox.Show("Có vẻ bạn chưa nhập hết các trường !");
            }


        }

        private bool ValidateTextBox()
        {
            if (string.IsNullOrEmpty(txtTennv.Text.Trim()) || string.IsNullOrEmpty(txtQueQuan.Text.Trim()) || string.IsNullOrEmpty(txtDiachi.Text.Trim()) || string.IsNullOrEmpty(txtEmail.Text.Trim()) || string.IsNullOrEmpty(txtDienthoai.Text.Trim()))
                return false;
            else
                return true;

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnXoa.Enabled = true;
            BtnCapnhat.Enabled = true;
            btnThem.Enabled = false;
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                int rowId = int.Parse(row.Cells[0].Value.ToString());
                DAL.NhanVien nv = bus_Nhanvien.GetNhanvienByID(rowId);
                DAL.TaiKhoan taiKhoanNV = bus_Nhanvien.GetTaiKhoanNhanVien(rowId);
                if (nv != null)
                {
                    txtManv.Text = nv.MaNV.ToString();
                    txtTennv.Text = nv.HoTen;
                    if (nv.Gioitinh == true)
                    {
                        rbnam.Checked = true;
                    }
                    else
                    {
                        rbnu.Checked = true;
                    }
                    dtbNgaysinh.Value = Convert.ToDateTime(nv.NgaySinh);
                    txtQueQuan.Text = nv.QueQuan;
                    txtDiachi.Text = nv.DiaChi;
                    txtDienthoai.Text = nv.SDT;
                    txtEmail.Text = nv.Email;

                    if (taiKhoanNV != null)
                    {
                        txtTaikhoan.Text = taiKhoanNV.TaiKhoan1;
                        txtTaikhoan.Enabled = false;

                        txtMatkhau.Text = taiKhoanNV.MatKhau;
                        cbbQuyenquantri.SelectedIndex = taiKhoanNV.MaQuyen - 1;

                        btncapnhattk.Enabled = true;
                    }
                    else
                    {
                        txtTaikhoan.Text = "";
                        txtMatkhau.Text = "";
                        cbbQuyenquantri.SelectedIndex = 0;
                        lbtaikhoan.Visible = true;
                        txtTaikhoan.Enabled = true;
                        btncapnhattk.Enabled = false;
                        btnThemtk.Enabled = true;
                    }

                }
                else
                {
                    MessageBox.Show("Lỗi trong quá trình xử lý");
                }

            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                //if (dataGridView1.SelectedRows.Count >= 2)
                //{
                //    btnSua.Enabled = false;
                //}
                var confirmation = MessageBox.Show("Bạn có chắc muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmation == DialogResult.Yes)
                {
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        int rowId = int.Parse(row.Cells[0].Value.ToString());

                        bus_Nhanvien.DeteteNhanvien(rowId);
                        Clear();
                        LoadDataToGridview();
                    }
                }
            }
            else
                MessageBox.Show("bạn chưa chọn item để xóa!!!");

        }

        public void Clear()
        {
            txtManv.Text = "";
            txtTennv.Text = "";
            rbnam.Checked = true;
            dtbNgaysinh.Value = DateTime.Now;
            txtQueQuan.Text = "";
            txtDiachi.Text = "";
            txtDienthoai.Text = "";
            txtEmail.Text = "";
            txtTaikhoan.Text = "";
            txtMatkhau.Text = "";
            cbbQuyenquantri.SelectedIndex = 0;

            btnThem.Enabled = true;
            BtnCapnhat.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = false;
            btncapnhattk.Enabled = false;
            btnThemtk.Enabled = false;
            dataGridView1.ClearSelection();
        }

        private void btnMoi_Click(object sender, EventArgs e)
        {
            Clear();
            LoadDataToGridview();
        }

        private void BtnCapnhat_Click(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
        }

        private void txtDienthoai_TextChanged(object sender, EventArgs e)
        {
            if (!IsNumber(txtDienthoai.Text))
            {
                MessageBox.Show("Chỉ có thể nhập số ở trường này");
                txtDienthoai.Text = txtDienthoai.Text.Remove(txtDienthoai.Text.Length - 1);
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

        private void btnThemtk_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                int rowId = int.Parse(row.Cells[0].Value.ToString());
                if (ValidateTK())
                {
                    if (bus_Nhanvien.InsertTaiKhoanNV(rowId, txtTaikhoan.Text, txtMatkhau.Text, cbbQuyenquantri.SelectedIndex + 1))
                    {
                        MessageBox.Show("Thêm tài khoản thành công");
                        lbtaikhoan.Visible = false;
                    }
                    else
                    {
                        MessageBox.Show("Thêm tài khoản thất bại");
                    }
                }
            }
        }

        private bool ValidateTK()
        {
            if (string.IsNullOrEmpty(txtTaikhoan.Text) || string.IsNullOrEmpty(txtMatkhau.Text))
            {
                return false;
            }
            else return true;
        }

        private void btncapnhattk_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                int rowId = int.Parse(row.Cells[0].Value.ToString());
                if (ValidateTK())
                {
                    if (bus_Nhanvien.UpdateTaiKhoanNV(rowId, txtTaikhoan.Text, txtMatkhau.Text, cbbQuyenquantri.SelectedIndex + 1))
                    {
                        MessageBox.Show("Cập nhật tài khoản thành công");
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật tài khoản thất bại");
                    }
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(txttimnv.Text))
            {
                List<DAL.NhanVien> listnv = bus_Nhanvien.FindNVWithName(txttimnv.Text);
                if (listnv != null)
                {
                    dataGridView1.DataSource = bus_Nhanvien.FindNVWithName(txttimnv.Text);
                    dataGridView1.ClearSelection();
                    dataGridView1.CurrentCell = null;
                    dataGridView1.EnableHeadersVisualStyles = false;
                    cbbQuyenquantri.DataSource = bus_Nhanvien.getAllQuyen();
                }
            }
            else
            {
                LoadDataToGridview();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0)
            {
                //if (dataGridView1.SelectedRows.Count >= 2)
                //{
                //    btnSua.Enabled = false;
                //}

                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    int rowId = int.Parse(row.Cells[0].Value.ToString());
                    string ten = txtTennv.Text.Trim();
                    bool gioitinh = rbnam.Checked ? true : false;
                    DateTime ngaysinh = dtbNgaysinh.Value;
                    string quequan = txtQueQuan.Text.Trim();
                    string diachi = txtDiachi.Text.Trim();
                    string dienthoai = txtDienthoai.Text.Trim();
                    string email = txtEmail.Text.Trim();
                    if (bus_Nhanvien.UpdateNhanvien(rowId, ten, gioitinh, ngaysinh, quequan, diachi, dienthoai, email))
                    {
                        MessageBox.Show("Cập nhật thành công");
                        LoadDataToGridview();

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
                string ten = txtTennv.Text.Trim();
                bool gioitinh = rbnam.Checked ? true : false;
                DateTime ngaysinh = dtbNgaysinh.Value;
                string quequan = txtQueQuan.Text.Trim();
                string diachi = txtDiachi.Text.Trim();
                string dienthoai = txtDienthoai.Text.Trim();
                string email = txtEmail.Text.Trim();

                if (bus_Nhanvien.InsertNhanvien(ten, gioitinh, ngaysinh, quequan, diachi, dienthoai, email))
                {
                    MessageBox.Show("Thêm thành công");
                    LoadDataToGridview();
                    Clear();
                }
                else
                {
                    MessageBox.Show("Có lỗi trong qua trình xử lý");
                }

            }
        }
    }
}
