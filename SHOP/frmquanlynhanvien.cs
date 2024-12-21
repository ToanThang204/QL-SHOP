using SHOP.Database;
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
    public partial class frmquanlynhanvien : Form
    {
        private Model1 db; // Đổi kiểu db thành DbContext (Model1)

        public frmquanlynhanvien()
        {
            InitializeComponent();
            db = new Model1(); // Khởi tạo DbContext
        }

        // Hiển thị dữ liệu lên DataGridView
        private void BindGrid(List<NhanVien> lstNhanVien)
        {
            dvgNhanVien.Rows.Clear(); // Xóa dữ liệu cũ
            foreach (var item in lstNhanVien)
            {
                int index = dvgNhanVien.Rows.Add();
                dvgNhanVien.Rows[index].Cells[0].Value = item.MaNV;
                dvgNhanVien.Rows[index].Cells[1].Value = item.TenNV;
                dvgNhanVien.Rows[index].Cells[2].Value = item.SoDienThoai;
                dvgNhanVien.Rows[index].Cells[3].Value = item.QueQuan;
                dvgNhanVien.Rows[index].Cells[4].Value = item.Email;
            }
        }

        // Sự kiện Load Form
        private void frmquanlynhanvien_Load(object sender, EventArgs e)
        {
            try
            {
                // Lấy danh sách nhân viên
                var NhanVienList = db.NhanViens.ToList();
                if (NhanVienList.Count > 0)
                {
                    BindGrid(NhanVienList); // Hiển thị dữ liệu lên DataGridView
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu nhân viên trong hệ thống!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            string maNV = txtMaNV.Text;
            if (db.NhanViens.Any(nv => nv.MaNV == maNV))
            {
                MessageBox.Show("Mã nhân viên đã tồn tại!");
                return;
            }

            // Chuyển đổi SoDienThoai từ string sang int
            if (!int.TryParse(txtSoDienThoai.Text, out int soDienThoai))
            {
                MessageBox.Show("Số điện thoại phải là số nguyên!");
                return;
            }

            try
            {
                NhanVien newNhanVien = new NhanVien()
                {
                    MaNV = maNV,
                    TenNV = txtHoTen.Text,
                    SoDienThoai = soDienThoai, // Giá trị đã chuyển đổi
                    QueQuan = txtQueQuan.Text,
                    Email = txtEmail.Text
                };

                db.NhanViens.Add(newNhanVien);
                db.SaveChanges();

                MessageBox.Show("Thêm nhân viên thành công!");
                BindGrid(db.NhanViens.ToList());
                ResetInputFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm nhân viên: " + ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            string maNV = txtMaNV.Text;
            var editNhanVien = db.NhanViens.FirstOrDefault(nv => nv.MaNV == maNV);
            if (editNhanVien == null)
            {
                MessageBox.Show("Không tìm thấy nhân viên cần sửa!");
                return;
            }

            // Chuyển đổi SoDienThoai từ string sang int
            if (!int.TryParse(txtSoDienThoai.Text, out int soDienThoai))
            {
                MessageBox.Show("Số điện thoại phải là số nguyên!");
                return;
            }

            try
            {
                editNhanVien.TenNV = txtHoTen.Text;
                editNhanVien.SoDienThoai = soDienThoai; // Giá trị đã chuyển đổi
                editNhanVien.QueQuan = txtQueQuan.Text;
                editNhanVien.Email = txtEmail.Text;

                db.SaveChanges();
                MessageBox.Show("Cập nhật thông tin nhân viên thành công!");
                BindGrid(db.NhanViens.ToList());
                ResetInputFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa nhân viên: " + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maNV = txtMaNV.Text;
            if (string.IsNullOrEmpty(maNV))
            {
                MessageBox.Show("Vui lòng nhập mã nhân viên để xóa!");
                return;
            }

            var delNhanVien = db.NhanViens.FirstOrDefault(nv => nv.MaNV == maNV);
            if (delNhanVien == null)
            {
                MessageBox.Show("Không tìm thấy nhân viên cần xóa!");
                return;
            }

            try
            {
                var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này?", "Xác nhận xóa", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    db.NhanViens.Remove(delNhanVien);
                    db.SaveChanges();
                    MessageBox.Show("Xóa nhân viên thành công!");
                    BindGrid(db.NhanViens.ToList());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa nhân viên: " + ex.Message);
            }
        }

        private void dvgNhanVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dvgNhanVien.Rows[e.RowIndex];
                txtMaNV.Text = row.Cells[0].Value?.ToString();
                txtHoTen.Text = row.Cells[1].Value?.ToString();
                txtSoDienThoai.Text = row.Cells[2].Value?.ToString();
                txtQueQuan.Text = row.Cells[3].Value?.ToString();
                txtEmail.Text = row.Cells[4].Value?.ToString();
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrEmpty(txtMaNV.Text) || txtMaNV.Text.Length < 3)
            {
                MessageBox.Show("Mã nhân viên phải có ít nhất 3 ký tự!");
                return false;
            }

            if (string.IsNullOrEmpty(txtHoTen.Text) || txtHoTen.Text.Length < 3)
            {
                MessageBox.Show("Họ tên phải có ít nhất 3 ký tự!");
                return false;
            }

            if (string.IsNullOrEmpty(txtSoDienThoai.Text) || !txtSoDienThoai.Text.All(char.IsDigit) || txtSoDienThoai.Text.Length != 10)
            {
                MessageBox.Show("Số điện thoại phải là số và có 10 chữ số!");
                return false;
            }

            if (string.IsNullOrEmpty(txtEmail.Text) || !txtEmail.Text.Contains("@"))
            {
                MessageBox.Show("Email không hợp lệ!");
                return false;
            }

            return true;
        }
        
        /// Reset các trường nhập liệu
       
        private void ResetInputFields()
        {
            txtMaNV.Clear();
            txtHoTen.Clear();
            txtSoDienThoai.Clear();
            txtQueQuan.Clear();
            txtEmail.Clear();
        }

    }
}
