/****************/  

// Mã sinh viên: 202418890

// Họ tên: Nguyễn Minh Hà  

/****************/ 

namespace DeviceManagement.Models;

/// <summary>
/// Lớp đại diện cho máy in trong phòng thực hành.
/// </summary>
public class Printer : Device
{
    private int _pagesPrinted;

    /// <summary>
    /// Công nghệ in (Laser hoặc Inkjet).
    /// </summary>
    public PrinterType Type { get; set; }

    /// <summary>
    /// Tổng số trang đã in (không được âm).
    /// </summary>
    public int PagesPrinted
    {
        get => _pagesPrinted;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Số trang đã in không được âm.");
            _pagesPrinted = value;
        }
    }

    /// <summary>
    /// Cho biết máy in có in màu hay không.
    /// </summary>
    public bool IsColor { get; set; }

    /// <summary>
    /// Khởi tạo một đối tượng máy in mới.
    /// </summary>
    public Printer(
        string id,
        string name,
        int yearInUse,
        decimal purchasePrice,
        PrinterType type,
        int pagesPrinted,
        bool isColor,
        DeviceStatus status = DeviceStatus.Active)
        : base(id, name, yearInUse, purchasePrice, status)
    {
        Type = type;
        PagesPrinted = pagesPrinted;
        IsColor = isColor;
    }

    /// <summary>
    /// Tính chi phí bảo trì hàng năm:
    /// 4% giá mua + 500.000 nếu in > 100.000 trang + 300.000 nếu là máy in màu.
    /// </summary>
    public override decimal CalculateMaintenanceCost()
    {
        decimal cost = PurchasePrice * 0.04m;
        if (PagesPrinted > 100_000)
        {
            cost += 500_000m;
        }
        if (IsColor)
        {
            cost += 300_000m;
        }
        return cost;
    }

    public override string ToString()
    {
        return $"{base.ToString()} | Loại in: {Type} | Đã in: {PagesPrinted:N0} trang | In màu: {(IsColor ? "Có" : "Không")}";
    }
}
