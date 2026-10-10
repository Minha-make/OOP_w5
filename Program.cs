/****************/  

// Mã sinh viên: 202418890

// Họ tên: Nguyễn Minh Hà  

/****************/ 

namespace DeviceManagement;

using DeviceManagement.Interfaces;
using DeviceManagement.Models;

internal class Program
{
    private static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        int currentYear = DateTime.Now.Year;

        Console.WriteLine("================================================================================");
        Console.WriteLine("        CHƯƠNG TRÌNH QUẢN LÝ THIẾT BỊ VÀ BẢO TRÌ PHÒNG THỰC HÀNH");
        Console.WriteLine("================================================================================\n");

        // -----------------------------------------------------------------------------
        // KHỞI TẠO DỮ LIỆU MẪU THEO YÊU CẦU:
        // - 2 Computer (1 máy có GPU rời, 1 máy đã dùng trên 5 năm)
        // - 2 Máy in (1 máy in > 100.000 trang, ít nhất 1 NetworkPrinter, ít nhất 1 in màu)
        // - 1 Projector (có LampHours > 3000 giờ)
        // - 2 LabRoom
        // - Đặt ít nhất 1 thiết bị ở trạng thái UnderMaintenance
        // -----------------------------------------------------------------------------

        // Computer 1: Có GPU rời, tuổi 2 năm (<= 5 năm)
        var pc1 = new Computer(
            id: "PC01",
            name: "Máy trạm AI Dell Precision 3660",
            yearInUse: currentYear - 2,
            purchasePrice: 40_000_000m,
            ramGb: 32,
            processor: "Intel Core i7-13700",
            hasDedicatedGpu: true,
            status: DeviceStatus.Active
        );

        // Computer 2: Tuổi 6 năm (> 5 năm), không GPU rời
        var pc2 = new Computer(
            id: "PC02",
            name: "Máy bàn văn phòng HP ProDesk 400 G6",
            yearInUse: currentYear - 6,
            purchasePrice: 15_000_000m,
            ramGb: 16,
            processor: "Intel Core i5-10400",
            hasDedicatedGpu: false,
            status: DeviceStatus.Active
        );

        // Printer 1: Máy in laser thông thường, in 125.000 trang (> 100.000), trạng thái UnderMaintenance
        var pr1 = new Printer(
            id: "PR01",
            name: "Máy in văn phòng Canon LBP 2900",
            yearInUse: currentYear - 3,
            purchasePrice: 4_500_000m,
            type: PrinterType.Laser,
            pagesPrinted: 125_000,
            isColor: false,
            status: DeviceStatus.UnderMaintenance
        );

        // Printer 2: NetworkPrinter, Phun màu, in 35.000 trang
        var pr2 = new NetworkPrinter(
            id: "PR02",
            name: "Máy in màu đa năng HP OfficeJet Pro 9020",
            yearInUse: currentYear - 1,
            purchasePrice: 10_000_000m,
            type: PrinterType.Inkjet,
            pagesPrinted: 35_000,
            isColor: true,
            status: DeviceStatus.Active
        );

        // Projector: Đèn chiếu đã chạy 3.400 giờ (> 3.000 giờ)
        var pj1 = new Projector(
            id: "PJ01",
            name: "Máy chiếu hội trường Sony VPL-EX570",
            yearInUse: currentYear - 4,
            purchasePrice: 20_000_000m,
            lumen: 4200,
            lampHours: 3400,
            status: DeviceStatus.Active
        );

        // 2 Phòng thực hành
        var lab1 = new LabRoom(id: "LAB101", name: "Phòng Thực hành Mạng & Đồ họa", capacity: 10);
        var lab2 = new LabRoom(id: "LAB102", name: "Phòng Thực hành Tin học Cơ sở", capacity: 5);

        // =============================================================================
        // THAO TÁC 1: THÊM THIẾT BỊ VÀO CÁC PHÒNG
        // =============================================================================
        Console.WriteLine("--- 1. THÊM THIẾT BỊ VÀO PHÒNG THỰC HÀNH ---");
        lab1.AddDevice(pc1);
        lab1.AddDevice(pr2);
        lab1.AddDevice(pj1);
        Console.WriteLine($"[+] Đã thêm 3 thiết bị ({pc1.Id}, {pr2.Id}, {pj1.Id}) vào phòng '{lab1.Name}'.");

        lab2.AddDevice(pc2);
        lab2.AddDevice(pr1);
        Console.WriteLine($"[+] Đã thêm 2 thiết bị ({pc2.Id}, {pr1.Id}) vào phòng '{lab2.Name}'.");
        Console.WriteLine();

