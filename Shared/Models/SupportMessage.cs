using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace SupportCosmos.Shared.Models
{
    public class SupportMessage
    {
        [JsonProperty(PropertyName = "id")]
        [JsonPropertyName("id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [JsonProperty(PropertyName = "name")]
        [JsonPropertyName("name")]
        [Required(ErrorMessage = "Navn er påkrævet")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "email")]
        [JsonPropertyName("email")]
        [Required(ErrorMessage = "Email er påkrævet")]
        public string Email { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "phone")]
        [Required(ErrorMessage = "Telefonnummer skal udfyldes.")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "Telefonnummeret skal bestå af præcis 8 tal.")]
        [JsonPropertyName("phone")]
        public string Phone { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "category")]
        [JsonPropertyName("category")]
        [Required(ErrorMessage = "Vælg venligst en kategori")]
        public string Category { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "description")]
        [Required(ErrorMessage = "Beskriv venligst din henvendelse")]
        [StringLength(500, ErrorMessage = "Beskrivelsen må højst være 500 tegn")]
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "createdUtc")]
        [JsonPropertyName("createdUtc")]
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
    }
}