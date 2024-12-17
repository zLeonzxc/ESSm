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

                Debug.WriteLine($"Informing API about logout for user: {username}");

                var content = new StringContent(JsonSerializer.Serialize(username), Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync("https://10.0.2.2:7087/api/Users/logout", content);

                if (!response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine($"Failed to inform API about logout. Status Code: {response.StatusCode}, Response: {responseContent}");
                }
                else
                {
                    Debug.WriteLine("Successfully informed API about logout.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error informing API about logout: {ex.Message}");
            }
        }
    }
}
