using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;

namespace QL_BanHoa
{

    public partial class Login : Form
    {

        BUS.Bus_Nhanvien bus = new BUS.Bus_Nhanvien();
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
           
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string tk = txtTaikhoan.Text.Trim();
            string mk = txtMatkhau.Text.Trim();

            if(String.IsNullOrEmpty(tk))
            {
                MessageBox.Show("Bạn chưa nhập tài khoản");
                txtTaikhoan.Focus();
            }
            else
            {
                if(String.IsNullOrEmpty(mk))
                {
                    MessageBox.Show("Bạn chưa nhập mật khẩu");
                    txtMatkhau.Focus();
                }
                else
                {
                    if (bus.CheckLogin(tk,mk))
                    {
                        MessageBox.Show("Thành công");
                        MDIParent_Form mDIParent_Form = new MDIParent_Form();
                        this.Hide();
                        mDIParent_Form.Show();
                    }
                    else
                    {
                        MessageBox.Show("Thất bại");
                    }
                }
            }
                     

        }


        private void txtTaikhoan_TextChanged(object sender, EventArgs e)
        {
            string specialChar = @"\|!#$%&/()=?»«@£§€{}.-;'<>_,";
            foreach (var item in specialChar)
            {
                if (txtTaikhoan.Text.Contains(item))
                {
                    txtTaikhoan.Text = txtTaikhoan.Text.Remove(txtTaikhoan.Text.Length - 1);
                    MessageBox.Show("Không được nhập ký tự đặc biệt vào tài khoản");
                }
            }
        }

        private void txtMatkhau_TextChanged(object sender, EventArgs e)
        {
            string specialChar = @"\|!#$%&/()=?»«@£§€{}.-;'<>_,";
            foreach (var item in specialChar)
            {
                if (txtMatkhau.Text.Contains(item))
                {
                    txtMatkhau.Text = txtMatkhau.Text.Remove(txtMatkhau.Text.Length - 1);
                    MessageBox.Show("Không được nhập ký tự đặc biệt vào mật khẩu");
                }
            }
        }
    }
}
