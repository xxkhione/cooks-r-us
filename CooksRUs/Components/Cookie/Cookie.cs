using Microsoft.JSInterop;
using Microsoft.AspNetCore.Http;

namespace CooksRUs.Components.Cookie
{
    public class Cookie : ICookie
    {
        private readonly IHttpContextAccessor _http;
        private readonly IJSRuntime JSRuntime;
        // # of days the cookie expires in
        int _expires;

        public Cookie(IHttpContextAccessor http, IJSRuntime js)
        {
            _http = http;
            JSRuntime = js;
            _expires = 30;
        }

        public async Task<int> GetValue(int def = -1)
        {
            var cValue = await GetCookie();
            if (string.IsNullOrEmpty(cValue)) { return def; }

            var vals = cValue.Split(";");
            foreach (var v in vals)
            {
                if (!string.IsNullOrEmpty(v) && v.IndexOf('=') > 0)
                {
                    if (v.Substring(0, v.IndexOf('=')).Trim().Equals("CurrentUserID", StringComparison.OrdinalIgnoreCase))
                    {
                        return int.Parse(v.Substring(v.IndexOf("=") + 1));
                    }
                }
            }
            return def;
        }

        private async Task<string> GetCookie()
        {
            var req = _http?.HttpContext?.Request;
            if (req?.Cookies != null && req.Cookies.Count > 0)
            {
                return string.Join("; ", req.Cookies.Select(kv => $"{kv.Key}={kv.Value}"));
            }
            // fallback to client interop (only when interactive)
            return await JSRuntime.InvokeAsync<string>("eval", "document.cookie");
        }

        public async Task SetValue(int value, int? days = null)
        {
            var curExp = (days != null && days > 0) ? DateToUTC(days.Value) : "";
            await SetCookie($"CurrentUserID = {value}; expires{curExp}; path=/");
        }

        private async Task SetCookie(string value)
        {
            await JSRuntime.InvokeVoidAsync("eval", $"document.cookie = \"{value}\"");
        }

        private static string DateToUTC(int value)
        {
            return DateTime.Now.AddDays(value).ToUniversalTime().ToString("R");
        }
    }
}
