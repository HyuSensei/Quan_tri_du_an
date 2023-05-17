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
using System.IO;

namespace Do_An_PhanTienHuy_NguyenHuuToan
{
    public partial class fr_sanpham : Form
    {

        string connect = "server=" + @"DESKTOP-1VK71I1\SQLEXPRESS" + ";database=" + "DoAn.Net" + ";integrated security=true";
        SqlConnection con = new SqlConnection();
        public fr_sanpham()
        {
            InitializeComponent();
        }
        byte[] ConvertImageToBytes(Image img)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }
        public Image ConverByteArrayToImage(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                return Image.FromStream(ms);
            }

        }
        public void get_loaisp()
        {
            try
            {
                con = new SqlConnection(connect);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM LoaiSanPham", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                DataRow row = dt.NewRow();
                row[0] = 0;
                row[1] = "Chọn loại sản phẩm";
                dt.Rows.InsertAt(row, 0);
                cb_loai.DataSource = dt;
                cb_loai.DisplayMember = "Loai";
                cb_loai.ValueMember = "id";
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void dis_sanpham() {
            try
            {
                con = new SqlConnection(connect);
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM SanPham", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gv_sanpham.DataSource = dt;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void cl_sanpham()
        {
            txt_tensanpham.Text = "";
            txt_gia.Text = "";
            txt_soluong.Text = "";
            cb_loai.SelectedIndex = -1;
            txt_mota.Text = "";
            txt_timkiem.Text = "";
            pic_sanpham.Image = null;
            pic_sanpham.Invalidate();
            txt_ma.Text = "";
            pic_sanpham_con.Visible = true;
        }
        private void bt_them_Click(object sender, EventArgs e)
        {
            if (txt_tensanpham.Text != "" && txt_gia.Text != "" && txt_soluong.Text != "" && cb_loai.Text != "" && txt_mota.Text != ""&&pic_sanpham.Image!=null)
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO SanPham(TenSanPham,Gia,SoLuong,Anh,Loai,MoTa) VALUES(@tensanpham,@gia,@soluong,@anh,@loai,@mota)", con);
                    cmd.Parameters.AddWithValue("@tensanpham", txt_tensanpham.Text);
                    cmd.Parameters.AddWithValue("@gia", txt_gia.Text);
                    cmd.Parameters.AddWithValue("@soluong", txt_soluong.Text);
                    if (pic_sanpham != null)
                    {
                        MemoryStream ms = new MemoryStream();
                        pic_sanpham.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        byte[] anh = new byte[ms.Length];
                        ms.Position = 0;
                        ms.Read(anh, 0, anh.Length);
                        cmd.Parameters.AddWithValue("@anh", anh);
                    }
                    cmd.Parameters.AddWithValue("@loai", cb_loai.Text);
                    cmd.Parameters.AddWithValue("@mota", txt_mota.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm sản phẩm thành công");
                    dis_sanpham();
                    cl_sanpham();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin sản phẩm!");
            }
        }
        private void bt_chonanh_Click(object sender, EventArgs e)
        {
            pic_sanpham_con.Visible = false;
            //using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "jpeg|*.jpg|bmp|*.bmp|all files|*.*" })
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Pictures files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png)|*.jpg; *.jpeg; *.jpe; *.jfif; *.png|All files (*.*)|*.*" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                     pic_sanpham.Image = Image.FromFile(ofd.FileName);
                }
            }  
        }

        private void fr_sanpham_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the '_DoAn_NetDataSet.SanPham' table. You can move, or remove it, as needed.
            this.sanPhamTableAdapter.Fill(this._DoAn_NetDataSet.SanPham);
            get_loaisp();
            dis_sanpham();
        }

        private void gv_sanpham_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            pic_sanpham_con.Visible = false;
            int rows_id = e.RowIndex;
            txt_ma.Text = gv_sanpham.Rows[rows_id].Cells[0].Value.ToString().Trim();
            txt_tensanpham.Text = gv_sanpham.Rows[rows_id].Cells[1].Value.ToString().Trim();
            txt_gia.Text = gv_sanpham.Rows[rows_id].Cells[2].Value.ToString().Trim();
            txt_soluong.Text = gv_sanpham.Rows[rows_id].Cells[3].Value.ToString().Trim();
            cb_loai.Text = gv_sanpham.Rows[rows_id].Cells[5].Value.ToString().Trim();
            txt_mota.Text= gv_sanpham.Rows[rows_id].Cells[6].Value.ToString().Trim();
            MemoryStream ms = new MemoryStream((byte[])gv_sanpham.CurrentRow.Cells[4].Value);
            pic_sanpham.Image = Image.FromStream(ms);
        }

        private void txt_timkiem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(connect);
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM SanPham WHERE TenSanPham LIKE N'%" + txt_timkiem.Text + "%'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gv_sanpham.DataSource = dt;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void bt_lammoi_Click(object sender, EventArgs e)
        {
            cl_sanpham();
        }

        private void bt_sua_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE SanPham SET TenSanPham=@tensanpham,Gia=@gia,SoLuong=@soluong,MoTa=@mota,Loai=@loai,Anh=@anh WHERE id=@masanpham", con);
                cmd.Parameters.AddWithValue("@tensanpham", txt_tensanpham.Text);
                cmd.Parameters.AddWithValue("@gia", txt_gia.Text);
                cmd.Parameters.AddWithValue("@soluong", txt_soluong.Text);
                cmd.Parameters.AddWithValue("@mota", txt_mota.Text);
                cmd.Parameters.AddWithValue("@loai", cb_loai.Text);
                if (pic_sanpham != null)
                {
                    MemoryStream ms = new MemoryStream();
                    pic_sanpham.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    byte[] anh = new byte[ms.Length];
                    ms.Position = 0;
                    ms.Read(anh, 0, anh.Length);
                    cmd.Parameters.AddWithValue("@anh", anh);
                }
                cmd.Parameters.AddWithValue("@masanpham", txt_ma.Text);
                int rowsaffected = cmd.ExecuteNonQuery();
                if (rowsaffected == 1)
                {
                    MessageBox.Show("Sửa sản phẩm thành công");
                }
                dis_sanpham();
                cl_sanpham();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void bt_xoa_Click(object sender, EventArgs e)
        {
            if (txt_ma.Text!="")
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE SanPham WHERE id=@masanpham", con);
                    cmd.Parameters.AddWithValue("@masanpham", txt_ma.Text);
                    int rowsaffected = cmd.ExecuteNonQuery();
                    if (rowsaffected == 1)
                    {
                        MessageBox.Show("Xóa sản phẩm thành công");
                    }
                    dis_sanpham();
                    cl_sanpham();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("Vui chọn mã sản phẩm cần xóa!");
            }
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

        private void btn_trangchu_Click(object sender, EventArgs e)
        {
            fr_trangchu trangchu = new fr_trangchu();
            this.Hide();
            trangchu.ShowDialog();
        }

        private void btn_trangchu_Click_1(object sender, EventArgs e)
        {
            fr_trangchu trangchu = new fr_trangchu();
            this.Hide();
            trangchu.ShowDialog();
        }

        private void btn_nhaphang_Click(object sender, EventArgs e)
        {
            fr_nhaphang nhanhang = new fr_nhaphang();
            this.Hide();
            nhanhang.ShowDialog();
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

        private void guna2Button1_Click(object sender, EventArgs e)
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
