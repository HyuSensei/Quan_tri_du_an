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

namespace Do_An
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
                    string connect = "server=" + @"DESKTOP-K5LRVGB\SQLEXPRESS" + ";database=" + "DoAn.Net" + ";integrated security=true";
                    SqlConnection con = new SqlConnection(connect);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM HoaDon WHERE TenKhachHang =N'" + txt_tenkhachhang.Text + "'", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "HoaDon");
                    if (ds.Tables.Count > 0)
                    {
                        this.reportViewer1.LocalReport.ReportEmbeddedResource = "Do_An.XuatHoaDon.rdlc";
                        ReportDataSource rds = new ReportDataSource();
                        rds.Name = "DataSet1";
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
