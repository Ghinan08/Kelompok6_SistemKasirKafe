namespace KasirKafe_Kel6.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }
        public T? Data { get; private set; }
        private ApiResponse(bool success, string message, T? data)
        {
            Success = success;
            Message = message;
            Data = data;
        }
        public static ApiResponse<T> Ok(T data, string message = "Berhasil")
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data), "Data tidak boleh kosong pada response sukses.");

            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException("Pesan response tidak boleh kosong.", nameof(message));

            return new ApiResponse<T>(true, message, data);
        }
        public static ApiResponse<T> Fail(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException("Pesan error tidak boleh kosong.", nameof(message));

            return new ApiResponse<T>(false, message, default);
        }
    }
}