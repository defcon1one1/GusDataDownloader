﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace GusDataDownloader.Models
{
    public class Area
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("nazwa")]
        public string Nazwa { get; set; }

        [JsonProperty("id-nadrzedny-element")]
        public int IdNadrzednyElement { get; set; }

        [JsonProperty("id-poziom")]
        public int IdPoziom { get; set; }

        [JsonProperty("nazwa-poziom")]
        public string NazwaPoziom { get; set; }

        [JsonProperty("czy-zmienne")]
        public bool CzyZmienne { get; set; }
    }


}
