using System;
using System.IO;
using System.Windows.Forms;
using LibVLCSharp.Shared;
using LibVLCSharp.WinForms;

namespace SHOP
{
    public partial class FrmDangNhap : Form
    {
        private LibVLC _libVLC;                // Thư viện VLC
        private MediaPlayer _mediaPlayer;      // Trình phát VLC
        private LibVLCSharp.WinForms.VideoView videoView1; // VideoView động

        public FrmDangNhap()
        {
            InitializeComponent();

            // Khởi tạo LibVLC
            Core.Initialize();
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btndangnhap_Click(object sender, EventArgs e)
        {
            // Kiểm tra người dùng đã nhập thông tin chưa
            if (string.IsNullOrWhiteSpace(txttendangnhap.Text) || string.IsNullOrWhiteSpace(txtmatkhau.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string username = txttendangnhap.Text;
            string password = txtmatkhau.Text;

            if (username == "admin" && password == "123") // Kiểm tra thông tin đăng nhập
            {
                MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                PlayVideo(); // Phát video khi đăng nhập thành công
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PlayVideo()
        {
            // Tạo VideoView động
            videoView1 = new LibVLCSharp.WinForms.VideoView
            {
                Location = new System.Drawing.Point(0, 0),  // Vị trí góc trái
                Size = new System.Drawing.Size(860, 460),  // Kích thước form
                Dock = DockStyle.Fill,                     // Phủ toàn bộ form
                BackColor = System.Drawing.Color.Black     // Nền đen
            };

            // Thêm VideoView vào form và đưa lên trên cùng
            this.Controls.Add(videoView1);
            videoView1.BringToFront();

            // Khởi tạo LibVLC và MediaPlayer
            _libVLC = new LibVLC();
            _mediaPlayer = new MediaPlayer(_libVLC);
            videoView1.MediaPlayer = _mediaPlayer;

            // Đường dẫn video
            var videoPath = Path.GetFullPath(@"D:\Github_demo\demo2\SHOP\Resources\LOGO.mp4");

            if (!File.Exists(videoPath))
            {
                MessageBox.Show("Không tìm thấy file video!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Phát video
            _mediaPlayer.Play(new Media(_libVLC, new Uri(videoPath)));

            // Khi video kết thúc, chuyển sang form mới
            _mediaPlayer.EndReached += (sender, e) =>
            {
                Invoke(new Action(() =>
                {
                    OpenNextForm();
                }));
            };
        }

        private void OpenNextForm()
        {
            // Mở form mới
            trangchu form2 = new trangchu();
            form2.Show();
            this.Hide();
        }

    }
}
