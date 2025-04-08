namespace ESSmPrototype.Custom
{
    public static class BasicAuth
    {
        // LOGOUT
        public static async Task NotifyLogout()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };

            using var httpClient = new HttpClient(handler);

            try
            {
                var username = Preferences.Get("Username", string.Empty);

                var content = new StringContent(JsonSerializer.Serialize(username), Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync("https://10.0.2.2:8198/api/Users/logout", content);

                if (!response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine($"Error encountered during logout. Status Code: {response.StatusCode}, Server Response: {responseContent}");
                }
                else
                {
                    Debug.WriteLine("Logout successful.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error occured: {ex.Message}");
            }
        }
    }
}
