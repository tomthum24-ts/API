using API.DOMAIN;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.INFRASTRUCTURE.EFConfigs
{
    public class JobApplysConfiguration : IEntityTypeConfiguration<JobApplys>
    {
        public void Configure(EntityTypeBuilder<JobApplys> builder)
        {
            builder.ToTable(TableConstants.JOBAPPLY_TABLENAME);
            builder.Property(x => x.Name).HasField("_name").HasColumnType("nvarchar(max)").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.IdJobs).HasField("_idJobs").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.IdUser).HasField("_idUser").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.TimeApply).HasField("_timeApply").HasMaxLength(1000).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Number).HasField("_number").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Note).HasField("_note").HasColumnType("nvarchar(max)").UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