        // =============================================================================
        // THAO TÁC 2: THỬ THÊM THIẾT BỊ TRÙNG MÃ (BẮT LỖI BẰNG TRY/CATCH)
        // =============================================================================
        Console.WriteLine("--- 2. KIỂM THỬ THÊM THIẾT BỊ TRÙNG MÃ ---");
        try
        {
            var duplicatePc = new Computer(
                id: "PC01", // Trùng mã với pc1 trong lab1
                name: "Máy tính trùng mã",
                yearInUse: currentYear,
                purchasePrice: 12_000_000m,
                ramGb: 8,
                processor: "Intel Core i3",
                hasDedicatedGpu: false
            );
            Console.WriteLine("Đang thử thêm thiết bị mã 'PC01' lần thứ hai vào phòng LAB101...");
            lab1.AddDevice(duplicatePc);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"[BẮT ĐƯỢC LỖI DỰ KIẾN]: {ex.Message}");
        }
        Console.WriteLine();

        // =============================================================================
        // THAO TÁC 3: IN DANH SÁCH THIẾT BỊ CỦA TỪNG PHÒNG
        // =============================================================================
        Console.WriteLine("--- 3. DANH SÁCH THIẾT BỊ TỪNG PHÒNG THỰC HÀNH ---");
        var allRooms = new List<LabRoom> { lab1, lab2 };
        foreach (var room in allRooms)
        {
            Console.WriteLine($"\n>> {room}");
            int stt = 1;
            foreach (var dev in room.Devices)
            {
                Console.WriteLine($"   {stt++}. {dev}");
            }
        }
        Console.WriteLine();

        // =============================================================================
        // THAO TÁC 4: TỔNG CHI PHÍ BẢO TRÌ DỰ KIẾN CỦA MỖI PHÒNG (ĐA HÌNH)
        // =============================================================================
        Console.WriteLine("--- 4. TỔNG CHI PHÍ BẢO TRÌ DỰ KIẾN MỖI PHÒNG (ĐA HÌNH) ---");
        foreach (var room in allRooms)
        {
            decimal totalCost = room.CalculateAnnualMaintenanceCost();
            Console.WriteLine($"• {room.Name}: {totalCost:N0} VNĐ/năm");
        }
        decimal grandTotal = allRooms.Sum(r => r.CalculateAnnualMaintenanceCost());
        Console.WriteLine($"=> TỔNG CỘNG TẤT CẢ CÁC PHÒNG: {grandTotal:N0} VNĐ/năm\n");

        // =============================================================================
        // THAO TÁC 5: LIỆT KÊ CÁC THIẾT BỊ CẦN BẢO TRÌ
        // =============================================================================
        Console.WriteLine("--- 5. DANH SÁCH THIẾT BỊ CẦN BẢO TRÌ (Status = UnderMaintenance HOẶC Tuổi > 5) ---");
        foreach (var room in allRooms)
        {
            var maintenanceDevices = room.GetDevicesRequiringMaintenance();
            Console.WriteLine($"\nPhòng {room.Name} ({maintenanceDevices.Count} thiết bị cần bảo trì):");
            if (maintenanceDevices.Count == 0)
            {
                Console.WriteLine("   (Không có thiết bị nào cần bảo trì)");
            }
            else
            {
                foreach (var dev in maintenanceDevices)
                {
                    string reasons = "";
                    if (dev.Status == DeviceStatus.UnderMaintenance)
                    {
                        reasons += "[Trạng thái đang sửa chữa/bảo trì] ";
                    }
                    if (dev.Age > 5)
                    {
                        reasons += $"[Tuổi sử dụng {dev.Age} năm > 5 năm] ";
                    }
                    Console.WriteLine($"   * [{dev.Id}] {dev.Name} -> Lý do: {reasons.Trim()}");
                }
            }
        }
        Console.WriteLine();

        // =============================================================================
        // THAO TÁC 6: KẾT NỐI MẠNG, BẮT LỖI KẾT NỐI LẠI, IP RỖNG, DISCONNECT
        // =============================================================================
        Console.WriteLine("--- 6. KIỂM THỬ KHẢ NĂNG KẾT NỐI MẠNG (INetworkable) ---");
        // 6.1. Kết nối hợp lệ
        Console.WriteLine("a. Kết nối mạng hợp lệ:");
        pc1.Connect("192.168.1.101");
        Console.WriteLine($"   - {pc1.Name}: Đã kết nối IP '{pc1.IpAddress}', IsConnected = {pc1.IsConnected}");
        pr2.Connect("192.168.1.200");
        Console.WriteLine($"   - {pr2.Name}: Đã kết nối IP '{pr2.IpAddress}', IsConnected = {pr2.IsConnected}");

        // 6.2. Thử kết nối lại khi đang kết nối
        Console.WriteLine("\nb. Thử kết nối lại khi đang kết nối:");
        try
        {
            Console.WriteLine("   Đang thử gọi pc1.Connect(\"192.168.1.102\")...");
            pc1.Connect("192.168.1.102");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"   [BẮT ĐƯỢC LỖI DỰ KIẾN]: {ex.Message}");
        }

        // 6.3. Thử kết nối với IP rỗng / khoảng trắng
        Console.WriteLine("\nc. Thử kết nối với IP rỗng:");
        try
        {
            Console.WriteLine("   Đang thử gọi pr2.Connect(\"   \")...");
            pr2.Connect("   ");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"   [BẮT ĐƯỢC LỖI DỰ KIẾN]: {ex.Message}");
        }

        // 6.4. Ngắt kết nối và kiểm tra IsConnected, IpAddress
        Console.WriteLine("\nd. Ngắt kết nối (Disconnect):");
        Console.WriteLine($"   Trước khi ngắt: IsConnected = {pc1.IsConnected}, IpAddress = '{pc1.IpAddress}'");
        pc1.Disconnect();
        Console.WriteLine($"   Sau khi ngắt:   IsConnected = {pc1.IsConnected}, IpAddress = '{(string.IsNullOrEmpty(pc1.IpAddress) ? "(rỗng)" : pc1.IpAddress)}'");
        Console.WriteLine();

        // =============================================================================
        // THAO TÁC 7: DUYỆT THIẾT BỊ MẠNG QUA OfType<INetworkable>()
        // =============================================================================
        Console.WriteLine("--- 7. DUYỆT CÁC THIẾT BỊ MẠNG QUA INTERFACE INetworkable ---");
        Console.WriteLine("(Sử dụng hoàn toàn kiểu INetworkable, không ép kiểu về Computer hay NetworkPrinter)\n");

        foreach (var room in allRooms)
        {
            Console.WriteLine($"Phòng '{room.Name}':");
            IEnumerable<INetworkable> networkDevices = room.Devices.OfType<INetworkable>();
            int count = 0;
            foreach (INetworkable netDevice in networkDevices)
            {
                count++;
                string statusStr = netDevice.IsConnected
                    ? $"Đang kết nối tới IP: {netDevice.IpAddress}"
                    : "Hiện đang ngắt kết nối";
                Console.WriteLine($"   [Thiết bị mạng #{count}] Trạng thái: {statusStr}");
            }
            if (count == 0)
            {
                Console.WriteLine("   (Không có thiết bị nào hỗ trợ mạng)");
            }
        }
        Console.WriteLine();

        // =============================================================================
        // THAO TÁC 8: MINH HỌA CÁC TRƯỜNG HỢP DỮ LIỆU SAI
        // =============================================================================
        Console.WriteLine("--- 8. KIỂM THỬ XỬ LÝ DỮ LIỆU KHÔNG HỢP LỆ (TRY/CATCH) ---");

        // 8.1. Giá mua bằng 0
        try
        {
            Console.WriteLine("1. Thử tạo Computer với giá mua = 0 VNĐ:");
            var invalidDevice = new Computer("ERR01", "PC Lỗi Giá", currentYear, 0m, 8, "i5", false);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"   [BẮT ĐƯỢC LỖI DỰ KIẾN]: {ex.Message}");
        }

        // 8.2. Mã thiết bị rỗng
        try
        {
            Console.WriteLine("2. Thử tạo Printer với mã thiết bị rỗng (chuỗi trắng):");
            var invalidDevice = new Printer("   ", "Máy In Lỗi Mã", currentYear, 5_000_000m, PrinterType.Laser, 0, false);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"   [BẮT ĐƯỢC LỖI DỰ KIẾN]: {ex.Message}");
        }

        // 8.3. Năm đưa vào sử dụng ở tương lai
        try
        {
            Console.WriteLine($"3. Thử tạo Projector với năm sử dụng ở tương lai ({currentYear + 1}):");
            var invalidDevice = new Projector("ERR03", "Máy Chiếu Tương Lai", currentYear + 1, 15_000_000m, 3000, 100);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"   [BẮT ĐƯỢC LỖI DỰ KIẾN]: {ex.Message}");
        }

        // 8.4. Thêm thiết bị null vào phòng
        try
        {
            Console.WriteLine("4. Thử thêm thiết bị null vào phòng:");
            lab1.AddDevice(null!);
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine($"   [BẮT ĐƯỢC LỖI DỰ KIẾN]: {ex.Message}");
        }
        Console.WriteLine();

        // =============================================================================
        // THAO TÁC 9: ĐỐI CHIẾU TÍNH TAY CHI PHÍ BẢO TRÌ VỚI KẾT QUẢ CHƯƠNG TRÌNH
        // =============================================================================
        Console.WriteLine("--- 9. ĐỐI CHIẾU TÍNH TAY VÀ TÍNH TỰ ĐỘNG CHI PHÍ BẢO TRÌ ---");

        Console.WriteLine("\n[Thiết bị 1: PC01 - Máy trạm AI Dell Precision 3660]");
        Console.WriteLine($" - Giá mua: {pc1.PurchasePrice:N0} VNĐ, GPU rời: Có, Tuổi: {pc1.Age} năm");
        Console.WriteLine(" - Công thức tính tay:");
        Console.WriteLine($"   + Cơ bản (5%): {pc1.PurchasePrice:N0} * 0.05 = {pc1.PurchasePrice * 0.05m:N0} VNĐ");
        Console.WriteLine($"   + GPU rời (+2%): {pc1.PurchasePrice:N0} * 0.02 = {pc1.PurchasePrice * 0.02m:N0} VNĐ");
        Console.WriteLine($"   + Tuổi > 5 năm (+1%): 0 VNĐ (Do tuổi = {pc1.Age} <= 5)");
        decimal manualPc1 = (pc1.PurchasePrice * 0.05m) + (pc1.PurchasePrice * 0.02m);
        decimal autoPc1 = pc1.CalculateMaintenanceCost();
        Console.WriteLine($"   => Chi phí tính tay:       {manualPc1:N0} VNĐ");
        Console.WriteLine($"   => Chương trình tính được: {autoPc1:N0} VNĐ");
        Console.WriteLine($"   => Trạng thái khớp: {(manualPc1 == autoPc1 ? "CHÍNH XÁC (Khớp 100%)" : "SAI LỆCH")}");

        Console.WriteLine("\n[Thiết bị 2: PR01 - Máy in Canon LBP 2900]");
        Console.WriteLine($" - Giá mua: {pr1.PurchasePrice:N0} VNĐ, Trang in: {pr1.PagesPrinted:N0}, In màu: {(pr1.IsColor ? "Có" : "Không")}");
        Console.WriteLine(" - Công thức tính tay:");
        Console.WriteLine($"   + Cơ bản (4%): {pr1.PurchasePrice:N0} * 0.04 = {pr1.PurchasePrice * 0.04m:N0} VNĐ");
        Console.WriteLine("   + Trang in > 100.000: +500.000 VNĐ");
        Console.WriteLine("   + In màu (+300.000): +0 VNĐ (Không in màu)");
        decimal manualPr1 = (pr1.PurchasePrice * 0.04m) + 500_000m;
        decimal autoPr1 = pr1.CalculateMaintenanceCost();
        Console.WriteLine($"   => Chi phí tính tay:       {manualPr1:N0} VNĐ");
        Console.WriteLine($"   => Chương trình tính được: {autoPr1:N0} VNĐ");
        Console.WriteLine($"   => Trạng thái khớp: {(manualPr1 == autoPr1 ? "CHÍNH XÁC (Khớp 100%)" : "SAI LỆCH")}");

        Console.WriteLine("\n[Thiết bị 3: PJ01 - Máy chiếu Sony VPL-EX570]");
        Console.WriteLine($" - Giá mua: {pj1.PurchasePrice:N0} VNĐ, Giờ đèn: {pj1.LampHours:N0}h");
        Console.WriteLine(" - Công thức tính tay:");
        Console.WriteLine($"   + Cơ bản (3%): {pj1.PurchasePrice:N0} * 0.03 = {pj1.PurchasePrice * 0.03m:N0} VNĐ");
        Console.WriteLine("   + Giờ đèn > 3.000h: +1.500.000 VNĐ");
        decimal manualPj1 = (pj1.PurchasePrice * 0.03m) + 1_500_000m;
        decimal autoPj1 = pj1.CalculateMaintenanceCost();
        Console.WriteLine($"   => Chi phí tính tay:       {manualPj1:N0} VNĐ");
        Console.WriteLine($"   => Chương trình tính được: {autoPj1:N0} VNĐ");
        Console.WriteLine($"   => Trạng thái khớp: {(manualPj1 == autoPj1 ? "CHÍNH XÁC (Khớp 100%)" : "SAI LỆCH")}");

        Console.WriteLine("\n================================================================================");
        Console.WriteLine("                       KẾT THÚC KIỂM THỬ");
        Console.WriteLine("================================================================================");
    }
}
