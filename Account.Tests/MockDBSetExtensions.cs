using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace Account.Tests
{
    public static class MockDBSetExtensions
    {
        public static Mock<DbSet<T>> GetDbSet<T>(IQueryable<T> TestData) where T : class
        {
            var MockSet = new Mock<DbSet<T>>();
            MockSet.As<IAsyncEnumerable<T>>().Setup(x => x.GetEnumerator()).Returns(new TestAsyncEnumerator<T>(TestData.GetEnumerator()));
            MockSet.As<IQueryable<T>>().Setup(x => x.Provider).Returns(new TestAsyncQueryProvider<T>(TestData.Provider));
            MockSet.As<IQueryable<T>>().Setup(x => x.Expression).Returns(TestData.Expression);
            MockSet.As<IQueryable<T>>().Setup(x => x.ElementType).Returns(TestData.ElementType);
            MockSet.As<IQueryable<T>>().Setup(x => x.GetEnumerator()).Returns(TestData.GetEnumerator());
            return MockSet;
        }
        //public static void SetSource<T>(this Mock<DbSet<T>> mockSet, IList<T> source) where T:class
        //{
        //    var data = source.AsQueryable();
        //    mockSet.As<IAsyncEnumerable<T>>().Setup(m => m.GetEnumerator()).Returns(new TestAsyncEnumerator<T>(data.GetEnumerator()));
        //    mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<T>(data.Provider));
        //    mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
        //    mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
        //    mockSet.As<IQueryable<T>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());
        //}
    }
}
