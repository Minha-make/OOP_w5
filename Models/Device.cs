/****************/  

// Mã sinh viên: 202418890

// Họ tên: Nguyễn Minh Hà  

/****************/ 

namespace DeviceManagement.Models;

/// <summary>
/// Lớp cơ sở trừu tượng đại diện cho mọi thiết bị trong hệ thống.
/// </summary>
public abstract class Device
{
    private string _name = string.Empty;
    private int _yearInUse;
    private decimal _purchasePrice;

    /// <summary>
    /// Mã thiết bị (chỉ đọc, chỉ được gán duy nhất trong constructor).
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// Tên thiết bị (không được để trống).
    /// </summary>
    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Tên thiết bị không được để trống hoặc chỉ chứa khoảng trắng.", nameof(value));
            _name = value;
        }
    }

    /// <summary>
    /// Năm đưa vào sử dụng (không được lớn hơn năm hiện tại).
    /// </summary>
    public int YearInUse
    {
        get => _yearInUse;
        set
        {
            if (value > DateTime.Now.Year)
                throw new ArgumentOutOfRangeException(nameof(value), "Năm đưa vào sử dụng không được lớn hơn năm hiện tại.");
            if (value < 1900)
                throw new ArgumentOutOfRangeException(nameof(value), "Năm đưa vào sử dụng không hợp lệ.");
            _yearInUse = value;
        }
    }

    /// <summary>
    /// Giá mua ban đầu của thiết bị (phải lớn hơn 0).
    /// </summary>
    public decimal PurchasePrice
    {
        get => _purchasePrice;
        set
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Giá mua phải lớn hơn 0.");
            _purchasePrice = value;
        }
    }

    /// <summary>
    /// Trạng thái hoạt động hiện tại của thiết bị.
    /// </summary>
    public DeviceStatus Status { get; set; }

    /// <summary>
    /// Tuổi của thiết bị tính từ năm đưa vào sử dụng đến năm hiện tại.
    /// </summary>
    public int Age => Math.Max(0, DateTime.Now.Year - YearInUse);

    /// <summary>
    /// Khởi tạo một thiết bị mới.
    /// </summary>
    protected Device(string id, string name, int yearInUse, decimal purchasePrice, DeviceStatus status = DeviceStatus.Active)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentException("Mã thiết bị không được để trống hoặc chỉ chứa khoảng trắng.", nameof(id));

        Id = id;
        Name = name;
        YearInUse = yearInUse;
        PurchasePrice = purchasePrice;
        Status = status;
    }

    /// <summary>
    /// Tính toán chi phí bảo trì dự kiến trong một năm.
    /// </summary>
    /// <returns>Chi phí bảo trì dự kiến.</returns>
    public abstract decimal CalculateMaintenanceCost();

    /// <summary>
    /// Trả về chuỗi thông tin biểu diễn thiết bị.
    /// </summary>
    public override string ToString()
    {
        return $"[Mã: {Id}] {Name} | Năm SD: {YearInUse} (Tuổi: {Age} năm) | Giá mua: {PurchasePrice:N0} VNĐ | Trạng thái: {Status}";
    }
}
