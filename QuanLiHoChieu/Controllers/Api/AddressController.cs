using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

[ApiController]
[Route("api/[controller]")]
public class AddressController : ControllerBase
{
    private readonly string dataPath = Path.Combine(Directory.GetCurrentDirectory(), "Data");

    private List<Province>? LoadAddresses()
    {
        var filePath = Path.Combine(dataPath, "Addresses.json");
        if (!System.IO.File.Exists(filePath)) return null;

        var json = System.IO.File.ReadAllText(filePath);
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        return JsonSerializer.Deserialize<List<Province>>(json, options);
    }

    // GET api/Address/addresses
    [HttpGet("addresses")]
    public IActionResult GetProvinces()
    {
        var provinces = LoadAddresses();
        if (provinces == null) return NotFound("Addresses data not found.");

        var provinceList = provinces.Select(p => new { p.Code, p.Name }).ToList();
        return Ok(provinceList);
    }

    [HttpGet("addresses/p/{provinceCode}")]
    public IActionResult GetProvinceDetails(int provinceCode)
    {
        var provinces = LoadAddresses();
        if (provinces == null) return NotFound("Addresses data not found.");

        var province = provinces.FirstOrDefault(p => p.Code == provinceCode);
        if (province == null) return NotFound("Province not found.");

        return Ok(province);
    }

    [HttpGet("addresses/d/{districtCode}")]
    public IActionResult GetDistrictDetails(int districtCode)
    {
        var provinces = LoadAddresses();
        if (provinces == null) return NotFound("Addresses data not found.");

        foreach (var province in provinces)
        {
            var district = province.Districts?.FirstOrDefault(d => d.Code == districtCode);
            if (district != null)
            {
                return Ok(district);  // includes wards
            }
        }

        return NotFound("District not found.");
    }


    public class Province
    {
        public int Code { get; set; }       // int instead of string
        public string? Name { get; set; }
        public List<District>? Districts { get; set; }
    }

    public class District
    {
        public int Code { get; set; }
        public string? Name { get; set; }
        public List<Ward>? Wards { get; set; }
    }

    public class Ward
    {
        public int Code { get; set; }
        public string? Name { get; set; }
    }
}
