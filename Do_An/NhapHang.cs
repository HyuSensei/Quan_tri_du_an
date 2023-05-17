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
    public partial class fr_nhaphang : Form
    {
        SqlCommand cmd;
        string connect = "server=" + @"DESKTOP-1VK71I1\SQLEXPRESS" + ";database=" + "DoAn.Net" + ";integrated security=true";
        SqlConnection con = new SqlConnection();
        public fr_nhaphang()
        {
            InitializeComponent();
        }

        private void fr_nhaphang_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the '_DoAn_NetDataSet2.PhieuNhap' table. You can move, or remove it, as needed.
            dis_nhaphang();
            get_tennhanvien();
            bt_sua.Enabled = false;
            bt_xoa.Enabled = false;
        }
       public void dis_nhaphang()
        {
            try
            {
                con = new SqlConnection(connect);
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM PhieuNhap", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gv_nhaphang.DataSource = dt;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void cl_nhaphang()
        {
            txt_maphieunhap.Text = "";
            txt_manhanvien.Text = "";
            cb_tennhanvien.SelectedIndex = -1;
            txt_tensanpham.Text = "";
            txt_soluong.Text = "";
            txt_tiennhap.Text = "";
            txt_ngaynhap.Text = "";
            bt_sua.Enabled = false;
            bt_xoa.Enabled = false;
        }
        public void get_tennhanvien()
        {
            try
            {
                con = new SqlConnection(connect);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM NhanVien", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                DataRow row = dt.NewRow();
                row[0] = 0;
                row[1] = "Chọn nhân viên";
                dt.Rows.InsertAt(row, 0);
                cb_tennhanvien.DataSource = dt;
                cb_tennhanvien.DisplayMember = "TenNhanVien";
                cb_tennhanvien.ValueMember = "MaNhanVien";
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void bt_them_Click(object sender, EventArgs e)
        {
            if (txt_maphieunhap.Text != "" && txt_manhanvien.Text != "" && cb_tennhanvien.Text != "" && txt_ngaynhap.Text != ""&& txt_soluong.Text != ""&& txt_tiennhap.Text != ""&&txt_tensanpham.Text!="")
            {
                try
                {
                    con = new SqlConnection(connect);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO PhieuNhap(MaPhieuNhap,MaNhanVien,TenNhanVien,TenSanPham,SoLuong,TienNhap,NgayNhap) VALUES(@maphieunhap,@manhanvien,@tennhanvien,@tensanpham,@soluong,@tiennhap,@ngaynhap)", con);
                    cmd.Parameters.AddWithValue("@maphieunhap", txt_maphieunhap.Text);
                    cmd.Parameters.AddWithValue("@manhanvien", txt_manhanvien.Text);
                    cmd.Parameters.AddWithValue("@tennhanvien", cb_tennhanvien.Text);
                    cmd.Parameters.AddWithValue("@tensanpham", txt_tensanpham.Text);
                    cmd.Parameters.AddWithValue("@soluong", txt_soluong.Text);
                    cmd.Parameters.AddWithValue("@tiennhap", txt_tiennhap.Text);
                    cmd.Parameters.AddWithValue("@ngaynhap", txt_ngaynhap.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm phiếu nhập thành công");
                    dis_nhaphang();
                    cl_nhaphang();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());

                }
            }
            else
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin nhập hàng!");
            }
        }

        private void cb_tennhanvien_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(connect);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM NhanVien WHERE TenNhanVien=N'" + cb_tennhanvien.Text + "'", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    txt_manhanvien.Text = dt.Rows[0]["MaNhanVien"].ToString();
                }
                if(cb_tennhanvien.Text== "Chọn nhân viên")
                {
                    txt_manhanvien.Text = "";
                }         
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void bt_lammoi_Click(object sender, EventArgs e)
        {
            cl_nhaphang();
        }

        private void gv_nhaphang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int rows_id = e.RowIndex;
            txt_maphieunhap.Text = gv_nhaphang.Rows[rows_id].Cells[0].Value.ToString().Trim();
            txt_manhanvien.Text = gv_nhaphang.Rows[rows_id].Cells[1].Value.ToString().Trim();
            cb_tennhanvien.Text = gv_nhaphang.Rows[rows_id].Cells[2].Value.ToString().Trim();
            txt_tensanpham.Text = gv_nhaphang.Rows[rows_id].Cells[3].Value.ToString().Trim();
            txt_soluong.Text = gv_nhaphang.Rows[rows_id].Cells[4].Value.ToString().Trim();
            txt_tiennhap.Text = gv_nhaphang.Rows[rows_id].Cells[5].Value.ToString().Trim();
            txt_ngaynhap.Text = gv_nhaphang.Rows[rows_id].Cells[6].Value.ToString().Trim();
            bt_sua.Enabled = true;
            bt_xoa.Enabled = true;
        }

        private void bt_sua_Click(object sender, EventArgs e)
        {
            if (txt_maphieunhap.Text != "" && txt_manhanvien.Text != "" && cb_tennhanvien.Text != "" && txt_ngaynhap.Text != "" && txt_soluong.Text != "" && txt_tiennhap.Text != "" && txt_tensanpham.Text != "")
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE PhieuNhap SET MaNhanVien=@manhanvien,TenNhanVien=@tennhanvien,TenSanPham=@tensanpham,SoLuong=@soluong,TienNhap=@tiennhap,NgayNhap=@ngaynhap WHERE MaPhieuNhap=@maphieunhap", con);
                    cmd.Parameters.AddWithValue("@manhanvien", txt_manhanvien.Text);
                    cmd.Parameters.AddWithValue("@tennhanvien", cb_tennhanvien.Text);
                    cmd.Parameters.AddWithValue("@tensanpham", txt_tensanpham.Text);
                    cmd.Parameters.AddWithValue("@soluong", txt_soluong.Text);
                    cmd.Parameters.AddWithValue("@tiennhap", txt_tiennhap.Text);
                    cmd.Parameters.AddWithValue("@ngaynhap", txt_ngaynhap.Text);
                    cmd.Parameters.AddWithValue("@maphieunhap", txt_maphieunhap.Text);
                    int rowsaffected = cmd.ExecuteNonQuery();
                    if (rowsaffected == 1)
                    {
                        MessageBox.Show("Sửa thông tin khách hàng thành công");
                    }
                    dis_nhaphang();
                    cl_nhaphang();
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

        private void bt_xoa_Click(object sender, EventArgs e)
        {
            if (txt_maphieunhap.Text != "")
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE PhieuNhap WHERE MaPhieuNhap=@maphieunhap", con);
                    cmd.Parameters.AddWithValue("@maphieunhap", txt_maphieunhap.Text);
                    int rowsaffected = cmd.ExecuteNonQuery();
                    if (rowsaffected == 1)
                    {
                        MessageBox.Show("Xóa phiếu nhập thành công");
                    }
                    dis_nhaphang();
                    cl_nhaphang();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("Vui chọn mã phiếu nhập cần xóa!");
            }
        }

        private void txt_timkiem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(connect);
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM PhieuNhap WHERE MaPhieuNhap LIKE N'%" + txt_timkiem.Text + "%'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gv_nhaphang.DataSource = dt;
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

        private void btn_khachhang_Click(object sender, EventArgs e)
        {
            fr_khachhang khachhang = new fr_khachhang();
            this.Hide();
            khachhang.ShowDialog();
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
    }
}
