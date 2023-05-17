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
    public partial class fr_dangky : Form
    {
        SqlCommand cmd;
        public fr_dangky()
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

        private void btn_them_anh_Click(object sender, EventArgs e)
        {
            pic_avatar_con.Visible = false;
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "jpeg|*.jpg|bmp|*.bmp|all files|*.*" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {

                    pic_avatar.Image = Image.FromFile(ofd.FileName);
                }
            }
        }
        public void clear_dangky()
        {
            pic_avatar.Image = null;
            pic_avatar.Invalidate();
            txt_tendangnhap.Text = "";
            txt_matkhau.Text = "";
            txt_hovaten.Text = "";
            txt_sodienthoai.Text = "";
            txt_email.Text = "";
            txt_diachi.Text = "";
        }
        public void conv_avatar()
        {
            if (pic_avatar != null)
            {
                MemoryStream ms = new MemoryStream();
                pic_avatar.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] avatar = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(avatar, 0, avatar.Length);
                cmd.Parameters.AddWithValue("@avatar", avatar);
            }
        }

        private void btn_dangky_Click(object sender, EventArgs e)
        {
            pic_avatar_con.Visible = true;
            if (txt_diachi.Text != "" && txt_tendangnhap.Text != "" && txt_matkhau.Text != "" && txt_sodienthoai.Text != "" && txt_email.Text != "" && txt_hovaten.Text != "")
            {
                string connect = "server=" + @"DESKTOP-1VK71I1\SQLEXPRESS" + ";database=" + "DoAn.Net" + ";integrated security=true";
                SqlConnection con = new SqlConnection(connect);
                try
                {
                    con.Open();
                    String role = "NhanVien";
                    cmd = new SqlCommand("Insert Into NguoiDung(TenDangNhap,MatKhau,HoVaTen,SoDienThoai,Email,DiaChi,Role,Avatar) Values(@tendangnhap,@matkhau,@hovaten,@sodienthoai,@email,@diachi,@role,@avatar)", con);
                    cmd.Parameters.AddWithValue("@tendangnhap", txt_tendangnhap.Text);
                    cmd.Parameters.AddWithValue("@matkhau", txt_matkhau.Text);
                    cmd.Parameters.AddWithValue("@hovaten", txt_hovaten.Text);
                    cmd.Parameters.AddWithValue("@sodienthoai", txt_sodienthoai.Text);
                    cmd.Parameters.AddWithValue("@email", txt_email.Text);
                    cmd.Parameters.AddWithValue("@diachi", txt_diachi.Text);
                    cmd.Parameters.AddWithValue("@role", role);
                    conv_avatar();
                    int row = cmd.ExecuteNonQuery();
                    if (row > 0)
                    {
                        MessageBox.Show("Đăng Ký Thành Công");
                    }
                    else
                    {
                        MessageBox.Show("Đăng ký không thành công vui lòng thử lại!");
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                clear_dangky();
            }
            else
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin đăng ký!");
            }
        }
    }
}
