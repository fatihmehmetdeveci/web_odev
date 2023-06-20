using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data;
using System.Diagnostics;
using web_odev.Models;

namespace web_odev.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IConfiguration _configuration;
        private string connectionString;
        
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            connectionString = configuration.GetSection("Connections").GetSection("MySQL_Admin").Value;
        }

        public IActionResult Index()
        {
            List<SliderModel> sliderModels = new List<SliderModel>();
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlDataAdapter mySql = new MySqlDataAdapter("Select * From sliders", connection);
            DataTable dataTable = new DataTable();
            mySql.Fill(dataTable);
            foreach (DataRow row in dataTable.Rows)
            {
                sliderModels.Add(
                    new SliderModel()
                    {
                        id = row.Field<int>("id"),
                        title = row.Field<string>("title"),
                        content = row.Field<string>("content"),
                        url = row.Field<string>("url"),
                        img = row.Field<string>("img")
                    });
            }
            List<ProductModel> productsModels = new List<ProductModel>();
            MySqlDataAdapter mySqlProduct = new MySqlDataAdapter("Select * From products", connection);
            DataTable dataTableProduct = new DataTable();
            mySqlProduct.Fill(dataTableProduct);
            foreach (DataRow row in dataTableProduct.Rows)
            {
                productsModels.Add(
                    new ProductModel()
                    {
                        id = row.Field<int>("id"),
                        title = row.Field<string>("title"),
                        content = row.Field<string>("content"),
                        url = row.Field<string>("url"),
                        img = row.Field<string>("img")
                    });
            }
            ViewBag.productModels = productsModels;
            return View(sliderModels);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}