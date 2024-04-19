using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain.Entities;

namespace Order.Infra.Interfaces;
public class OrderMap : IEntityTypeConfiguration<OrderEntity>
{
    public void Configure(EntityTypeBuilder<OrderEntity> builder)
    {
        builder.ToTable("Orders");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .UseIdentityColumn()
            .HasColumnName("id")
            .HasColumnType("BIGINT");

        builder.Property(x => x.OrderStatus)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("status")
            .HasColumnType("VARCHAR(80)");

        builder.Property(x => x.Person)
            .IsRequired()
            .HasColumnName("status")
            .HasColumnType("BIGINT");

        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("createAt")
            .HasColumnType("TIMESTAMP");
    
        builder.Property(x => x.UpdateAt)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("updateAt")
            .HasColumnType("TIMESTAMP");
    }
}