



namespace Ordering.Infrastructure.Data.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure ( EntityTypeBuilder<Customer> builder )
    {
        builder.ToTable ( "Customers" );
        builder.HasKey ( c => c.Id );
        builder.Property ( c => c.Id ).HasConversion (
            customerId => customerId.Value,
            customerDbId => CustomerId.Of ( customerDbId ) );

        builder.Property ( c => c.Name )
            .IsRequired ()
            .HasMaxLength ( 100 );

        builder.Property ( c => c.Email )
            .IsRequired ()
            .HasMaxLength ( 255 );

        builder.HasIndex ( c => c.Email ).IsUnique ();

        builder.Property ( c => c.Phone )
            .IsRequired ( false )
            .HasMaxLength ( 50 );

    }
}
