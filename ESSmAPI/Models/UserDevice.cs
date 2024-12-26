namespace ESSmAPI.Models
{
    public class UserDevice
    {
        public int Id { get; set; }
        public string? DeviceManufacturer { get; set; }
        public string? DeviceModel { get; set; }
        public string? AppVersion { get; set; }
        public string? DeviceOS { get; set; }
        public string? DeviceOSVersion { get; set; }
        public string? DeviceName { get; set; }
        public string? DeviceType { get; set; }
    }
}
