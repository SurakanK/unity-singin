using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;
using System;
using System.Text;

public class ApiManager : MonoBehaviour
{
    public ENV env;
    private static ApiManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public async Task PostRequest(string url, string jsonBody, Action<string> Action)
    {

        HttpResponseMessage response = await PostData(url, jsonBody);
        if (response.IsSuccessStatusCode)
        {
            string responseJson = await response.Content.ReadAsStringAsync();
            Action(responseJson);
            Debug.Log("Response: " + responseJson);
        }
        else
        {
            Debug.LogError("Error: " + response.StatusCode);
        }
    }

    async Task<HttpResponseMessage> PostData(string url, string jsonBody)
    {
        using (HttpClient client = new HttpClient())
        {
            string apiUrl = EvnConfig.Api[env] + url;
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, apiUrl);
            request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            // request.Headers.Add("Authorization", "Bearer <access_token>");
            HttpResponseMessage response = await client.SendAsync(request);
            return response;
        }
    }
}
