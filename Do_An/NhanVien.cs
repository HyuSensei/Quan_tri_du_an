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
    public partial class fr_nhanvien : Form
    {
        string connect = "server=" + @"DESKTOP-1VK71I1\SQLEXPRESS" + ";database=" + "DoAn.Net" + ";integrated security=true";
        SqlConnection con = new SqlConnection();
        public fr_nhanvien()
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

        private void fr_nhanvien_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the '_DoAn_NetDataSet3.NhanVien' table. You can move, or remove it, as needed.
            this.nhanVienTableAdapter.Fill(this._DoAn_NetDataSet3.NhanVien);

        }
        public void dis_nhanvien()
        {
            try
            {
                con = new SqlConnection(connect);
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM NhanVien", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gv_nhanvien.DataSource = dt;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void cl_nhanvien()
        {
            txt_manhanvien.Text = "";
            txt_tennhanvien.Text = "";
            txt_sodienthoai.Text = "";
            txt_diachi.Text = "";
            txt_timkiem.Text = "";
            pic_nhanvien.Image = null;
            pic_nhanvien.Invalidate();
            pic_nhanvien_con.Visible = true;
        }
        private void bt_chonanh_Click(object sender, EventArgs e)
        {
            pic_nhanvien_con.Visible = false;
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Pictures files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png)|*.jpg; *.jpeg; *.jpe; *.jfif; *.png|All files (*.*)|*.*" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    pic_nhanvien.Image = Image.FromFile(ofd.FileName);
                }
            }
        }

        private void bt_them_Click(object sender, EventArgs e)
        {
            if (txt_manhanvien.Text != "" && txt_tennhanvien.Text != "" && txt_sodienthoai.Text != "" && txt_diachi.Text != "" && pic_nhanvien.Image != null)
            {
                try
                {
                    con = new SqlConnection(connect);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO NhanVien(MaNhanVien,TenNhanVien,SoDienThoai,DiaChi,Anh) VALUES(@manhanvien,@tennhanvien,@sodienthoai,@diachi,@anh)", con);
                    cmd.Parameters.AddWithValue("@manhanvien", txt_manhanvien.Text);
                    cmd.Parameters.AddWithValue("@tennhanvien", txt_tennhanvien.Text);
                    cmd.Parameters.AddWithValue("@sodienthoai", txt_sodienthoai.Text);
                    cmd.Parameters.AddWithValue("@diachi", txt_diachi.Text);
                    if (pic_nhanvien != null)
                    {
                        MemoryStream ms = new MemoryStream();
                        pic_nhanvien.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        byte[] anh = new byte[ms.Length];
                        ms.Position = 0;
                        ms.Read(anh, 0, anh.Length);
                        cmd.Parameters.AddWithValue("@anh", anh);
                    }
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm nhân viên thành công");
                    dis_nhanvien();
                    cl_nhanvien();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin nhân viên!");
            }
        }

        private void bt_sua_Click(object sender, EventArgs e)
        {
            if (txt_manhanvien.Text != "" && txt_tennhanvien.Text != "" && txt_sodienthoai.Text != "" && txt_diachi.Text != "" && pic_nhanvien.Image != null)
            {
                try
                {
                    con = new SqlConnection(connect);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE NhanVien SET TenNhanVien=@tennhanvien,SoDienThoai=@sodienthoai,Diachi=@diachi,Anh=@anh WHERE MaNhanVien=@manhanvien", con);
                    cmd.Parameters.AddWithValue("@tennhanvien", txt_tennhanvien.Text);
                    cmd.Parameters.AddWithValue("@sodienthoai", txt_sodienthoai.Text);
                    cmd.Parameters.AddWithValue("@diachi", txt_diachi.Text);
                    if (pic_nhanvien != null)
                    {
                        MemoryStream ms = new MemoryStream();
                        pic_nhanvien.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        byte[] anh = new byte[ms.Length];
                        ms.Position = 0;
                        ms.Read(anh, 0, anh.Length);
                        cmd.Parameters.AddWithValue("@anh", anh);
                    }
                    cmd.Parameters.AddWithValue("@manhanvien", txt_manhanvien.Text);
                    int rowsaffected = cmd.ExecuteNonQuery();
                    if (rowsaffected == 1)
                    {
                        MessageBox.Show("Sửa thông tin nhân viên thành công");
                    }
                    dis_nhanvien();
                    cl_nhanvien();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin nhân viên!");
            }
        }

        private void gv_nhanvien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            pic_nhanvien_con.Visible = false;
            int rows_id = e.RowIndex;
            txt_manhanvien.Text = gv_nhanvien.Rows[rows_id].Cells[0].Value.ToString().Trim();
            txt_tennhanvien.Text = gv_nhanvien.Rows[rows_id].Cells[1].Value.ToString().Trim();
            txt_sodienthoai.Text = gv_nhanvien.Rows[rows_id].Cells[2].Value.ToString().Trim();
            txt_diachi.Text = gv_nhanvien.Rows[rows_id].Cells[3].Value.ToString().Trim();
            MemoryStream ms = new MemoryStream((byte[])gv_nhanvien.CurrentRow.Cells[4].Value);
            pic_nhanvien.Image = Image.FromStream(ms);
        }

        private void bt_xoa_Click(object sender, EventArgs e)
        {
            if (txt_manhanvien.Text != "")
            {
                try
                {
                    con = new SqlConnection(connect);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE NhanVien WHERE MaNhanVien=@manhanvien", con);
                    cmd.Parameters.AddWithValue("@manhanvien", txt_manhanvien.Text);
                    int rowsaffected = cmd.ExecuteNonQuery();
                    if (rowsaffected == 1)
                    {
                        MessageBox.Show("Xóa nhân viên công");
                    }
                    dis_nhanvien();
                    cl_nhanvien();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("Vui lòng điền mã nhân viên cần xóa!");
            }
        }

        private void txt_timkiem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(connect);
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM NhanVien WHERE TenNhanVien LIKE N'%" + txt_timkiem.Text + "%'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gv_nhanvien.DataSource = dt;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void bt_lammoi_Click(object sender, EventArgs e)
        {
            cl_nhanvien();
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

        private void btn_khachhang_Click(object sender, EventArgs e)
        {
            fr_khachhang khachhang = new fr_khachhang();
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
