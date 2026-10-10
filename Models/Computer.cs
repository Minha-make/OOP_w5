/****************/  

// Mã sinh viên: 202418890

// Họ tên: Nguyễn Minh Hà  

/****************/ 

namespace DeviceManagement.Models;

/// <summary>
/// Lớp đại diện cho máy tính trong phòng thực hành (chưa thực thi INetworkable ở bước này).
/// </summary>
public class Computer : Device
{
    private int _ramGb;
    private string _processor = string.Empty;

    /// <summary>
    /// Dung lượng RAM (GB), phải lớn hơn 0.
    /// </summary>
    public int RamGb
    {
        get => _ramGb;
        set
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Dung lượng RAM phải lớn hơn 0.");
            _ramGb = value;
        }
    }

    /// <summary>
    /// Loại bộ xử lý (CPU), không được để trống.
    /// </summary>
    public string Processor
    {
        get => _processor;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Loại bộ xử lý không được để trống hoặc chỉ chứa khoảng trắng.", nameof(value));
            _processor = value;
        }
    }

    /// <summary>
    /// Cho biết máy tính có card đồ họa rời (GPU) hay không.
    /// </summary>
    public bool HasDedicatedGpu { get; set; }

    /// <summary>
    /// Khởi tạo một đối tượng máy tính mới.
    /// </summary>
    public Computer(
        string id,
        string name,
        int yearInUse,
        decimal purchasePrice,
        int ramGb,
        string processor,
        bool hasDedicatedGpu,
        DeviceStatus status = DeviceStatus.Active)
        : base(id, name, yearInUse, purchasePrice, status)
    {
        RamGb = ramGb;
        Processor = processor;
        HasDedicatedGpu = hasDedicatedGpu;
    }

    /// <summary>
    /// Tính chi phí bảo trì hàng năm:
    /// 5% giá mua + 2% nếu có GPU rời + 1% nếu tuổi thiết bị > 5 năm.
    /// </summary>
    public override decimal CalculateMaintenanceCost()
    {
        decimal cost = PurchasePrice * 0.05m;
        if (HasDedicatedGpu)
        {
            cost += PurchasePrice * 0.02m;
        }
        if (Age > 5)
        {
            cost += PurchasePrice * 0.01m;
        }
        return cost;
    }

    public override string ToString()
    {
        return $"{base.ToString()} | CPU: {Processor} | RAM: {RamGb}GB | GPU rời: {(HasDedicatedGpu ? "Có" : "Không")}";
    }
}
