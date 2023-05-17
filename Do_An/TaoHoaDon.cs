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
    public partial class fr_taohoadon : Form
    {
        string connect = "server=" + @"DESKTOP-1VK71I1\SQLEXPRESS" + ";database=" + "DoAn.Net" + ";integrated security=true";
        SqlConnection con = new SqlConnection();
        public fr_taohoadon()
        {
            InitializeComponent();
        }

        private void fr_taohoadon_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the '_DoAn_NetDataSet5.SanPhamDat' table. You can move, or remove it, as needed.
            this.sanPhamDatTableAdapter.Fill(this._DoAn_NetDataSet5.SanPhamDat);
            lb_id.Visible = false;
            dis_cthoadon();
            get_tensp();
            get_tenkhachhang();
            txt_makhachhang.Enabled = false;
            txt_masanpham.Enabled = false;
        }
        public void get_tensp()
        {
            try
            {
                con = new SqlConnection(connect);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM SanPham", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                DataRow row = dt.NewRow();
                row[0] = 0;
                row[1] = "Chọn tên sản phẩm";
                dt.Rows.InsertAt(row, 0);
                cb_tensanpham.DataSource = dt;
                cb_tensanpham.DisplayMember = "TenSanPham";
                cb_tensanpham.ValueMember = "id";
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void get_tenkhachhang()
        {
            try
            {
                con = new SqlConnection(connect);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM KhachHang", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                DataRow row = dt.NewRow();
                row[0] = 0;
                row[1] = "Chọn tên khách hàng";
                dt.Rows.InsertAt(row, 0);
                cb_tenkhachhang.DataSource = dt;
                cb_tenkhachhang.DisplayMember = "TenKhachHang";
                cb_tenkhachhang.ValueMember = "MaKhachHang";
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void dis_cthoadon()
        {
            try
            {
                con = new SqlConnection(connect);
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM SanPhamDat", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gv_cthoadon.DataSource = dt;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void cl_cthoadon()
        {
            txt_id.Text = "";
            txt_masanpham.Text = "";
            cb_tensanpham.SelectedIndex = -1;
            cb_tenkhachhang.SelectedIndex = 0;
            txt_makhachhang.Text = "";
            txt_gia.Text = "";
            txt_soluong.Text = "";
            txt_tongtien.Text = "";
            lb_id.Visible = false;
            cb_tenkhachhang.Enabled = true;
        }
        private void bt_lammoi_Click(object sender, EventArgs e)
        {
            cl_cthoadon();
        }

        private void gv_cthoadon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int rows_id = e.RowIndex;
            txt_id.Text = gv_cthoadon.Rows[rows_id].Cells[0].Value.ToString().Trim();
            txt_masanpham.Text = gv_cthoadon.Rows[rows_id].Cells[1].Value.ToString().Trim();
            txt_makhachhang.Text = gv_cthoadon.Rows[rows_id].Cells[2].Value.ToString().Trim();
            cb_tensanpham.Text = gv_cthoadon.Rows[rows_id].Cells[4].Value.ToString().Trim();
            cb_tenkhachhang.Text = gv_cthoadon.Rows[rows_id].Cells[3].Value.ToString().Trim();
            txt_soluong.Text = gv_cthoadon.Rows[rows_id].Cells[5].Value.ToString().Trim();
            txt_tongtien.Text = gv_cthoadon.Rows[rows_id].Cells[6].Value.ToString().Trim();
            cb_tenkhachhang.Enabled = false;
            lb_id.Visible = true;
        }

        private void bt_them_Click(object sender, EventArgs e)
        {
            if (txt_masanpham.Text != "" && cb_tensanpham.Text != "" && txt_gia.Text !="" && txt_tongtien.Text!=""&&cb_tenkhachhang.Text!="")
            {
                try
                {
                    con = new SqlConnection(connect);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO SanPhamDat(Id_SanPham,TenSanPham,SoLuong,TongTien,MaKhachHang,TenKhachHang) VALUES(@masanpham,@tensanpham,@soluong,@tongtien,@makhachhang,@tenkhachhang)", con);                   
                    cmd.Parameters.AddWithValue("@masanpham", txt_masanpham.Text);
                    cmd.Parameters.AddWithValue("@tensanpham", cb_tensanpham.Text);
                    cmd.Parameters.AddWithValue("@soluong", txt_soluong.Text);
                    cmd.Parameters.AddWithValue("@tongtien", txt_tongtien.Text);
                    cmd.Parameters.AddWithValue("@makhachhang", txt_makhachhang.Text);
                    cmd.Parameters.AddWithValue("@tenkhachhang", cb_tenkhachhang.Text);
                    int row = cmd.ExecuteNonQuery();
                    {
                        if (row >0)
                        {
                            MessageBox.Show("Thêm thành công");
                        }
                        else
                        {
                            MessageBox.Show("Thêm sản phẩm đặt thất bại");
                        }
                    }
                    dis_cthoadon();
                    cl_cthoadon();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!");
            }
        }

        private void cb_tenkhachhang_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(connect);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM KhachHang WHERE TenKhachHang=N'" + cb_tenkhachhang.Text + "'", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    txt_makhachhang.Text = dt.Rows[0]["MaKhachHang"].ToString();
                }
                if (cb_tenkhachhang.Text == "Chọn tên khách hàng")
                {
                    txt_makhachhang.Text = "";
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cb_tensanpham_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(connect);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM SanPham WHERE TenSanPham=N'" + cb_tensanpham.Text + "'", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    txt_masanpham.Text = dt.Rows[0]["id"].ToString();
                    txt_gia.Text= dt.Rows[0]["Gia"].ToString();
                    NguoiDung.gia=float.Parse(dt.Rows[0]["Gia"].ToString());
                }
                if (cb_tenkhachhang.Text == "Chọn tên sản phẩm")
                {
                    txt_masanpham.Text = "";
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txt_soluong_TextChanged(object sender, EventArgs e)
        {
            if (txt_soluong.Text!="")
            {
                txt_tongtien.Text = (Int32.Parse(txt_soluong.Text) * NguoiDung.gia).ToString();
            }
            else
            {
                txt_tongtien.Text = "";
            }
        }

        private void bt_sua_Click(object sender, EventArgs e)
        {
            if (txt_masanpham.Text != "" && cb_tensanpham.Text != "" && txt_gia.Text != "" && txt_tongtien.Text != "" && cb_tenkhachhang.Text != "")
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE SanPhamDat SET TenSanPham=@tensanpham,SoLuong=@soluong,TongTien=@tongtien, MaKhachHang=@makhachhang,TenKhachHang=@tenkhachhang,Id_SanPham=@masanpham WHERE id=@id", con);
                    cmd.Parameters.AddWithValue("@tensanpham", cb_tensanpham.Text);
                    cmd.Parameters.AddWithValue("@soluong", txt_soluong.Text);
                    cmd.Parameters.AddWithValue("@tongtien", txt_tongtien.Text);
                    cmd.Parameters.AddWithValue("@makhachhang", txt_makhachhang.Text);
                    cmd.Parameters.AddWithValue("@tenkhachhang", cb_tenkhachhang.Text);
                    cmd.Parameters.AddWithValue("@masanpham", txt_masanpham.Text);
                    cmd.Parameters.AddWithValue("@id", txt_id.Text);
                    int rowsaffected = cmd.ExecuteNonQuery();
                    if (rowsaffected == 1)
                    {
                        MessageBox.Show("Sửa thành công");
                    }
                    dis_cthoadon();
                    cl_cthoadon();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!");
            }
        }

        private void bt_xoa_Click(object sender, EventArgs e)
        {
            if (txt_id.Text != "")
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE SanPhamDat WHERE id=@id", con);
                    cmd.Parameters.AddWithValue("@id", txt_id.Text);
                    int rowsaffected = cmd.ExecuteNonQuery();
                    if (rowsaffected == 1)
                    {
                        MessageBox.Show("Xóa thành công");
                    }
                    dis_cthoadon();
                    cl_cthoadon();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("Vui chọn id cần xóa!");
            }
        }

        private void bt_toahoadon_Click(object sender, EventArgs e)
        {
            if (txt_makhachhang.Text != "" && cb_tenkhachhang.Text != "" &&txt_tongtien.Text != ""&&txt_ngaylap.Text!="")
            {
                try
                {
                    con = new SqlConnection(connect);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO HoaDon(MaKhachHang,TenKhachHang,TongTien,NgayLap) VALUES(@makhachhang,@tenkhachhang,@tongtien,@ngaylap)", con);
                    cmd.Parameters.AddWithValue("@makhachhang", txt_makhachhang.Text);                   
                    cmd.Parameters.AddWithValue("@tenkhachhang", cb_tenkhachhang.Text);
                    cmd.Parameters.AddWithValue("@tongtien", txt_tongtien.Text);
                    cmd.Parameters.AddWithValue("@ngaylap", txt_ngaylap.Text);
                    int row = cmd.ExecuteNonQuery();
                    {
                        if (row >0)
                        {                         
                                cmd = new SqlCommand("DELETE SanPhamDat WHERE id=@id", con);
                                cmd.Parameters.AddWithValue("@id", txt_id.Text);
                                int rowsaffected = cmd.ExecuteNonQuery();
                                if (rowsaffected == 1)
                                {
                                    MessageBox.Show("Tạo thành công");
                                }                                                   
                        }
                        else
                        {
                            MessageBox.Show("Tạo lỗi!");
                        }
                    }
                    txt_ngaylap.Text = "";
                    dis_cthoadon();
                    cl_cthoadon();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!");
            }
        }

        
    }
}
