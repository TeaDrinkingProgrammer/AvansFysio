using Domain;
using DomainServices;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure {
    public class ApiVektisRepo : IVektisRepo {
        private static readonly HttpClient client = new HttpClient();
        private static readonly JsonSerializerOptions options = new() {
            PropertyNameCaseInsensitive = true
        };
        public int Count() {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<VektisDiagnosis>> GetAllDiagnosesAsync() {

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Core App");
            var streamTask = client.GetStreamAsync("https://svh-avansfysioapi.azurewebsites.net/Diagnosis");
            return await JsonSerializer.DeserializeAsync<List<VektisDiagnosis>>(await streamTask, options);
        }

        public async Task<VektisDiagnosis> GetDiagnosisByIdAsync(int id) {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Core App");

            var streamTask = client.GetStreamAsync("https://svh-avansfysioapi.azurewebsites.net/Diagnosis/" + id);
            return await JsonSerializer.DeserializeAsync<VektisDiagnosis>(await streamTask, options);
        }

        public async Task<VektisProceeding> GetVektisProceedingByIdAsync(int id) {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Core App");

            var streamTask = client.GetStreamAsync("https://svh-avansfysioapi.azurewebsites.net/Proceeding/" + id);
            return await JsonSerializer.DeserializeAsync<VektisProceeding>(await streamTask, options);
        }

        public async Task<IEnumerable<VektisProceeding>> GetAllProceedingsAsync() {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Core App");

            var streamTask = client.GetStreamAsync("https://svh-avansfysioapi.azurewebsites.net/Proceeding");
            return await JsonSerializer.DeserializeAsync<List<VektisProceeding>>(await streamTask, options);
        }
    }
}
