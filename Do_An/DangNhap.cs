using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Do_An_PhanTienHuy_NguyenHuuToan
{
    public partial class fr_dangnhap : Form
    {
        public fr_dangnhap()
        {
            InitializeComponent();
        }
        
        private void cb_matkhau_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_matkhau.Checked)
            {
                txt_matkhau.PasswordChar = '\0';
                lb_matkhau.Text = "Hiện mật khẩu";
            }
            else
            {
                txt_matkhau.PasswordChar = '*';
            }
        }

        private void lb_dangky_Click(object sender, EventArgs e)
        {
           fr_dangky dangky=new fr_dangky();
           dangky.ShowDialog();
        }

        private void bt_dangnhap_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_taikhoan.Text != "" && txt_matkhau.Text != "")
                {
                    string connect = "server=" + @"DESKTOP-1VK71I1\SQLEXPRESS" + ";database=" + "DoAn.Net" + ";integrated security=true";
                    SqlConnection con = new SqlConnection(connect);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM NguoiDung WHERE TenDangNhap='" + txt_taikhoan.Text + "' AND MatKhau='" + txt_matkhau.Text + "'", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        NguoiDung.HoVaTen = dt.Rows[0]["HoVaTen"].ToString();
                        NguoiDung.id = Convert.ToInt32(dt.Rows[0]["id"].ToString());
                        NguoiDung.Email = dt.Rows[0]["Email"].ToString();
                        NguoiDung.Role= dt.Rows[0]["Role"].ToString();
                        MessageBox.Show("Đăng nhập thành công");
                        fr_trangchu trangchu = new fr_trangchu();
                        this.Hide();
                        trangchu.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Đăng nhập không thành công! Kiểm tra thông tin đăng nhập");
                    }
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin đăng nhâp!");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
