using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SHOP
{
    public partial class frmquanlyhang : Form
    {
        SqlConnection connection;
        SqlCommand command;
        string str = "Data Source=DESKTOP-QC6GJ5C\\SQLEXPRESS;Initial Catalog=QLShop;Integrated Security=True";
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        public frmquanlyhang()
        {
            InitializeComponent();

        }
        void loaddata(string searchKeyword = "")
        {
            try
            {
                using (connection = new SqlConnection(str))
                {
                    connection.Open(); // Open the connection
                    command = connection.CreateCommand();

                    if (!string.IsNullOrEmpty(searchKeyword))
                    {
                        command.CommandText = "SELECT * FROM ThongTinSanPham WHERE TenSanPham LIKE @searchKeyword";
                        command.Parameters.AddWithValue("@searchKeyword", "%" + searchKeyword + "%");
                    }
                    else
                    {
                        command.CommandText = "SELECT * FROM ThongTinSanPham";
                    }

                    adapter.SelectCommand = command;
                    table.Clear();
                    adapter.Fill(table);
                    dgvQLH.DataSource = table;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                using (connection = new SqlConnection(str)) // Tạo kết nối mới
                {
                    connection.Open(); // Mở kết nối
                    using (command = connection.CreateCommand()) // Tạo lệnh
                    {
                        command.CommandText = "INSERT INTO ThongTinSanPham (MaSP, MaNCC, HangSP, TenSanPham, TheLoai, XuatXu, GiaBan) " +
                                              "VALUES (@MaSP, @MaNCC, @HangSP, @TenSP, @TheLoai, @XuatXu, @GiaBan)";

                        // Thêm các tham số an toàn
                        command.Parameters.AddWithValue("@MaSP", txtMaSP.Text);
                        command.Parameters.AddWithValue("@MaNCC", txtMaNCC.Text);
                        command.Parameters.AddWithValue("@HangSP", txtHangSP.Text);
                        command.Parameters.AddWithValue("@TenSP", txtTenSP.Text);
                        command.Parameters.AddWithValue("@TheLoai", txtTheLoai.Text);
                        command.Parameters.AddWithValue("@XuatXu", txtXuatXu.Text);
                        command.Parameters.AddWithValue("@GiaBan", txtGiaBan.Text);

                        command.ExecuteNonQuery(); // Thực thi lệnh
                        MessageBox.Show("Thêm sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    loaddata(); // Load lại dữ liệu
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            try
            {
                using (connection = new SqlConnection(str))
                {
                    connection.Open();
                    command = connection.CreateCommand();
                    command.CommandText = "DELETE FROM ThongTinSanPham WHERE MaSP = @MaSP";
                    command.Parameters.AddWithValue("@MaSP", txtMaSP.Text);

                    command.ExecuteNonQuery();
                    MessageBox.Show("Xóa sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loaddata();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnkhoitao_Click(object sender, EventArgs e)
        {
            txtMaSP.Text = "";
            txtMaNCC.Text = "";
            txtHangSP.Text = "";
            txtTenSP.Text = "";
            txtTheLoai.Text = "";
            txtXuatXu.Text = "";
            txtGiaBan.Text = "";
     
        }

        private void btnSua_Click(object sender, EventArgs e)
        {

            try
            {
                using (connection = new SqlConnection(str))
                {
                    connection.Open();
                    command = connection.CreateCommand();
                    command.CommandText = "UPDATE ThongTinSanPham SET MaNCC = @MaNCC, HangSP = @HangSP, TenSanPham = @TenSanPham, TheLoai = @TheLoai, XuatXu = @XuatXu, GiaBan = @GiaBan WHERE MaSP = @MaSP";

                    command.Parameters.AddWithValue("@MaSP", txtMaSP.Text);
                    command.Parameters.AddWithValue("@MaNCC", txtMaNCC.Text);
                    command.Parameters.AddWithValue("@HangSP", txtHangSP.Text);
                    command.Parameters.AddWithValue("@TenSanPham", txtTenSP.Text);
                    command.Parameters.AddWithValue("@TheLoai", txtTheLoai.Text);
                    command.Parameters.AddWithValue("@XuatXu", txtXuatXu.Text);
                    command.Parameters.AddWithValue("@GiaBan", txtGiaBan.Text);

                    command.ExecuteNonQuery();
                    MessageBox.Show("Cập nhật sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loaddata();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void dgvQLH_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvQLH.Rows[e.RowIndex];
                txtMaSP.Text = row.Cells["MaSP"].Value.ToString();
                txtMaNCC.Text = row.Cells["MaNCC"].Value.ToString();
                txtHangSP.Text = row.Cells["HangSP"].Value.ToString();
                txtTenSP.Text = row.Cells["TenSanPham"].Value.ToString();
                txtTheLoai.Text = row.Cells["TheLoai"].Value.ToString();
                txtXuatXu.Text = row.Cells["XuatXu"].Value.ToString();
                txtGiaBan.Text = row.Cells["GiaBan"].Value.ToString();
            }
         
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchKeyword = txtSearch.Text;
            loaddata(searchKeyword);
        }

        private void frmquanlyhang_Load(object sender, EventArgs e)
        {
            loaddata();
        }
    }
}
