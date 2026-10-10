/****************/  

// Mã sinh viên: 202418890

// Họ tên: Nguyễn Minh Hà  

/****************/ 

namespace DeviceManagement.Models;

/// <summary>
/// Lớp đại diện cho phòng thực hành chứa các thiết bị (quan hệ kết tập - Aggregation).
/// </summary>
public class LabRoom
{
    private string _name = string.Empty;
    private int _capacity;
    private readonly List<Device> _devices = new();

    /// <summary>
    /// Mã phòng thực hành (chỉ đọc).
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// Tên phòng thực hành (không được để trống).
    /// </summary>
    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Tên phòng không được để trống hoặc chỉ chứa khoảng trắng.", nameof(value));
            _name = value;
        }
    }

    /// <summary>
    /// Sức chứa tối đa của phòng (phải lớn hơn 0).
    /// </summary>
    public int Capacity
    {
        get => _capacity;
        set
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Sức chứa phòng phải lớn hơn 0.");
            _capacity = value;
        }
    }

    /// <summary>
    /// Danh sách thiết bị hiện có trong phòng (chỉ đọc từ bên ngoài).
    /// </summary>
    public IReadOnlyList<Device> Devices => _devices.AsReadOnly();

    /// <summary>
    /// Khởi tạo một phòng thực hành mới.
    /// </summary>
    public LabRoom(string id, string name, int capacity)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentException("Mã phòng không được để trống hoặc chỉ chứa khoảng trắng.", nameof(id));

        Id = id;
        Name = name;
        Capacity = capacity;
    }

    /// <summary>
    /// Thêm một thiết bị vào phòng thực hành.
    /// </summary>
    /// <param name="device">Thiết bị cần thêm.</param>
    /// <exception cref="ArgumentNullException">Khi thiết bị là null.</exception>
    /// <exception cref="InvalidOperationException">Khi thiết bị trùng mã hoặc phòng đã đầy sức chứa.</exception>
    public void AddDevice(Device device)
    {
        if (device == null)
            throw new ArgumentNullException(nameof(device), "Đối tượng thiết bị không được null.");

        if (_devices.Any(d => d.Id.Equals(device.Id, StringComparison.OrdinalIgnoreCase)))
            throw new InvalidOperationException($"Thiết bị với mã '{device.Id}' đã tồn tại trong phòng '{Name}'.");

        if (_devices.Count >= Capacity)
            throw new InvalidOperationException($"Phòng '{Name}' đã đạt sức chứa tối đa ({Capacity} thiết bị). Không thể thêm mới.");

        _devices.Add(device);
    }

    /// <summary>
    /// Gỡ bỏ thiết bị khỏi phòng thực hành theo mã thiết bị.
    /// Thiết bị không bị hủy, chỉ bị tách khỏi phòng (thể hiện quan hệ kết tập).
    /// </summary>
    /// <param name="deviceId">Mã thiết bị cần gỡ.</param>
    /// <returns>True nếu gỡ thành công, False nếu không tìm thấy.</returns>
    public bool RemoveDevice(string deviceId)
    {
        var device = FindDevice(deviceId);
        if (device == null)
            return false;

        return _devices.Remove(device);
    }

    /// <summary>
    /// Tìm kiếm thiết bị trong phòng theo mã thiết bị.
    /// </summary>
    /// <param name="deviceId">Mã thiết bị cần tìm.</param>
    /// <returns>Thiết bị nếu tìm thấy, ngược lại trả về null.</returns>
    public Device? FindDevice(string deviceId)
    {
        if (string.IsNullOrWhiteSpace(deviceId))
            return null;

        return _devices.FirstOrDefault(d => d.Id.Equals(deviceId, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Tính tổng chi phí bảo trì dự kiến hàng năm của toàn bộ thiết bị trong phòng.
    /// Sử dụng tính đa hình hoàn toàn (polymorphism), không kiểm tra kiểu cụ thể.
    /// </summary>
    public decimal CalculateAnnualMaintenanceCost()
    {
        return _devices.Sum(d => d.CalculateMaintenanceCost());
    }

    /// <summary>
    /// Lấy danh sách các thiết bị cần bảo trì:
    /// Có trạng thái UnderMaintenance hoặc tuổi sử dụng > 5 năm.
    /// </summary>
    public List<Device> GetDevicesRequiringMaintenance()
    {
        return _devices.Where(d => d.Status == DeviceStatus.UnderMaintenance || d.Age > 5).ToList();
    }

    public override string ToString()
    {
        return $"[Phòng: {Id}] {Name} (Sức chứa: {_devices.Count}/{Capacity})";
    }
}
