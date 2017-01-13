namespace Kid.English.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class AddEntity_Phrase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Phrases",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Sentence = c.String(),
                        SentenceHtml = c.String(),
                        ChineseMean = c.String(),
                        CreatorUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        LastModifierUserId = c.Long(),
                        LastModificationTime = c.DateTime(),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Phrase_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.CreatorUserId)
                .ForeignKey("dbo.AbpUsers", t => t.DeleterUserId)
                .ForeignKey("dbo.AbpUsers", t => t.LastModifierUserId)
                .Index(t => t.CreatorUserId)
                .Index(t => t.LastModifierUserId)
                .Index(t => t.DeleterUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Phrases", "LastModifierUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.Phrases", "DeleterUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.Phrases", "CreatorUserId", "dbo.AbpUsers");
            DropIndex("dbo.Phrases", new[] { "DeleterUserId" });
            DropIndex("dbo.Phrases", new[] { "LastModifierUserId" });
            DropIndex("dbo.Phrases", new[] { "CreatorUserId" });
            DropTable("dbo.Phrases",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Phrase_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
