- Tạo thêm các Manager cho từng module (manager sử dụng Singleton Pattern):	 
    - GamePlayManager quản lý Player BulletContainer
    - UIManager quản lý toàn bộ UI trong game:
        - Có 1 script UIBase.cs để các UI thừa kế, trong này định nghĩa sẵn hàm Hide() Show() để thực hiện việc hiện lên/ẩn đi. Tách hàm riêng thay vì chỉ gọi gameobject.SetActive() là vì sau cần thêm logic cho việc bật/tắt UI - ví dụ như làm animation"
    - AudioManager quản lý bật/tắt âm thanh
    - DataManager quản lý dữ liệu cả immutable data (dữ liệu của game) và mutable data (dlieu của player)
    - VFXManager quản lý các hiệu ứng của game (Particle System, GameObject tạo effect)
	
- Mỗi module có 1 script const riêng:
    - GamePlayConst: chứa thông tin về maxLife = 3
- Đặt namespace cho từng Module:
    - ContraClone.UI, ContraClone.GamePlay

- Các module khác nhau thì không kéo reference tới thành phần của nhau:
    - Nếu người chơi chết và module gameplay muốn gọi tới UI Game Over thì phải gọi lệnh: GUIManager.Instance.UIGameOver.Show();"	
- Làm game trong 1 Scene	
- Làm pooling object đơn giản cho bullet 	

- Cố gắng tráng các magic value:
    - Không so sánh layer/tag bằng string (gameobject.tag == "Enemy"). Hãy dùng 1 script TagConst và định nghĩa public const string ENEMY = "Enemy";