/****************/  

// Mã sinh viên: 202418890

// Họ tên: Nguyễn Minh Hà  

/****************/ 

namespace DeviceManagement.Models;

using DeviceManagement.Interfaces;

/// <summary>
/// Lớp đại diện cho máy in có hỗ trợ kết nối mạng.
/// </summary>
public class NetworkPrinter : Printer, INetworkable
{
    private readonly NetworkConnection _connection = new();

    #region INetworkable Implementation (ủy quyền cho NetworkConnection)

    /// <summary>
    /// Địa chỉ IP hiện tại của máy in mạng.
    /// </summary>
    public string IpAddress => _connection.IpAddress;

    /// <summary>
    /// Trạng thái kết nối mạng của máy in.
    /// </summary>
    public bool IsConnected => _connection.IsConnected;

    /// <summary>
    /// Kết nối máy in vào mạng với IP chỉ định.
    /// </summary>
    public void Connect(string ipAddress) => _connection.Connect(ipAddress);

    /// <summary>
    /// Ngắt kết nối mạng của máy in.
    /// </summary>
    public void Disconnect() => _connection.Disconnect();

    #endregion

    /// <summary>
    /// Khởi tạo một đối tượng máy in mạng mới.
    /// </summary>
    public NetworkPrinter(
        string id,
        string name,
        int yearInUse,
        decimal purchasePrice,
        PrinterType type,
        int pagesPrinted,
        bool isColor,
        DeviceStatus status = DeviceStatus.Active)
        : base(id, name, yearInUse, purchasePrice, type, pagesPrinted, isColor, status)
    {
    }

    public override string ToString()
    {
        string networkInfo = IsConnected ? $"Đã kết nối ({IpAddress})" : "Chưa kết nối";
        return $"{base.ToString()} | Mạng: {networkInfo}";
    }
}
