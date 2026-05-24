using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyMgmt.Domain.Entities;

namespace PropertyMgmt.Infrastructure.Persistence.Configurations;

public class ChatMessageConfiguration : IEntityTypeConfiguration<ChatMessage>
{
    public void Configure(EntityTypeBuilder<ChatMessage> builder)
    {
        builder.HasKey(m => m.Id);
        builder.Property(m => m.TenantId).HasMaxLength(50).IsRequired();
        builder.Property(m => m.Content).HasMaxLength(1000).IsRequired(); // حد أقصى للرسالة
        builder.Property(m => m.IsRead).HasDefaultValue(false);

        builder.HasOne(m => m.Conversation)
            .WithMany(c => c.Messages)
            .HasForeignKey(m => m.ConversationId)
            .OnDelete(DeleteBehavior.Cascade); 

        builder.HasOne(m => m.Sender)
            .WithMany()
            .HasForeignKey(m => m.SenderId)
            .OnDelete(DeleteBehavior.Cascade);

        // الاندكس السحري: لترتيب الرسائل تصاعدياً حسب وقت الإرسال بسرعة فائقة
        builder.HasIndex(m => new { m.ConversationId, m.CreatedAt });
    }
}