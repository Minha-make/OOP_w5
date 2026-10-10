/****************/  

// Mã sinh viên: 202418890

// Họ tên: Nguyễn Minh Hà  

/****************/ 

namespace DeviceManagement.Models;

using DeviceManagement.Interfaces;

/// <summary>
/// Lớp thành phần quản lý logic kết nối mạng, dùng để ủy quyền (composition).
/// </summary>
public class NetworkConnection : INetworkable
{
    /// <summary>
    /// Địa chỉ IP hiện tại (rỗng nếu chưa kết nối).
    /// </summary>
    public string IpAddress { get; private set; } = string.Empty;

    /// <summary>
    /// Trạng thái kết nối hiện tại.
    /// </summary>
    public bool IsConnected { get; private set; }

    /// <summary>
    /// Kết nối đến mạng với địa chỉ IP được cung cấp.
    /// </summary>
    /// <param name="ipAddress">Địa chỉ IP kết nối.</param>
    /// <exception cref="ArgumentException">Khi địa chỉ IP null, rỗng hoặc toàn khoảng trắng.</exception>
    /// <exception cref="InvalidOperationException">Khi thiết bị đã và đang kết nối mạng.</exception>
    public void Connect(string ipAddress)
    {
        if (string.IsNullOrWhiteSpace(ipAddress))
            throw new ArgumentException("Địa chỉ IP không được để trống hoặc chỉ chứa khoảng trắng.", nameof(ipAddress));

        if (IsConnected)
            throw new InvalidOperationException("Thiết bị đang kết nối mạng, không được phép kết nối lại.");

        IpAddress = ipAddress.Trim();
        IsConnected = true;
    }

    /// <summary>
    /// Ngắt kết nối mạng hiện tại.
    /// </summary>
    public void Disconnect()
    {
        IsConnected = false;
        IpAddress = string.Empty;
    }
}
