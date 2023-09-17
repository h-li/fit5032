public class RegisterController : Controller
{
    private readonly DbContext _context;

    public RegisterController(DbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Map ViewModel to User model
            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                Password = model.Password
            };

            // Save user to the database
            _context.Users.Add(user);
            _context.SaveChanges();

            // Redirect to login page or perform login
            return RedirectToAction("Login", "Account");
        }
        return View(model);
    }
}
