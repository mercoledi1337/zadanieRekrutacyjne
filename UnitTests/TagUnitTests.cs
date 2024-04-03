using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using RealWorldApp.Core.Exceptions;
using RealWorldApp.Core.Tags;
using RealWorldApp.Infrastructure.DAL;
using RealWorldApp.Infrastructure.DAL.Repositories;
using Shouldly;
using System;

namespace UnitTests
{
    public class TagUnitTests
    {
        private DataContext ArangeDb()
        {
            DbContextOptions<DataContext> dbContextOptions = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "dbo")
            .Options;
            DataContext context = new DataContext(dbContextOptions);
            context.Database.EnsureCreated();
            return context;
        }

        [Fact]
        public void TagCreateMethod_WhenTagCountNumberIsNegative_ShouldThrowTagCountException()
        {
            var exception = Record.Exception(() => Tag.Create("c#", -1));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<TagCountException>();
        }

        [Fact]
        public async Task GivenTag_TagRepositoryAddIt_ShouldBeAddedToDb()
        {
            //Arange
            var context = ArangeDb();
            var tag = Tag.Create("c#", 0);
            var tagAdd = new TagRepository(context);

            //Act
            await tagAdd.AddTag(tag);

            //Asert
            var res = context.Tags.Count();
            res.ShouldBe(1);
        }
    }
}