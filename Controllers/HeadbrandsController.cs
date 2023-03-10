using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Head_brands.web.Data;
using Head_brands.web.Models;
using Head_brands.web.ViewModel.Heads;

[Route("Heads")]
    public class HeadBrandsController : Controller
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly HeadIndexContext _context;
        private readonly string _baseUrl;
        private readonly JsonSerializerOptions _options;

        public HeadBrandsController(HeadIndexContext context, IConfiguration config, IHttpClientFactory httpClient)
        {   
            _context = context;
            _httpClient = httpClient;
            _baseUrl = config.GetSection("apiSettings:baseUrl").Value;
            _options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            
        }
        [HttpGet("list")]
        public async Task<IActionResult> Index()
        {
             using var client = _httpClient.CreateClient();
              var response = await client.GetAsync($"{_baseUrl}/Heads");
            if (!response.IsSuccessStatusCode)
            {
                return Content("Error");
            }
            var json = await response.Content.ReadAsStringAsync();

            var head = JsonSerializer.Deserialize<List<HeadListViewModel>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return View("Index", head);
        }

    [HttpGet("details/{id}")]
    public async Task<IActionResult> Details(int id)
    {
        using var client = _httpClient.CreateClient();
        var response = await client.GetAsync($"{_baseUrl}/heads/{id}");

        if (!response.IsSuccessStatusCode) return Content("Oops det gick fel");

        var json = await response.Content.ReadAsStringAsync();

        var head = JsonSerializer.Deserialize<HeadListViewModel>(json, _options);

        return View("Details", head);
    }

       
       
        [HttpGet("create")]
        public IActionResult Create()
        {
            var head = new HeadModel();

            return View("Create", head);
        }
        [HttpPost ("create")]
        public async Task<IActionResult> Create(HeadsPostListModel head)
        {
            if (!ModelState.IsValid) return View("Create", head);

            var headToAdd = new HeadModel
            {
                Name = head.Name,
                Brand = head.Brand,
                Description = head.Description,
            };
                var json = JsonSerializer.Serialize(headToAdd);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                using var client = _httpClient.CreateClient();
                var response = await client.PostAsync($"{_baseUrl}/Heads", data);

                if (response.IsSuccessStatusCode)
            {
                var createdHeadJson = await response.Content.ReadAsStringAsync();
                var createdHead = JsonSerializer.Deserialize<HeadsPostListModel>(createdHeadJson);
                // Do something with the created head, e.g. redirect to a details page
                return RedirectToAction("Index");
            }
            else
            {
                // Handle the error response
                return Content("Error");
            }
            
        }
        
    }
