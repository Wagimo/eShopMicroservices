

namespace Ordering.Infrastructure.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure ( EntityTypeBuilder<Order> builder )
    {
        builder.ToTable ( "Orders" );
        builder.HasKey ( o => o.Id );
        builder.Property ( o => o.Id ).HasConversion (
                       orderId => orderId.Value,
                       orderDbId => new OrderId ( orderDbId ) );

        builder.HasOne<Customer> ()
            .WithMany ()
            .HasForeignKey ( o => o.CustomerId )
            .IsRequired ();

        builder.HasMany<OrderItem> ()
            .WithOne ()
            .HasForeignKey ( o => o.OrderId );

        builder.ComplexProperty ( o => o.OrderName, nameBuilder =>
        {
            nameBuilder.Property ( o => o.Value )
            .HasColumnName ( nameof ( Order.OrderName ) )
            .HasMaxLength ( 100 )
            .IsRequired ();
        } );


        builder.ComplexProperty (
               x => x.ShippingAddress, addressBuilder =>
               {
                   addressBuilder.Property ( y => y.FirstName ).HasMaxLength ( 150 ).IsRequired ();
                   addressBuilder.Property ( y => y.LastName ).HasMaxLength ( 150 ).IsRequired ();
                   addressBuilder.Property ( y => y.EmailAddress ).HasMaxLength ( 150 ).IsRequired ();
                   addressBuilder.Property ( y => y.AddressLine ).HasMaxLength ( 250 ).IsRequired ();
                   addressBuilder.Property ( x => x.City ).HasMaxLength ( 50 ).IsRequired ();
                   addressBuilder.Property ( x => x.State ).HasMaxLength ( 50 ).IsRequired ();
                   addressBuilder.Property ( x => x.Country ).HasMaxLength ( 50 ).IsRequired ();
                   addressBuilder.Property ( x => x.ZipCode ).HasMaxLength ( 200 ).IsRequired ();
               }
           );

        builder.ComplexProperty (
              x => x.BillingAddress, addressBuilder =>
              {
                  addressBuilder.Property ( y => y.FirstName ).HasMaxLength ( 150 ).IsRequired ();
                  addressBuilder.Property ( y => y.LastName ).HasMaxLength ( 150 ).IsRequired ();
                  addressBuilder.Property ( y => y.EmailAddress ).HasMaxLength ( 150 ).IsRequired ();
                  addressBuilder.Property ( y => y.AddressLine ).HasMaxLength ( 250 ).IsRequired ();
                  addressBuilder.Property ( x => x.City ).HasMaxLength ( 50 ).IsRequired ();
                  addressBuilder.Property ( x => x.State ).HasMaxLength ( 50 ).IsRequired ();
                  addressBuilder.Property ( x => x.Country ).HasMaxLength ( 50 ).IsRequired ();
                  addressBuilder.Property ( x => x.ZipCode ).HasMaxLength ( 200 ).IsRequired ();
              }
          );

        builder.ComplexProperty (
               x => x.Payment, paimentBuilder =>
               {
                   paimentBuilder.Property ( y => y.CardName ).HasMaxLength ( 50 ).IsRequired ();
                   paimentBuilder.Property ( y => y.CardNumber ).HasMaxLength ( 24 ).IsRequired ();
                   paimentBuilder.Property ( y => y.Expiration ).HasMaxLength ( 12 ).IsRequired ();
                   paimentBuilder.Property ( y => y.CVV ).HasMaxLength ( 4 ).IsRequired ();
                   paimentBuilder.Property ( x => x.PaymentMethod ).HasMaxLength ( 100 ).IsRequired ();
               }
           );

        builder.Property ( o => o.OrderStatus )
            .HasDefaultValue ( OrderStatus.Draft )
            .HasConversion (
                s => s.ToString (),
                statusDb => (OrderStatus)Enum.Parse ( typeof ( OrderStatus ), statusDb )
            )
            .IsRequired ();

        builder.Property ( o => o.TotalPrice )
            .HasColumnType ( "decimal(18,4)" )
            .IsRequired ();
    }
}
