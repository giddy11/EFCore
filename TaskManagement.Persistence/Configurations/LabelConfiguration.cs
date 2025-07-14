using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagement.Domain;
using TaskManagement.Persistence.Utils;

namespace TaskManagement.Persistence.Configurations;

public class LabelConfiguration : IEntityTypeConfiguration<Label>
{
    public void Configure(EntityTypeBuilder<Label> builder)
    {
        builder.ToTable(nameof(TaskManagementDbContext.Labels), schema: PersistenceConstants.TaskManagement_SCHEMA);

        // Primary key
        builder.HasKey(l => l.Id);

        // Properties
        builder.Property(l => l.Name)
               .IsRequired()
               .HasMaxLength(100); // Adjust max length as needed

        builder.Property(l => l.Color)
               .HasMaxLength(7) // For hex color codes like #FF5733
               .IsRequired(false);

        // Unique constraint on Name to prevent duplicate labels
        builder.HasIndex(l => l.Name)
               .IsUnique()
               .HasDatabaseName("IX_Labels_Name_Unique");
    }
}
