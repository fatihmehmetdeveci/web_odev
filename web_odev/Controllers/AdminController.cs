using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data;
using web_odev.Models;

namespace web_odev.Controllers
{
    public class AdminController : Controller
    {
        private IConfiguration _configuration;
        private string connectionString;

        public AdminController(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = configuration.GetSection("Connections").GetSection("MySQL_Admin").Value;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                return Redirect("/Admin/Login");
            }
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

            return View(sliderModels);
        }

        public IActionResult Sliders()
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                return Redirect("/Admin/Login");
            }
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

            return View(sliderModels);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel loginModel)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlDataAdapter adapter = new MySqlDataAdapter("select * from users Where username=@username and password=@password", connection);
            adapter.SelectCommand.Parameters.Clear();
            adapter.SelectCommand.Parameters.AddWithValue("@username", loginModel.username);
            adapter.SelectCommand.Parameters.AddWithValue("@password", loginModel.password);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            if (dataTable.Rows.Count > 0)
            {
                HttpContext.Session.SetString("username", loginModel.username);
                return Redirect("/Admin/Index");
            }
            else
            {
                return Redirect("/Admin/Login");
            }
        }

        [HttpGet]
        public IActionResult SaveSlider()
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                return Redirect("/Admin/Login");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveSlider(SliderModel sliderModel)
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                return Redirect("/Admin/Login");
            }
            IFormFile formFile = sliderModel.file;
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads\", formFile.FileName);
            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
            {
                await formFile.CopyToAsync(fileStream);
            }
            sliderModel.img = Path.Combine(@"/Uploads/", formFile.FileName);
            MySqlConnection mySql = new MySqlConnection(connectionString);
            MySqlCommand mySqlCommand = new MySqlCommand("INSERT INTO sliders(title,content,url,img) VALUES(@title,@content,@url,@img)", mySql);
            mySqlCommand.Parameters.AddWithValue("@title", sliderModel.title);
            mySqlCommand.Parameters.AddWithValue("@content", sliderModel.content);
            mySqlCommand.Parameters.AddWithValue("@url", sliderModel.url);
            mySqlCommand.Parameters.AddWithValue("@img", sliderModel.img);
            mySql.Open();
            mySqlCommand.ExecuteNonQuery();
            mySql.Close();
            return Redirect("/Admin/Sliders");
        }

        [HttpGet]
        public IActionResult UpdateSlider(int id)
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                return Redirect("/Admin/Login");
            }
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlDataAdapter adapter = new MySqlDataAdapter("select * from sliders Where id=@id", connection);
            adapter.SelectCommand.Parameters.Clear();
            adapter.SelectCommand.Parameters.AddWithValue("@id", id);
            DataTable table = new DataTable();
            adapter.Fill(table);
            DataRow row = table.Rows[0];
            SliderModel sliderModel = new SliderModel()
            {
                id = row.Field<int>("id"),
                title = row.Field<string>("title"),
                content = row.Field<string>("content"),
                url = row.Field<string>("url"),
                img = row.Field<string>("img")
            };
            return View(sliderModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSlider(SliderModel sliderModel)
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                return Redirect("/Admin/Login");
            }
            if (sliderModel.file != null)
            {
                IFormFile formFile = sliderModel.file;
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads\", formFile.FileName);
                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(fileStream);
                }
                sliderModel.img = Path.Combine(@"/Uploads/", formFile.FileName);
            }
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand command = new MySqlCommand("Update sliders set title=@title,content=@content,img=@img,url=@url where id=@id", connection);
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@id", sliderModel.id);
            command.Parameters.AddWithValue("@title", sliderModel.title);
            command.Parameters.AddWithValue("@content", sliderModel.content);
            command.Parameters.AddWithValue("@url", sliderModel.url);
            command.Parameters.AddWithValue("@img", sliderModel.img);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            return Redirect("/Admin/Sliders");
        }

        public IActionResult DeleteSlider(int id)
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                return Redirect("/Admin/Login");
            }
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            MySqlCommand command = new MySqlCommand("Delete from sliders where id=@id", mySqlConnection);
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@id", id);
            mySqlConnection.Open();
            command.ExecuteNonQuery();
            mySqlConnection.Close();
            return Redirect("/Admin/Index");
        }

        public IActionResult Products()
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                return Redirect("/Admin/Login");
            }
            List<ProductModel> productModels = new List<ProductModel>();
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlDataAdapter mySql = new MySqlDataAdapter("Select * From products", connection);
            DataTable dataTable = new DataTable();
            mySql.Fill(dataTable);
            foreach (DataRow row in dataTable.Rows)
            {
                productModels.Add(
                    new ProductModel()
                    {
                        id = row.Field<int>("id"),
                        title = row.Field<string>("title"),
                        content = row.Field<string>("content"),
                        url = row.Field<string>("url"),
                        img = row.Field<string>("img")
                    });
            }
            return View(productModels);
        }

		[HttpGet]
		public IActionResult SaveProduct()
		{
			if (HttpContext.Session.GetString("username") == null)
			{
				return Redirect("/Admin/Login");
			}
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SaveProduct(ProductModel productModel)
		{
			if (HttpContext.Session.GetString("username") == null)
			{
				return Redirect("/Admin/Login");
			}
			IFormFile formFile = productModel.file;
			var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads\Product\", formFile.FileName);
			using (Stream fileStream = new FileStream(filePath, FileMode.Create))
			{
				await formFile.CopyToAsync(fileStream);
			}
            productModel.img = Path.Combine(@"/Uploads/Product/", formFile.FileName);
			MySqlConnection mySql = new MySqlConnection(connectionString);
			MySqlCommand mySqlCommand = new MySqlCommand("INSERT INTO products(title,content,url,img) VALUES(@title,@content,@url,@img)", mySql);
			mySqlCommand.Parameters.AddWithValue("@title", productModel.title);
			mySqlCommand.Parameters.AddWithValue("@content", productModel.content);
			mySqlCommand.Parameters.AddWithValue("@url", productModel.url);
			mySqlCommand.Parameters.AddWithValue("@img", productModel.img);
			mySql.Open();
			mySqlCommand.ExecuteNonQuery();
			mySql.Close();
			return Redirect("/Admin/Products");
		}

		[HttpGet]
		public IActionResult UpdateProduct(int id)
		{
			if (HttpContext.Session.GetString("username") == null)
			{
				return Redirect("/Admin/Login");
			}
			MySqlConnection connection = new MySqlConnection(connectionString);
			MySqlDataAdapter adapter = new MySqlDataAdapter("select * from products Where id=@id", connection);
			adapter.SelectCommand.Parameters.Clear();
			adapter.SelectCommand.Parameters.AddWithValue("@id", id);
			DataTable table = new DataTable();
			adapter.Fill(table);
			DataRow row = table.Rows[0];
			ProductModel productModel = new ProductModel()
			{
				id = row.Field<int>("id"),
				title = row.Field<string>("title"),
				content = row.Field<string>("content"),
				url = row.Field<string>("url"),
				img = row.Field<string>("img")
			};
			return View(productModel);
		}

		[HttpPost]
		public async Task<IActionResult> UpdateProduct(ProductModel productModel)
		{
			if (HttpContext.Session.GetString("username") == null)
			{
				return Redirect("/Admin/Login");
			}
			if (productModel.file != null)
			{
				IFormFile formFile = productModel.file;
				var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads\Product\", formFile.FileName);
				using (Stream fileStream = new FileStream(filePath, FileMode.Create))
				{
					await formFile.CopyToAsync(fileStream);
				}
				productModel.img = Path.Combine(@"/Uploads/Product/", formFile.FileName);
			}
			MySqlConnection connection = new MySqlConnection(connectionString);
			MySqlCommand command = new MySqlCommand("Update products set title=@title,content=@content,img=@img,url=@url where id=@id", connection);
			command.Parameters.Clear();
			command.Parameters.AddWithValue("@id", productModel.id);
			command.Parameters.AddWithValue("@title", productModel.title);
			command.Parameters.AddWithValue("@content", productModel.content);
			command.Parameters.AddWithValue("@url", productModel.url);
			command.Parameters.AddWithValue("@img", productModel.img);
			connection.Open();
			command.ExecuteNonQuery();
			connection.Close();
			return Redirect("/Admin/Products");
		}

		public IActionResult DeleteProduct(int id)
		{
			if (HttpContext.Session.GetString("username") == null)
			{
				return Redirect("/Admin/Login");
			}
			MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
			MySqlCommand command = new MySqlCommand("Delete from products where id=@id", mySqlConnection);
			command.Parameters.Clear();
			command.Parameters.AddWithValue("@id", id);
			mySqlConnection.Open();
			command.ExecuteNonQuery();
			mySqlConnection.Close();
			return Redirect("/Admin/Index");
		}


	}
}
