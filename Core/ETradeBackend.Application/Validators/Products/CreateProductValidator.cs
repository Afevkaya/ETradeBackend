using ETradeBackend.Application.ViewModels.Products;
using FluentValidation;

namespace ETradeBackend.Application.Validators.Products;

public class CreateProductValidator : AbstractValidator<VMCreateProduct>
{
    public CreateProductValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .NotNull().WithMessage("Lütfen ürün adını boş girmeyiniz")
            .MaximumLength(150)
            .MinimumLength(5).WithMessage("Lütfen ürün adını 5 karakter ile 150 karakter arasında giriniz");

        RuleFor(p => p.Stock)
            .NotEmpty()
            .NotNull().WithMessage("Lütfen stok bilgisini boş geçmeyin")
            .Must(s=>s >= 0).WithMessage("Stok bilgisi sıfırdan küçük olamaz");
        
        RuleFor(p => p.Price)
            .NotEmpty()
            .NotNull().WithMessage("Lütfen fiyat bilgisini boş geçmeyin")
            .Must(p=>p >= 0).WithMessage("Fiyat bilgisi sıfırdan küçük olamaz");
        
    }
}