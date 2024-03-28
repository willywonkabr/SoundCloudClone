using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using SoundCloudClone.Domain.Streaming;
using System.Text.Json;

namespace SoundCloudClone.Repository.Streaming
{
    public class BandaRepository
    {
        private readonly IHttpClientFactory _httpClientFactory = null;
        private int retries = 1;

        public BandaRepository(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }

        public Musica ObterMusica(Guid idMusica)
        {
            string url = $"/musicas/{idMusica}";

            HttpClient httpClient = _httpClientFactory.CreateHttpClient("SoundCloudApiServer");

            var response = httpClient.GetAsync(url).Result;

            if (response.IsSuccessStatusCode == false)
                throw new Exception("Música não encontrada.");
            
            var content = response.Content.ReadAsStringAsync().Result;

            return JsonSerializer.Deserialize<Musica>(content);
        }
    }
}
