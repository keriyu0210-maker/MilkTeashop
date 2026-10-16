using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilkTeaShop.Data;

namespace MilkTeaShop.Controllers;

public class ContentController : Controller
{
    private readonly ApplicationDbContext _context;
    public ContentController(ApplicationDbContext context) => _context = context;

    [HttpGet("/menu/tra-sua")]
    public IActionResult TeaMenu()
    {
        var products = _context.Products
            .Include(p => p.Category)
            .Include(p => p.Sizes)
            .Where(p => p.Category != null && p.Category.Name == "Trà sữa")
            .OrderBy(p => p.Name)
            .ToList();

        ViewData["Title"] = "Menu trà sữa - Chọn món và đặt online";
        ViewData["Description"] = "Xem menu trà sữa MilkTeaShop, giá từng món và tình trạng còn hàng. Chọn đồ uống yêu thích, đăng nhập và tùy chỉnh size, đường, đá khi đặt trà sữa online.";
        ViewData["CanonicalPath"] = "/menu/tra-sua";
        ViewData["Heading"] = "Menu trà sữa MilkTeaShop - Chọn món và đặt online";
        ViewData["Intro"] = "Khám phá các món trà sữa hiện có tại MilkTeaShop. Giá bên dưới là giá gốc; giá thanh toán thay đổi theo size, topping và số lượng bạn chọn.";
        // An empty category is still useful for visitors, but should not be indexed.
        if (products.Count == 0) ViewData["Robots"] = "noindex, follow";
        return View("~/Views/Product/Index.cshtml", products);
    }

    [HttpGet("/huong-dan/dat-hang")]
    public IActionResult OrderGuide() => View();
}
