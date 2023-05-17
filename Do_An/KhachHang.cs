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

namespace Do_An
{
    public partial class fr_khachhang : Form
    {
        SqlCommand cmd;
        string connect = "server=" + @"DESKTOP-K5LRVGB\SQLEXPRESS" + ";database=" + "DoAn.Net" + ";integrated security=true";
        SqlConnection con = new SqlConnection();
        public fr_khachhang()
        {
            InitializeComponent();
        }

        private void KhachHang_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the '_DoAn_NetDataSet1.KhachHang' table. You can move, or remove it, as needed.
 
            dis_khachhang();
            /*       bt_sua.Enabled = false;
                   bt_xoa.Enabled = false;*/

            if (NguoiDung.Role == "Admin")
            {
                btn_nhanvien.Visible = true;
            }
            else
            {
                btn_nhanvien.Visible = false;
            }

        }
        public void dis_khachhang()
        {
            try
            {
                con = new SqlConnection(connect);
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM KhachHang", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gv_khachhang.DataSource = dt;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void cl_khachhang()
        {
            txt_makhachhang.Text = "";
            txt_tenkhachhang.Text = "";
            txt_sodienthoai.Text = "";
            txt_diachi.Text = "";
            txt_timkiem.Text = "";
            bt_sua.Enabled = false;
            bt_xoa.Enabled = false;
        }
        private void bt_them_Click(object sender, EventArgs e)
        {
            if (txt_makhachhang.Text != "" && txt_tenkhachhang.Text != "" && txt_sodienthoai.Text != "" && txt_diachi.Text != "")
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO KhachHang(MaKhachHang,TenKhachHang,SoDienThoai,DiaChi) VALUES(@makhachhang,@tenkhachhang,@sodienthoai,@diachi)", con);
                    cmd.Parameters.AddWithValue("@makhachhang", txt_makhachhang.Text);
                    cmd.Parameters.AddWithValue("@tenkhachhang", txt_tenkhachhang.Text);
                    cmd.Parameters.AddWithValue("@sodienthoai", txt_sodienthoai.Text);
                    cmd.Parameters.AddWithValue("@diachi", txt_diachi.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm khách hàng thành công");
                    dis_khachhang();
                    cl_khachhang();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    
                }
            }
            else
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin khách hàng!");
            }
        }

        private void bt_sua_Click(object sender, EventArgs e)
        {
            if (txt_makhachhang.Text != "" && txt_tenkhachhang.Text != "" && txt_sodienthoai.Text != "" && txt_diachi.Text != "")
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE KhachHang SET TenKhachHang=@tenkhachhang,SoDienThoai=@sodienthoai,DiaChi=@diachi WHERE MaKhachHang=@makhachhang", con);
                    cmd.Parameters.AddWithValue("@tenkhachhang", txt_tenkhachhang.Text);
                    cmd.Parameters.AddWithValue("@sodienthoai", txt_sodienthoai.Text);
                    cmd.Parameters.AddWithValue("@diachi", txt_diachi.Text);
                    cmd.Parameters.AddWithValue("@makhachhang", txt_makhachhang.Text);
                    int rowsaffected = cmd.ExecuteNonQuery();
                    if (rowsaffected == 1)
                    {
                        MessageBox.Show("Sửa thông tin khách hàng thành công");
                    }
                    dis_khachhang();
                    cl_khachhang();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin khách hàng!");
            }
        }

        private void gv_khachhang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            int rows_id = e.RowIndex;
            txt_makhachhang.Text = gv_khachhang.Rows[rows_id].Cells[0].Value.ToString().Trim();
            txt_tenkhachhang.Text = gv_khachhang.Rows[rows_id].Cells[1].Value.ToString().Trim();
            txt_sodienthoai.Text = gv_khachhang.Rows[rows_id].Cells[2].Value.ToString().Trim();
            txt_diachi.Text = gv_khachhang.Rows[rows_id].Cells[3].Value.ToString().Trim();
            /*bt_sua.Enabled = true;
            bt_xoa.Enabled = true;*/
        }

        private void bt_lammoi_Click(object sender, EventArgs e)
        {
            cl_khachhang();
        }

        private void bt_xoa_Click(object sender, EventArgs e)
        {
            if (txt_makhachhang.Text != "")
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE KhachHang WHERE MaKhachHang=@makhachhang", con);
                    cmd.Parameters.AddWithValue("@makhachhang", txt_makhachhang.Text);
                    int rowsaffected = cmd.ExecuteNonQuery();
                    if (rowsaffected == 1)
                    {
                        MessageBox.Show("Xóa khách hàng thành công");
                    }
                    dis_khachhang();
                    cl_khachhang();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("Vui chọn mã khách hàng cần xóa!");
            }
        }

        private void txt_timkiem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(connect);
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM KhachHang WHERE TenKhachHang LIKE N'%" + txt_timkiem.Text + "%'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gv_khachhang.DataSource = dt;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btn_trangchu_Click(object sender, EventArgs e)
        {

            fr_trangchu trangchu = new fr_trangchu();
            this.Hide();
            trangchu.ShowDialog();
        }

        private void btn_sanpham_Click(object sender, EventArgs e)
        {
            fr_sanpham sanpham = new fr_sanpham();
            this.Hide();
            sanpham.ShowDialog();
        }

        private void btn_nhaphang_Click(object sender, EventArgs e)
        {
            fr_nhaphang nhanhang = new fr_nhaphang();
            this.Hide();
            nhanhang.ShowDialog();
        }

        private void bnt_taikhoan_Click(object sender, EventArgs e)
        {
            fr_taikhoan taikhoan = new fr_taikhoan();
            taikhoan.ShowDialog();
        }

        private void btn_hoadon_Click(object sender, EventArgs e)
        {
            fr_hoadon hoadon = new fr_hoadon();
            this.Hide();
            hoadon.ShowDialog();
        }

        private void btn_nhanvien_Click(object sender, EventArgs e)
        {
            fr_nhanvien nhanvien = new fr_nhanvien();
            this.Hide();
            nhanvien.ShowDialog();
        }

        private void btn_dangxuat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Đăng xuất ngay!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                fr_dangnhap dangnhap = new fr_dangnhap();
                this.Hide();
                dangnhap.ShowDialog();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
