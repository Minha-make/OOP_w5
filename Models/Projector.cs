/****************/  

// Mã sinh viên: 202418890

// Họ tên: Nguyễn Minh Hà  

/****************/ 

namespace DeviceManagement.Models;

/// <summary>
/// Lớp đại diện cho máy chiếu trong phòng thực hành.
/// </summary>
public class Projector : Device
{
    private int _lumen;
    private int _lampHours;

    /// <summary>
    /// Độ sáng của máy chiếu (Lumen), phải lớn hơn 0.
    /// </summary>
    public int Lumen
    {
        get => _lumen;
        set
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Độ sáng (Lumen) phải lớn hơn 0.");
            _lumen = value;
        }
    }

    /// <summary>
    /// Số giờ đã sử dụng bóng đèn (giờ), không được âm.
    /// </summary>
    public int LampHours
    {
        get => _lampHours;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Số giờ bóng đèn đã sử dụng không được âm.");
            _lampHours = value;
        }
    }

    /// <summary>
    /// Khởi tạo một đối tượng máy chiếu mới.
    /// </summary>
    public Projector(
        string id,
        string name,
        int yearInUse,
        decimal purchasePrice,
        int lumen,
        int lampHours,
        DeviceStatus status = DeviceStatus.Active)
        : base(id, name, yearInUse, purchasePrice, status)
    {
        Lumen = lumen;
        LampHours = lampHours;
    }

    /// <summary>
    /// Tính chi phí bảo trì hàng năm:
    /// 3% giá mua + 1.500.000 nếu số giờ bóng đèn > 3000 giờ.
    /// </summary>
    public override decimal CalculateMaintenanceCost()
    {
        decimal cost = PurchasePrice * 0.03m;
        if (LampHours > 3000)
        {
            cost += 1_500_000m;
        }
        return cost;
    }

    public override string ToString()
    {
        return $"{base.ToString()} | Độ sáng: {Lumen:N0} Lumens | Giờ đèn: {LampHours:N0}h";
    }
}
