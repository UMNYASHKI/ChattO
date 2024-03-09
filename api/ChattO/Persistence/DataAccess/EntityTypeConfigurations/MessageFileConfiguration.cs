using Domain.Models;
using Domain.Models.Files;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.DataAccess.EntityTypeConfigurations;

public class MessageFileConfiguration : IEntityTypeConfiguration<MessageFile>
{
    public void Configure(EntityTypeBuilder<MessageFile> builder)
    {
        builder.HasOne(messageFile => messageFile.Message)
            .WithOne(message=>message.MessageFile)
            .HasForeignKey<Message>(message=>message.MessageFileId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
