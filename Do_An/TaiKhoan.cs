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
    public partial class fr_taikhoan : Form
    {
        string connect = "server=" + @"DESKTOP-1VK71I1\SQLEXPRESS" + ";database=" + "DoAn.Net" + ";integrated security=true";
        SqlConnection con = new SqlConnection();
        public fr_taikhoan()
        {
            InitializeComponent();
        }
        public void dis_nguoidung()
        {
            try
            {
                    SqlConnection con = new SqlConnection(connect);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM NguoiDung WHERE id='" + NguoiDung.id + "'", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        txt_hovaten.Text = dt.Rows[0]["HoVaTen"].ToString().Trim();
                        txt_sodienthoai.Text= dt.Rows[0]["SoDienThoai"].ToString().Trim();
                        txt_email.Text= dt.Rows[0]["Email"].ToString().Trim();
                        txt_diachi.Text= dt.Rows[0]["DiaChi"].ToString().Trim();

                }
                    con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void cl_nguoidung()
        {
            txt_matkhaucu.Text = "";
            txt_matkhaumoi.Text = "";
            txt_nhaplai.Text = "";
            dis_nguoidung();
        }

        private void fr_taikhoan_Load(object sender, EventArgs e)
        {
            dis_nguoidung();
            txt_hovaten.Enabled = false;
        }

        private void btn_sua_Click(object sender, EventArgs e)
        { 
            if(txt_email.Text!=""&&txt_sodienthoai.Text!=""&&txt_diachi.Text!="")
            {
                try
                {
                    SqlConnection con = new SqlConnection(connect);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE NguoiDung SET SoDienThoai=@sodienthoai,Email=@email,DiaChi=@diachi WHERE id=@id", con);
                    cmd.Parameters.AddWithValue("@sodienthoai", txt_sodienthoai.Text);
                    cmd.Parameters.AddWithValue("@email", txt_email.Text);
                    cmd.Parameters.AddWithValue("@diachi", txt_diachi.Text);
                    cmd.Parameters.AddWithValue("@id", NguoiDung.id);
                    int rowsaffected = cmd.ExecuteNonQuery();
                    if (rowsaffected == 1)
                    {
                        MessageBox.Show("Sửa thông tin thành công");
                    }
                    dis_nguoidung();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin phiếu nhập!");
            }
        }

        private void txt_doimatkhau_Click(object sender, EventArgs e)
        {
            if (txt_matkhaucu.Text != "" && txt_matkhaumoi.Text != "" && txt_nhaplai.Text != "")
            {
                try
                {
                    SqlConnection con = new SqlConnection(connect);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM NguoiDung WHERE id='" + NguoiDung.id + "' AND MatKhau='"+txt_matkhaucu.Text+"'", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        if (txt_matkhaumoi.Text == txt_nhaplai.Text)
                        {
                            cmd = new SqlCommand("UPDATE NguoiDung SET MatKhau=@matkhaumoi WHERE id=@id", con);
                            cmd.Parameters.AddWithValue("@matkhaumoi", txt_matkhaumoi.Text);
                            cmd.Parameters.AddWithValue("@id", NguoiDung.id);
                            int rowsaffected = cmd.ExecuteNonQuery();
                            if (rowsaffected == 1)
                            {
                                MessageBox.Show("Đổi mật khẩu thành công");
                            }
                            dis_nguoidung();
                            cl_nguoidung();
                        }
                        else
                        {
                            MessageBox.Show("Mật khẩu nhập lại chưa đúng!");
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("Vui lòng điền đủ thông tin mật khẩu!");
            }
            
        }
    }
}
