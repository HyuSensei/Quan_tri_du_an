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


namespace Do_An
{
    public partial class fr_trangchu : Form
    {
        string connect = "server=" + @"DESKTOP-K5LRVGB\SQLEXPRESS" + ";database=" + "DoAn.Net" + ";integrated security=true";
        SqlConnection con = new SqlConnection();
        public fr_trangchu()
        {
            InitializeComponent();
        }
        void fill_thongke()
        {
            try
            {
                con = new SqlConnection(connect);
                DataTable dt = new DataTable();
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("Select Thang,DoanhThu From ThongKe", con);
                da.Fill(dt);
                thong_ke.DataSource = dt;
                con.Close();
                thong_ke.Series["DoanhThu"].XValueMember = "Thang";
                thong_ke.Series["DoanhThu"].YValueMembers = "DoanhThu";

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
         byte[] ConvertImageToBytes(Image img)
        {
            using(MemoryStream ms=new MemoryStream())
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
        private void fr_trangchu_Load(object sender, EventArgs e)
        {
            fill_thongke();
            lb_hovaten.Text = NguoiDung.HoVaTen;
            lb_email.Text = NguoiDung.Email;
            if (NguoiDung.Role == "Admin")
            {
                lb_role.Text = "Admin";
            }
            else
            {
                lb_role.Text = "Nhân viên";
                btn_nhanvien.Visible = false;
            }
            try
            {

                con = new SqlConnection(connect);
                con.Open();
                SqlCommand cmd = new SqlCommand("Select Avatar From NguoiDung Where id=@id", con);
                cmd.Parameters.AddWithValue("@id", NguoiDung.id);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    byte[] avatar = (byte[])(dt.Rows[0]["Avatar"]);
                    if(avatar == null)
                    {
                        pic_logo.Image = null;
                    }
                    else {
                        MemoryStream ms=new MemoryStream(avatar);
                        pic_avatar.Image = Image.FromStream(ms);
                    }
                }
                con.Close();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btn_dangxuat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Đăng xuất ngay!","Thông báo",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                fr_dangnhap dangnhap = new fr_dangnhap();
                this.Hide();
                dangnhap.ShowDialog();
            }
        }

        private void btn_sanpham_Click(object sender, EventArgs e)
        {
            fr_sanpham sanpham = new fr_sanpham();
            this.Hide();
            sanpham.ShowDialog();
        }

        private void bnt_taikhoan_Click(object sender, EventArgs e)
        {
            fr_taikhoan taikhoan = new fr_taikhoan();
            taikhoan.ShowDialog();
        }

        private void btn_sanpham_Click_1(object sender, EventArgs e)
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
            this.Hide();
            khachhang.ShowDialog();
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

        private void pic_logo_Click(object sender, EventArgs e)
        {

        }

        private void lb_email_Click(object sender, EventArgs e)
        {

        }

        private void lb_hovaten_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }
    }

}
