namespace Ordering.Infrastructure.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure ( EntityTypeBuilder<Product> builder )
    {
        builder.ToTable ( "Products" );

        builder.HasKey ( p => p.Id );

        builder.Property ( p => p.Id ).HasConversion (
            productId => productId.Value,
            productBdId => ProductId.Of ( productBdId ) );

        builder.Property ( p => p.Name )
            .IsRequired ()
            .HasMaxLength ( 100 );

        builder.Property ( p => p.Price )
            .HasColumnType ( "decimal(18,4)" )
            .IsRequired ();

    }
}
