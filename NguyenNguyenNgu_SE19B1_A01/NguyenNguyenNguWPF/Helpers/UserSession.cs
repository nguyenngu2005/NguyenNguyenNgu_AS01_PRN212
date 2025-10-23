namespace NguyenNguyenNguWPF.Helpers
{
    public static class UserSession
    {
        public static string ?CurrentEmail { get; set; } = string.Empty;
        public static bool IsAdmin { get; set; }
        public static void Clear()
        {
            CurrentEmail = null;
            IsAdmin = false;
        }
    }
}
