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
using Microsoft.Reporting.WinForms;

namespace Do_An_PhanTienHuy_NguyenHuuToan
{
    public partial class fr_xuathoadon : Form
    {
        public fr_xuathoadon()
        {
            InitializeComponent();
        }

        private void bt_xuat_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_tenkhachhang.Text != "")
                {
                    string connect = "server=" + @"DESKTOP-1VK71I1\SQLEXPRESS" + ";database=" + "DoAn.Net" + ";integrated security=true";
                    SqlConnection con = new SqlConnection(connect);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT MaKhachHang,TenKhachHang,TongTien,NgayLap From HoaDon WHERE TenKhachHang=N'"+txt_tenkhachhang+"'", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "HoaDon");
                    if (ds.Tables.Count > 0)
                    {
                        this.reportViewer1.LocalReport.ReportEmbeddedResource = "Do_An_PhanTienHuy_NguyenHuuToan.XuatHoaDon2.rdlc";
                        ReportDataSource rds = new ReportDataSource();
                        rds.Name = "HoaDon";
                        rds.Value = ds.Tables["HoaDon"];
                        this.reportViewer1.LocalReport.DataSources.Add(rds);
                       
                    }
                    else
                    {
                        MessageBox.Show("Không có hóa đơn cần xuất");
                    }
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập tên khách hàng");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            this.reportViewer1.RefreshReport();
        }
    }
}
