using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SHOP
{
    public partial class frmquanlyhang : Form
    {
        public frmquanlyhang()
        {
            InitializeComponent();


        }
        private int GetSelectedRow(string studentID)
        {
            // Duyệt qua từng dòng trong DataGridView
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                // Kiểm tra ô đầu tiên (Cells[0]) có giá trị hợp lệ
                if (dgv.Rows[i].Cells[0].Value != null &&
                    !string.IsNullOrEmpty(dgv.Rows[i].Cells[0].Value.ToString()))
                {
                    // So sánh giá trị ô với studentID
                    if (dgv.Rows[i].Cells[0].Value.ToString() == studentID)
                    {
                        return i; // Trả về chỉ số dòng nếu tìm thấy
                    }
                }
            }

            return -1; // Trả về -1 nếu không tìm thấy
        }
        private void InsertUpdate(int selectedRow)
        {
            // Gán giá trị cho từng ô (cell) của dòng được chỉ định trong DataGridView
            dgv.Rows[selectedRow].Cells[0].Value = txtMaSP.Text;      // Mã SP
            dgv.Rows[selectedRow].Cells[1].Value = txtNCC.Text;       // MaNCC
            dgv.Rows[selectedRow].Cells[2].Value = txtHangSP.Text;    //Hãng Sản Phẩm
            dgv.Rows[selectedRow].Cells[3].Value = txtTenSP.Text;     // Tên Sản Phẩm                    
            dgv.Rows[selectedRow].Cells[4].Value = txtTheLoai.Text;   // Thể Loại
            dgv.Rows[selectedRow].Cells[5].Value = txtXuatXu.Text;    //Xuất xứ
            dgv.Rows[selectedRow].Cells[6].Value = txtGiaBan.Text;    // Giá Bán
        }


        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra dữ liệu nhập vào
                if (txtMaSP.Text == "" || txtNCC.Text == "" || txtHangSP.Text == "" ||
                    txtTenSP.Text == "" || txtTheLoai.Text == "" || txtXuatXu.Text == "" || txtGiaBan.Text == "")
                {
                    throw new Exception("Vui lòng nhập đầy đủ thông tin sản phẩm!");
                }

                // Tìm dòng có mã sản phẩm tương ứng
                int selectedRow = GetSelectedRow(txtMaSP.Text);
                if (selectedRow == -1) // TH Thêm mới
                {
                    selectedRow = dgv.Rows.Add();
                    InsertUpdate(selectedRow);
                    MessageBox.Show("Thêm mới dữ liệu sản phẩm thành công!", "Thông Báo", MessageBoxButtons.OK);
                }
                else // TH cập nhật
                {
                    InsertUpdate(selectedRow);
                    MessageBox.Show("Cập nhật dữ liệu sản phẩm thành công!", "Thông Báo", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi nếu có ngoại lệ
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            try
            {
                // Tìm dòng có mã sản phẩm trùng khớp
                int selectedRow = GetSelectedRow(txtMaSP.Text);

                if (selectedRow == -1)
                {
                    // Nếu không tìm thấy mã sản phẩm
                    throw new Exception("Không tìm thấy Mã Sản Phẩm cần xóa!");
                }
                else
                {
                    // Hiển thị hộp thoại xác nhận xóa
                    DialogResult dr = MessageBox.Show("Bạn có muốn xóa sản phẩm này không?",
                                                      "Xác Nhận Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        // Xóa dòng sản phẩm tại vị trí tìm thấy
                        dgv.Rows.RemoveAt(selectedRow);

                        // Xóa sạch các ô nhập liệu sau khi xóa
                        txtMaSP.Text = "";
                        txtNCC.Text = "";
                        txtHangSP.Text = "";
                        txtTenSP.Text = "";
                        txtTheLoai.Text = "";
                        txtXuatXu.Text = "";
                        txtGiaBan.Text = "";


                        // Thông báo thành công
                        MessageBox.Show("Xóa sản phẩm thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi nếu có ngoại lệ
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnkhoitao_Click(object sender, EventArgs e)
        {
            try
            {
                //Xóa Dữ Liệu trong DGV
                dgv.Rows.Clear();
                // Xóa sạch các ô nhập liệu
                txtMaSP.Text = "";
                txtNCC.Text = "";
                txtHangSP.Text = "";
                txtTenSP.Text = "";
                txtTheLoai.Text = "";
                txtXuatXu.Text = "";
                txtGiaBan.Text = "";

                // Đặt focus vào ô đầu tiên
                txtMaSP.Focus();

                // Làm mới DataGridView (nếu cần thiết)
                dgv.Rows.Clear();

                // Thêm dữ liệu khởi tạo (nếu có)
                dgv.Rows.Add("SP001", "NCC001", "Adidas", "Áo Thun Nam", "Áo Thun", "USA", "300000");
                dgv.Rows.Add("SP002", "NCC002", "Adidas", "Quần Jeans Nữ", "Quần Jeans", "Germany", "450000");
                dgv.Rows.Add("SP003", "NCC003", "Puma", "Áo Khoác Hoodie", "Áo Khoác", "Italy", "600000");
                dgv.Rows.Add("SP004", "NCC004", "Zara", "Váy Liền Nữ ", "Váy", "Spain", "550000");
                dgv.Rows.Add("SP005", "NCC005", "H&M", "Áo Sơ Mi Nam ", "Áo Sơ Mi", "Sweden", "300000");
                dgv.Rows.Add("SP006", "NCC006", "Uniqlo", "Quần Short ", "Quần Short", "Japan", "250000");
                dgv.Rows.Add("SP007", "NCC007", "Levent", "Quần Jean Nam ", "Quần Jean", "USA", "700000");
                dgv.Rows.Add("SP008", "NCC008", "Pull&Bear", "Áo Thun Nữ ", "Áo Thun", "Portugal", "320000");
                dgv.Rows.Add("SP009", "NCC009", "Mango", "Đầm Xòe Nữ ", "Đầm", "Spain", "450000");
                dgv.Rows.Add("SP010", "NCC010", "Converse", "Giày Sneaker", "Giày", "China", "800000");

                // Thông báo thành công
                MessageBox.Show("Dữ liệu đã được khởi tạo!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu xảy ra
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    
    }
}
