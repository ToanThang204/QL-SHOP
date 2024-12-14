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
    public partial class trangchu : Form
    {
        private Form currentForm = null;  // Biến lưu trữ form hiện tại

        public trangchu()
        {
            InitializeComponent();
        }

        private void trangchu_Load(object sender, EventArgs e)
        {

            // Đặt kích thước và vị trí của pictureBoxtrangchu
            pictureBoxtrangchu.Size = new Size(1164, 983);  // Kích thước của pictureBoxtrangchu
            pictureBoxtrangchu.Location = new Point(349, -1);  // Vị trí của pictureBoxtrangchu
        
            this.Width = 1500;
            this.Height = 800;
            // Lặp qua tất cả các button trong form và đăng ký sự kiện
            foreach (Control control in this.Controls)
            {
                if (control is Button)
                {
                    Button button = (Button)control;
                    button.MouseEnter += Button_MouseEnter;
                    button.MouseLeave += Button_MouseLeave;
                }
            }

        }
        private void SwitchForm(Form newForm)
        {
            if (currentForm != null)
            {
                currentForm.Hide();  // Ẩn form hiện tại
            }

            currentForm = newForm; // Cập nhật form hiện tại
            currentForm.TopLevel = false;  // Đặt form con không phải top-level
            currentForm.FormBorderStyle = FormBorderStyle.None; // Loại bỏ viền của form con
            currentForm.Size = pictureBoxtrangchu.Size; // Đặt kích thước form con bằng kích thước của pictureBoxtrangchu
            currentForm.Location = pictureBoxtrangchu.Location; // Đặt vị trí form con giống vị trí của pictureBoxtrangchu

            // Thêm form con vào form chính
            this.Controls.Add(currentForm);
            currentForm.Show(); // Hiển thị form con
        }
        private void Button_MouseEnter(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.BackColor = Color.Green; // Thay đổi màu nền khi hover
        }

        // Khi di chuột ra khỏi button
        private void Button_MouseLeave(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.BackColor = Color.Goldenrod; // Màu nền mặc định
        }
        private void pictureBoxtranchu_Click(object sender, EventArgs e)
        {

        }

        private void btntrangchu_Click(object sender, EventArgs e)
        {
            pictureBoxtrangchu.Visible = true; // Hiển thị lại pictureBoxtrangchu

            // Ẩn form hiện tại nếu có
            if (currentForm != null)
            {
                currentForm.Hide();
            }
        }

        private void btnquanlyhang_Click(object sender, EventArgs e)
        {
            pictureBoxtrangchu.Visible = false;  // Ẩn pictureBoxtrangchu

            frmquanlyhang frm = new frmquanlyhang();
            SwitchForm(frm); // Chuyển sang form Quản lý hàng

        }

        private void btnquanlynhanvien_Click(object sender, EventArgs e)
        {
            pictureBoxtrangchu.Visible = false;  // Ẩn pictureBoxtrangchu

            frmquanlynhanvien frm = new frmquanlynhanvien();
            SwitchForm(frm); // Chuyển sang form Quản lý nhân viên
        }

        private void btnnhacungcap_Click(object sender, EventArgs e)
        {
            pictureBoxtrangchu.Visible = false;  

            frmnhacungcap frm = new frmnhacungcap();
            SwitchForm(frm); // Chuyển sang form nhà cung cấp
        }

        private void btnnhaphang_Click(object sender, EventArgs e)
        {
            pictureBoxtrangchu.Visible = false;  

            frmnhaphang frm = new frmnhaphang();
            SwitchForm(frm); // Chuyển sang form nhập hàng
        }

        private void btntaodonhang_Click(object sender, EventArgs e)
        {
            pictureBoxtrangchu.Visible = false;  

            frmtaodonhang frm = new frmtaodonhang();
            SwitchForm(frm); // Chuyển sang form Tạo đơn hàng
        }

        private void btnkhohang_Click(object sender, EventArgs e)
        {
            pictureBoxtrangchu.Visible = false;  

            frmkhohang frm = new frmkhohang();
            SwitchForm(frm); // Chuyển sang form Kho hàng
        }

        private void btndangxuat_Click(object sender, EventArgs e)
        {
            // Hiển thị MessageBox xác nhận
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận đăng xuất", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Nếu người dùng chọn Yes, thực hiện đăng xuất
            if (result == DialogResult.Yes)
            {
                // Ẩn form hiện tại (trangchu)
                this.Hide();

                // Tạo và hiển thị form đăng nhập
                FrmDangNhap frmLogin = new FrmDangNhap();
                frmLogin.Show();
            }
            // Nếu người dùng chọn No, không làm gì và quay lại form hiện tại
            else
            {
                return;
            }
        }
        private void OpenNextForm()
        {


        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}